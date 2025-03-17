using FormulaM.AstNodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FormulaM;

public class Calculator
{
    private readonly string formula;
    private readonly Guid firm;

    internal static Guid Firm;
    internal static Dictionary<Guid, Dictionary<string, double>> variables;

    public double this[string key]
    {
        get => variables[firm][key];
        set {
            if (key == "pi" || key == "e" || key == "tau") {
                throw new CalculatorException($"The constant {key} cannot be changed.");
            }
            variables[firm][key] = value;
        }
    }

    public static Dictionary<string, Func<double[], double>> functions = new Dictionary<string, Func<double[], double>>()
    {
        { "abs", parameters => { ExpectParameters("abs", parameters, 1); return Math.Abs(parameters[0]); } },
        { "acos", parameters => { ExpectParameters("acos", parameters, 1); return Math.Acos(parameters[0]); } },
        { "asin", parameters => { ExpectParameters("asin", parameters, 1); return Math.Asin(parameters[0]); } },
        { "atan", parameters => { ExpectParameters("atan", parameters, 1); return Math.Atan(parameters[0]); } },
        { "atan2", parameters => { ExpectParameters("atan2", parameters, 2); return Math.Atan2(parameters[0], parameters[1]); } },
        { "ceiling", parameters => { ExpectParameters("ceiling", parameters, 1); return Math.Ceiling(parameters[0]); } },
        { "cos", parameters => { ExpectParameters("cos", parameters, 1); return Math.Cos(parameters[0]); } },
        { "cosh", parameters => { ExpectParameters("cosh", parameters, 1); return Math.Cosh(parameters[0]); } },
        { "exp", parameters => { ExpectParameters("exp", parameters, 1); return Math.Exp(parameters[0]); } },
        { "floor", parameters => { ExpectParameters("floor", parameters, 1); return Math.Floor(parameters[0]); } },
        { "log", parameters =>
            {
                ExpectParameters("log", parameters, new int[] { 1, 2 });
                return parameters.Length == 1
                    ? Math.Log(parameters[0])
                    : Math.Log(parameters[0], parameters[1]);
            }
        },
        { "log10", parameters => { ExpectParameters("log10", parameters, 1); return Math.Log10(parameters[0]); } },
        { "max", parameters => { ExpectParameters("max", parameters, 2); return Math.Max(parameters[0], parameters[1]); } },
        { "min", parameters => { ExpectParameters("min", parameters, 2); return Math.Min(parameters[0], parameters[1]); } },
        { "pow", parameters => { ExpectParameters("pow", parameters, 2); return Math.Pow(parameters[0], parameters[1]); } },
        { "round", parameters =>
            {
                ExpectParameters("round", parameters, new int[] { 1, 2 });
                return parameters.Length == 1
                    ? Math.Round(parameters[0])
                    : Math.Round(parameters[0], (int)parameters[1]);
            }
        },
        { "sign", parameters => { ExpectParameters("sign", parameters, 1); return (double)Math.Sign(parameters[0]); } },
        { "sin", parameters => { ExpectParameters("sin", parameters, 1); return Math.Sin(parameters[0]); } },
        { "sinh", parameters => { ExpectParameters("sinh", parameters, 1); return Math.Sinh(parameters[0]); } },
        { "sqrt", parameters => { ExpectParameters("sqrt", parameters, 1); return Math.Sqrt(parameters[0]); } },
        { "tan", parameters => { ExpectParameters("tan", parameters, 1); return Math.Tan(parameters[0]); } },
        { "tanh", parameters => { ExpectParameters("tanh", parameters, 1); return Math.Tanh(parameters[0]); } },
        { "ieeeremainder", parameters => { ExpectParameters("ieeeremainder", parameters, 2); return Math.IEEERemainder(parameters[0], parameters[1]); } },
        { "truncate", parameters => { ExpectParameters("truncate", parameters, 1); return Math.Truncate(parameters[0]); } }
    };

    public Calculator(string formula) {
        this.formula = formula;
        this.firm = Guid.NewGuid();
        variables = new Dictionary<Guid, Dictionary<string, double>>();
        variables[this.firm] = new Dictionary<string, double>()
        {
            {"pi", Math.PI },
            {"e", Math.E },
            {"tau", Math.Tau }
        };
    }

    public double Calculate()
    {
        Firm = firm;
        try
        {
            Lexer lexer = new Lexer(formula);
            Parser parser = new Parser(lexer.Tokenize());
            if (parser.Parse(out InitialNode root))
            {
                return (double)root.Visit();
            }
            else
            {
                throw new CalculatorException("This formula is incorrect.");
            }
        }
        catch(Exception e)
        {
            throw new CalculatorException(e.Message);
        }
    }

    public static void ExpectParameters(string functionName, double[] parameters, int count)
    {
        if (parameters.Length != count)
            throw new CalculatorException(
                $"The {functionName} function expects {count} parameter(s), but received {parameters.Length}.");
    }

    public static void ExpectParameters(string functionName, double[] parameters, int[] allowedCounts)
    {
        if (!allowedCounts.Contains(parameters.Length))
        {
            // Generamos un mensaje con los recuentos permitidos, por ejemplo "1 or 2"
            string allowed = string.Join(" or ", allowedCounts);
            throw new CalculatorException(
                $"The {functionName} function expects {allowed} parameter(s), but received {parameters.Length}.");
        }
    }
}
