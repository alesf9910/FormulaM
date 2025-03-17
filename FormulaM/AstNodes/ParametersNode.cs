using System.Collections.Generic;

namespace FormulaM.AstNodes
{
    internal class ParametersNode : Node
    {
        internal FormulaNode Formula { get; set; }
        internal ParametersNode Parameters { get; set; }
        internal ParametersNode() : base()
        {
        }

        internal override object Visit()
        {
            if(Parameters != null)
            {
                var nums = new List<double>() { (double)Formula.Visit() };
                nums.AddRange((double[])Parameters.Visit());
                return nums.ToArray();
            }
            else
            {
                return new double[] { (double)Formula.Visit() };
            }
        }
    }
}
