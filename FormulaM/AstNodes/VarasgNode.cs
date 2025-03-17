namespace FormulaM.AstNodes
{
    internal class VarasgNode : Node
    {
        internal FormulaNode Formula { get; set; }
        internal VarasgNode() : base()
        {
            
        }

        internal override object Visit()
        {
            Calculator.variables[Calculator.Firm][Tokens[0].Value] = (double)Formula.Visit();
            return null;
        }
    }
}
