using System;

namespace FormulaM.AstNodes
{
    internal class MuldivNode : Node
    {
        internal MuldivNode Muldiv { get; set; }
        internal PrecedenceNode Precedence { get; set; }
        internal MuldivNode() : base()
        {
        }
        internal override object Visit()
        {
            if (Muldiv != null)
            {
                if (Tokens[0].Type == TokenType.MUL)
                {
                    return (double)Muldiv.Visit() * (double)Precedence.Visit();
                }
                else if (Tokens[0].Type == TokenType.DIV)
                {
                    return (double)Muldiv.Visit() / (double)Precedence.Visit();
                }
                else if (Tokens[0].Type == TokenType.DIVE)
                {
                    return Math.Floor((double)Muldiv.Visit() / (double)Precedence.Visit());
                }
                else
                {
                    return (double)Muldiv.Visit() % (double)Precedence.Visit();
                }
            }
            else
            {
                return Precedence.Visit();
            }
        }
    }
}
