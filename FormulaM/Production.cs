using System.Collections.Generic;

namespace FormulaM
{
    internal class Production
    {
        public string Left {  get; set; }
        public List<string> Right { get; set; }

        public Production(string left, List<string> right)
        {
            Left = left;
            Right = right;
        }
    }
}