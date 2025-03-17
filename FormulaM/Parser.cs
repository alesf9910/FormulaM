using FormulaM.AstNodes;
using FormulaM.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FormulaM
{
    internal class Parser
    {
        private readonly Stack<Node> nodes = new Stack<Node>();
        private readonly Stack<Token> tokensQ = new Stack<Token>();
        private readonly Stack<int> states = new Stack<int>();

        private readonly Dictionary<(int, string), (char, int)> tablelalr = new Dictionary<(int, string), (char, int)>()
        {
            [(0, "initial")] = ('g', 5),
            [(0, "PA")] = ('s', 3),
            [(0, "formula")] = ('g', 6),
            [(0, "muldiv")] = ('g', 7),
            [(0, "precedence")] = ('g', 8),
            [(0, "fn")] = ('g', 9),
            [(0, "NUM")] = ('s', 4),
            [(0, "ID")] = ('s', 2),
            [(0, "RES")] = ('s', 1),
            [(1, "precedence")] = ('g', 12),
            [(1, "fn")] = ('g', 9),
            [(1, "PA")] = ('s', 11),
            [(1, "NUM")] = ('s', 4),
            [(1, "ID")] = ('s', 2),
            [(1, "RES")] = ('s', 1),
            [(2, "$")] = ('r', 15),
            [(2, "SUM")] = ('r', 15),
            [(2, "RES")] = ('r', 15),
            [(2, "MUL")] = ('r', 15),
            [(2, "DIVE")] = ('r', 15),
            [(2, "DIV")] = ('r', 15),
            [(2, "REST")] = ('r', 15),
            [(2, "POW")] = ('r', 15),
            [(2, "PC")] = ('r', 15),
            [(2, "COMMA")] = ('r', 15),
            [(2, "PA")] = ('s', 13),
            [(3, "varsAsg")] = ('g', 18),
            [(3, "varAsg")] = ('g', 22),
            [(3, "ID")] = ('s', 15),
            [(3, "formula")] = ('g', 19),
            [(3, "muldiv")] = ('g', 7),
            [(3, "precedence")] = ('g', 8),
            [(3, "fn")] = ('g', 9),
            [(3, "PA")] = ('s', 11),
            [(3, "NUM")] = ('s', 4),
            [(3, "RES")] = ('s', 1),
            [(4, "$")] = ('r', 14),
            [(4, "SUM")] = ('r', 14),
            [(4, "RES")] = ('r', 14),
            [(4, "MUL")] = ('r', 14),
            [(4, "DIVE")] = ('r', 14),
            [(4, "DIV")] = ('r', 14),
            [(4, "REST")] = ('r', 14),
            [(4, "POW")] = ('r', 14),
            [(4, "PC")] = ('r', 14),
            [(4, "COMMA")] = ('r', 14),
            [(5, "$")] = ('a', -1),
            [(6, "$")] = ('r', 1),
            [(6, "SUM")] = ('s', 24),
            [(6, "RES")] = ('s', 25),
            [(7, "$")] = ('r', 6),
            [(7, "SUM")] = ('r', 6),
            [(7, "RES")] = ('r', 6),
            [(7, "PC")] = ('r', 6),
            [(7, "COMMA")] = ('r', 6),
            [(7, "MUL")] = ('s', 26),
            [(7, "DIVE")] = ('s', 27),
            [(7, "DIV")] = ('s', 28),
            [(7, "REST")] = ('s', 29),
            [(8, "$")] = ('r', 11),
            [(8, "SUM")] = ('r', 11),
            [(8, "RES")] = ('r', 11),
            [(8, "MUL")] = ('r', 11),
            [(8, "DIVE")] = ('r', 11),
            [(8, "DIV")] = ('r', 11),
            [(8, "REST")] = ('r', 11),
            [(8, "PC")] = ('r', 11),
            [(8, "COMMA")] = ('r', 11),
            [(8, "POW")] = ('s', 30),
            [(9, "$")] = ('r', 12),
            [(9, "SUM")] = ('r', 12),
            [(9, "RES")] = ('r', 12),
            [(9, "MUL")] = ('r', 12),
            [(9, "DIVE")] = ('r', 12),
            [(9, "DIV")] = ('r', 12),
            [(9, "REST")] = ('r', 12),
            [(9, "POW")] = ('r', 12),
            [(9, "PC")] = ('r', 12),
            [(9, "COMMA")] = ('r', 12),
            [(11, "formula")] = ('g', 19),
            [(11, "muldiv")] = ('g', 7),
            [(11, "precedence")] = ('g', 8),
            [(11, "fn")] = ('g', 9),
            [(11, "PA")] = ('s', 11),
            [(11, "NUM")] = ('s', 4),
            [(11, "ID")] = ('s', 2),
            [(11, "RES")] = ('s', 1),
            [(12, "$")] = ('r', 16),
            [(12, "SUM")] = ('r', 16),
            [(12, "RES")] = ('r', 16),
            [(12, "MUL")] = ('r', 16),
            [(12, "DIVE")] = ('r', 16),
            [(12, "DIV")] = ('r', 16),
            [(12, "REST")] = ('r', 16),
            [(12, "POW")] = ('s', 30),
            [(12, "PC")] = ('r', 16),
            [(12, "COMMA")] = ('r', 16),
            [(13, "parameters")] = ('g', 41),
            [(13, "formula")] = ('g', 37),
            [(13, "muldiv")] = ('g', 7),
            [(13, "precedence")] = ('g', 8),
            [(13, "fn")] = ('g', 9),
            [(13, "PA")] = ('s', 11),
            [(13, "NUM")] = ('s', 4),
            [(13, "ID")] = ('s', 2),
            [(13, "RES")] = ('s', 1),
            [(15, "ASG")] = ('s', 45),
            [(15, "PC")] = ('r', 15),
            [(15, "SUM")] = ('r', 15),
            [(15, "RES")] = ('r', 15),
            [(15, "MUL")] = ('r', 15),
            [(15, "DIVE")] = ('r', 15),
            [(15, "DIV")] = ('r', 15),
            [(15, "REST")] = ('r', 15),
            [(15, "POW")] = ('r', 15),
            [(15, "PA")] = ('s', 13),
            [(18, "PC")] = ('s', 47),
            [(19, "PC")] = ('s', 50),
            [(19, "SUM")] = ('s', 24),
            [(19, "RES")] = ('s', 25),
            [(22, "PC")] = ('r', 2),
            [(22, "COMMA")] = ('s', 56),
            [(24, "muldiv")] = ('g', 57),
            [(24, "precedence")] = ('g', 8),
            [(24, "fn")] = ('g', 9),
            [(24, "PA")] = ('s', 11),
            [(24, "NUM")] = ('s', 4),
            [(24, "ID")] = ('s', 2),
            [(24, "RES")] = ('s', 1),
            [(25, "muldiv")] = ('g', 58),
            [(25, "precedence")] = ('g', 8),
            [(25, "fn")] = ('g', 9),
            [(25, "PA")] = ('s', 11),
            [(25, "NUM")] = ('s', 4),
            [(25, "ID")] = ('s', 2),
            [(25, "RES")] = ('s', 1),
            [(26, "precedence")] = ('g', 59),
            [(26, "fn")] = ('g', 9),
            [(26, "PA")] = ('s', 11),
            [(26, "NUM")] = ('s', 4),
            [(26, "ID")] = ('s', 2),
            [(26, "RES")] = ('s', 1),
            [(27, "precedence")] = ('g', 60),
            [(27, "fn")] = ('g', 9),
            [(27, "PA")] = ('s', 11),
            [(27, "NUM")] = ('s', 4),
            [(27, "ID")] = ('s', 2),
            [(27, "RES")] = ('s', 1),
            [(28, "precedence")] = ('g', 61),
            [(28, "fn")] = ('g', 9),
            [(28, "PA")] = ('s', 11),
            [(28, "NUM")] = ('s', 4),
            [(28, "ID")] = ('s', 2),
            [(28, "RES")] = ('s', 1),
            [(29, "precedence")] = ('g', 62),
            [(29, "fn")] = ('g', 9),
            [(29, "PA")] = ('s', 11),
            [(29, "NUM")] = ('s', 4),
            [(29, "ID")] = ('s', 2),
            [(29, "RES")] = ('s', 1),
            [(30, "NUM")] = ('s', 63),
            [(37, "PC")] = ('r', 20),
            [(37, "COMMA")] = ('s', 71),
            [(37, "SUM")] = ('s', 24),
            [(37, "RES")] = ('s', 25),
            [(41, "PC")] = ('s', 77),
            [(45, "formula")] = ('g', 80),
            [(45, "muldiv")] = ('g', 7),
            [(45, "precedence")] = ('g', 8),
            [(45, "fn")] = ('g', 9),
            [(45, "PA")] = ('s', 11),
            [(45, "NUM")] = ('s', 4),
            [(45, "ID")] = ('s', 2),
            [(45, "RES")] = ('s', 1),
            [(47, "formula")] = ('g', 82),
            [(47, "muldiv")] = ('g', 7),
            [(47, "precedence")] = ('g', 8),
            [(47, "fn")] = ('g', 9),
            [(47, "PA")] = ('s', 11),
            [(47, "NUM")] = ('s', 4),
            [(47, "ID")] = ('s', 2),
            [(47, "RES")] = ('s', 1),
            [(50, "$")] = ('r', 13),
            [(50, "SUM")] = ('r', 13),
            [(50, "RES")] = ('r', 13),
            [(50, "MUL")] = ('r', 13),
            [(50, "DIVE")] = ('r', 13),
            [(50, "DIV")] = ('r', 13),
            [(50, "REST")] = ('r', 13),
            [(50, "POW")] = ('r', 13),
            [(50, "PC")] = ('r', 13),
            [(50, "COMMA")] = ('r', 13),
            [(56, "varsAsg")] = ('g', 91),
            [(56, "varAsg")] = ('g', 22),
            [(56, "ID")] = ('s', 90),
            [(57, "$")] = ('r', 4),
            [(57, "SUM")] = ('r', 4),
            [(57, "RES")] = ('r', 4),
            [(57, "PC")] = ('r', 4),
            [(57, "COMMA")] = ('r', 4),
            [(57, "MUL")] = ('s', 26),
            [(57, "DIVE")] = ('s', 27),
            [(57, "DIV")] = ('s', 28),
            [(57, "REST")] = ('s', 29),
            [(58, "$")] = ('r', 5),
            [(58, "SUM")] = ('r', 5),
            [(58, "RES")] = ('r', 5),
            [(58, "PC")] = ('r', 5),
            [(58, "COMMA")] = ('r', 5),
            [(58, "MUL")] = ('s', 26),
            [(58, "DIVE")] = ('s', 27),
            [(58, "DIV")] = ('s', 28),
            [(58, "REST")] = ('s', 29),
            [(59, "$")] = ('r', 7),
            [(59, "SUM")] = ('r', 7),
            [(59, "RES")] = ('r', 7),
            [(59, "MUL")] = ('r', 7),
            [(59, "DIVE")] = ('r', 7),
            [(59, "DIV")] = ('r', 7),
            [(59, "REST")] = ('r', 7),
            [(59, "PC")] = ('r', 7),
            [(59, "COMMA")] = ('r', 7),
            [(59, "POW")] = ('s', 30),
            [(60, "$")] = ('r', 8),
            [(60, "SUM")] = ('r', 8),
            [(60, "RES")] = ('r', 8),
            [(60, "MUL")] = ('r', 8),
            [(60, "DIVE")] = ('r', 8),
            [(60, "DIV")] = ('r', 8),
            [(60, "REST")] = ('r', 8),
            [(60, "PC")] = ('r', 8),
            [(60, "COMMA")] = ('r', 8),
            [(60, "POW")] = ('s', 30),
            [(61, "$")] = ('r', 9),
            [(61, "SUM")] = ('r', 9),
            [(61, "RES")] = ('r', 9),
            [(61, "MUL")] = ('r', 9),
            [(61, "DIVE")] = ('r', 9),
            [(61, "DIV")] = ('r', 9),
            [(61, "REST")] = ('r', 9),
            [(61, "PC")] = ('r', 9),
            [(61, "COMMA")] = ('r', 9),
            [(61, "POW")] = ('s', 30),
            [(62, "$")] = ('r', 10),
            [(62, "SUM")] = ('r', 10),
            [(62, "RES")] = ('r', 10),
            [(62, "MUL")] = ('r', 10),
            [(62, "DIVE")] = ('r', 10),
            [(62, "DIV")] = ('r', 10),
            [(62, "REST")] = ('r', 10),
            [(62, "PC")] = ('r', 10),
            [(62, "COMMA")] = ('r', 10),
            [(62, "POW")] = ('s', 30),
            [(63, "$")] = ('r', 17),
            [(63, "SUM")] = ('r', 17),
            [(63, "RES")] = ('r', 17),
            [(63, "MUL")] = ('r', 17),
            [(63, "DIVE")] = ('r', 17),
            [(63, "DIV")] = ('r', 17),
            [(63, "REST")] = ('r', 17),
            [(63, "POW")] = ('r', 17),
            [(63, "PC")] = ('r', 17),
            [(63, "COMMA")] = ('r', 17),
            [(71, "parameters")] = ('g', 98),
            [(71, "formula")] = ('g', 37),
            [(71, "muldiv")] = ('g', 7),
            [(71, "precedence")] = ('g', 8),
            [(71, "fn")] = ('g', 9),
            [(71, "PA")] = ('s', 11),
            [(71, "NUM")] = ('s', 4),
            [(71, "ID")] = ('s', 2),
            [(71, "RES")] = ('s', 1),
            [(77, "$")] = ('r', 19),
            [(77, "SUM")] = ('r', 19),
            [(77, "RES")] = ('r', 19),
            [(77, "MUL")] = ('r', 19),
            [(77, "DIVE")] = ('r', 19),
            [(77, "DIV")] = ('r', 19),
            [(77, "REST")] = ('r', 19),
            [(77, "POW")] = ('r', 19),
            [(77, "PC")] = ('r', 19),
            [(77, "COMMA")] = ('r', 19),
            [(80, "PC")] = ('r', 18),
            [(80, "COMMA")] = ('r', 18),
            [(80, "SUM")] = ('s', 24),
            [(80, "RES")] = ('s', 25),
            [(82, "$")] = ('r', 0),
            [(82, "SUM")] = ('s', 24),
            [(82, "RES")] = ('s', 25),
            [(90, "ASG")] = ('s', 45),
            [(91, "PC")] = ('r', 3),
            [(98, "PC")] = ('r', 21)
        };


        private readonly List<Production> productions = new List<Production>(){
new Production("initial", ["PA", "varsAsg", "PC", "formula"]),
new Production("initial", ["formula"]),
new Production("varsAsg", ["varAsg"]),
new Production("varsAsg", ["varAsg", "COMMA", "varsAsg"]),
new Production("formula", ["formula", "SUM", "muldiv"]),
new Production("formula", ["formula", "RES", "muldiv"]),
new Production("formula", ["muldiv"]),
new Production("muldiv", ["muldiv", "MUL", "precedence"]),
new Production("muldiv", ["muldiv", "DIVE", "precedence"]),
new Production("muldiv", ["muldiv", "DIV", "precedence"]),
new Production("muldiv", ["muldiv", "REST", "precedence"]),
new Production("muldiv", ["precedence"]),
new Production("precedence", ["fn"]),
new Production("precedence", ["PA", "formula", "PC"]),
new Production("precedence", ["NUM"]),
new Production("precedence", ["ID"]),
new Production("precedence", ["RES", "precedence"]),
new Production("precedence", ["precedence", "POW", "NUM"]),
new Production("varAsg", ["ID", "ASG", "formula"]),
new Production("fn", ["ID", "PA", "parameters", "PC"]),
new Production("parameters", ["formula"]),
new Production("parameters", ["formula", "COMMA", "parameters"])
};


        private readonly List<Token> tokens;

        internal Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public bool Parse(out InitialNode root)
        {
            states.Push(0);
            int index = 0;

            while (true)
            {
                var token = index < tokens.Count ? tokens[index] : null;
                int state = states.Peek();
                string expect = token.Type == TokenType.EOF ? "$" : token.Type.ToString();
                if (!tablelalr.TryGetValue((state, expect), out var action))
                    throw new ParserException(token);

                if (action.Item1 == 's') // Shift
                {
                    states.Push(action.Item2);
                    tokensQ.Push(token);
                    index++;
                }
                else if (action.Item1 == 'r') // Reduce
                {
                    Reduce(action.Item2);
                }
                else if (action.Item1 == 'a') // Accept
                {
                    root = (InitialNode)nodes.Pop();
                    return true;
                }
            }
        }

        private void Reduce(int item2)
        {
            var tokenStack = new Stack<Token>();
            var production = productions[item2];
            for (int i = 0; i < production.Right.Count; i++)
            {
                states.Pop();
            }
            Node node = null;
            switch (item2)
            {
                case 0:
                    node = new InitialNode();
                    ((InitialNode)node).Formula = (FormulaNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((InitialNode)node).Varsasg = (VarsasgNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    break;

                case 1:
                    node = new InitialNode();
                    ((InitialNode)node).Formula = (FormulaNode)nodes.Pop();
                    break;

                case 2:
                    node = new VarsasgNode();
                    ((VarsasgNode)node).Varasg = (VarasgNode)nodes.Pop();
                    break;

                case 3:
                    node = new VarsasgNode();
                    ((VarsasgNode)node).Varsasg = (VarsasgNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((VarsasgNode)node).Varasg = (VarasgNode)nodes.Pop();
                    break;

                case 4:
                    node = new FormulaNode();
                    ((FormulaNode)node).Muldiv = (MuldivNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((FormulaNode)node).Formula = (FormulaNode)nodes.Pop();
                    break;

                case 5:
                    node = new FormulaNode();
                    ((FormulaNode)node).Muldiv = (MuldivNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((FormulaNode)node).Formula = (FormulaNode)nodes.Pop();
                    break;

                case 6:
                    node = new FormulaNode();
                    ((FormulaNode)node).Muldiv = (MuldivNode)nodes.Pop();
                    break;

                case 7:
                    node = new MuldivNode();
                    ((MuldivNode)node).Precedence = (PrecedenceNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((MuldivNode)node).Muldiv = (MuldivNode)nodes.Pop();
                    break;

                case 8:
                    node = new MuldivNode();
                    ((MuldivNode)node).Precedence = (PrecedenceNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((MuldivNode)node).Muldiv = (MuldivNode)nodes.Pop();
                    break;

                case 9:
                    node = new MuldivNode();
                    ((MuldivNode)node).Precedence = (PrecedenceNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((MuldivNode)node).Muldiv = (MuldivNode)nodes.Pop();
                    break;

                case 10:
                    node = new MuldivNode();
                    ((MuldivNode)node).Precedence = (PrecedenceNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((MuldivNode)node).Muldiv = (MuldivNode)nodes.Pop();
                    break;

                case 11:
                    node = new MuldivNode();
                    ((MuldivNode)node).Precedence = (PrecedenceNode)nodes.Pop();
                    break;

                case 12:
                    node = new PrecedenceNode();
                    ((PrecedenceNode)node).Fn = (FnNode)nodes.Pop();
                    break;

                case 13:
                    node = new PrecedenceNode();
                    tokenStack.Push(tokensQ.Pop());
                    ((PrecedenceNode)node).Formula = (FormulaNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    break;

                case 14:
                    node = new PrecedenceNode();
                    tokenStack.Push(tokensQ.Pop());
                    break;

                case 15:
                    node = new PrecedenceNode();
                    tokenStack.Push(tokensQ.Pop());
                    break;

                case 16:
                    node = new PrecedenceNode();
                    ((PrecedenceNode)node).Precedence = (PrecedenceNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    break;

                case 17:
                    node = new PrecedenceNode();
                    tokenStack.Push(tokensQ.Pop());
                    tokenStack.Push(tokensQ.Pop());
                    ((PrecedenceNode)node).Precedence = (PrecedenceNode)nodes.Pop();
                    break;

                case 18:
                    node = new VarasgNode();
                    ((VarasgNode)node).Formula = (FormulaNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    tokenStack.Push(tokensQ.Pop());
                    break;

                case 19:
                    node = new FnNode();
                    tokenStack.Push(tokensQ.Pop());
                    ((FnNode)node).Parameters = (ParametersNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    tokenStack.Push(tokensQ.Pop());
                    break;

                case 20:
                    node = new ParametersNode();
                    ((ParametersNode)node).Formula = (FormulaNode)nodes.Pop();
                    break;

                case 21:
                    node = new ParametersNode();
                    ((ParametersNode)node).Parameters = (ParametersNode)nodes.Pop();
                    tokenStack.Push(tokensQ.Pop());
                    ((ParametersNode)node).Formula = (FormulaNode)nodes.Pop();
                    break;


            }
            if (node != null)
            {
                node.Tokens = tokenStack.ToList();
            }
            nodes.Push(node);
            int state = states.Peek();
            if (!tablelalr.TryGetValue((state, production.Left), out var gotoAction) || gotoAction.Item1 != 'g')
                throw new Exception($"Error en la transición GOTO para {production.Left}.");

            states.Push(gotoAction.Item2);
        }
    }
}
