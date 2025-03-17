using System;

namespace FormulaM.AstNodes
{
    internal class PrecedenceNode : Node
    {
        internal FnNode Fn { get; set; }
        internal FormulaNode Formula { get; set; }
        internal PrecedenceNode Precedence { get; set; }

        internal PrecedenceNode() : base()
        {
        }

        internal override object Visit()
        {
            if (Fn != null) return Fn.Visit();
            else if (Formula != null) return Formula.Visit();
            else if (Tokens[0].Type == TokenType.NUM) return double.Parse(Tokens[0].Value);
            else if (Tokens[0].Type == TokenType.ID)
            {
                if (Calculator.variables[Calculator.Firm].ContainsKey(Tokens[0].Value))
                {
                    return Calculator.variables[Calculator.Firm][Tokens[0].Value];
                }
                else
                {
                    throw new CalculatorException($"The variable {Tokens[0].Value} does not exist");
                }
            }
            else if (Tokens[0].Type == TokenType.RES)
            {
                return -(double)Precedence.Visit();
            }
            else
            {
                return Math.Pow((double)Precedence.Visit(), double.Parse(Tokens[1].Value));
            }
        }
    }
}
