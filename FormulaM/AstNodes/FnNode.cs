namespace FormulaM.AstNodes
{
    internal class FnNode : Node
    {
        internal ParametersNode Parameters { get; set; }

        internal FnNode() : base()
        {
        }

        internal override object Visit()
        {
            var fnName = Tokens[0].Value;
            if (Calculator.functions.ContainsKey(fnName)) {
                if(Parameters == null)
                {
                    return Calculator.functions[fnName].Invoke(null);
                }
                else
                {
                    return Calculator.functions[fnName].Invoke((double[])Parameters.Visit());
                }
            }
            else
            {
                throw new CalculatorException($"{fnName} function is not defined.");
            }
        }
    }
}
