namespace FormulaM.AstNodes
{
    internal class FormulaNode : Node
    {
        internal FormulaNode Formula { get; set; }
        internal MuldivNode Muldiv { get; set; }
        internal FormulaNode() : base()
        {
        }
        internal override object Visit()
        {
            if (Formula != null)
            {
                if(Tokens[0].Value == "+")
                {
                    return (double)Formula.Visit() + (double)Muldiv.Visit();
                }
                else
                {
                    return (double)Formula.Visit() - (double)Muldiv.Visit();
                }
            }
            else
            {
                return Muldiv.Visit();
            }
        }
    }
}
