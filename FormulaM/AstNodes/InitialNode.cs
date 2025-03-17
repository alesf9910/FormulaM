namespace FormulaM.AstNodes
{
    internal class InitialNode : Node
    {
        internal VarsasgNode Varsasg { get; set; }
        internal FormulaNode Formula { get; set; }
        internal InitialNode() : base()
        {
        }

        internal override object Visit()
        {
            if (Varsasg != null) Varsasg.Visit();
            return Formula.Visit();
        }
    }
}
