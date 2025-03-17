namespace FormulaM.AstNodes
{
    internal class VarsasgNode : Node
    {
        internal VarasgNode Varasg { get; set; }
        internal VarsasgNode Varsasg { get; set; }

        internal VarsasgNode() : base()
        {
        }

        internal override object Visit()
        {
            Varasg.Visit();
            if (Varsasg != null) Varsasg.Visit();
            return base.Visit();
        }
    }
}
