using System.Collections.Generic;

namespace FormulaM.AstNodes
{
    internal abstract class Node
    {
        public List<Token> Tokens { get; set; }

        public Node(){
            Tokens = new();
        }

        internal virtual object Visit()
        {
            return null;
        }
    }
}
