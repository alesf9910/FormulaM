using FormulaM.Exceptions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FormulaM
{
    internal partial class Lexer
    {
        private readonly string input;

        Dictionary<TokenType, Regex> regPatterns = new()
        {
            [TokenType.SUM] = GetSUMRegex(),
            [TokenType.RES] = GetRESRegex(),
            [TokenType.MUL] = GetMULRegex(),
            [TokenType.DIVE] = GetDIVERegex(),
            [TokenType.DIV] = GetDIVRegex(),
            [TokenType.REST] = GetRESTRegex(),
            [TokenType.POW] = GetPOWRegex(),
            [TokenType.ID] = GetIDRegex(),
            [TokenType.PA] = GetPARegex(),
            [TokenType.PC] = GetPCRegex(),
            [TokenType.ASG] = GetASGRegex(),
            [TokenType.COMMA] = GetCOMMARegex(),
            [TokenType.NUM] = GetNUMRegex(),
            [TokenType.IGNORE] = GetIGNORERegex()
        };

        internal Lexer(string input)
        {
            this.input = input;
        }

        [GeneratedRegex("^\\+", RegexOptions.Compiled)]
        private static partial Regex GetSUMRegex();
        [GeneratedRegex("^-", RegexOptions.Compiled)]
        private static partial Regex GetRESRegex();
        [GeneratedRegex("^\\*", RegexOptions.Compiled)]
        private static partial Regex GetMULRegex();
        [GeneratedRegex("^//", RegexOptions.Compiled)]
        private static partial Regex GetDIVERegex();
        [GeneratedRegex("^/", RegexOptions.Compiled)]
        private static partial Regex GetDIVRegex();
        [GeneratedRegex("^%", RegexOptions.Compiled)]
        private static partial Regex GetRESTRegex();
        [GeneratedRegex("^\\^", RegexOptions.Compiled)]
        private static partial Regex GetPOWRegex();
        [GeneratedRegex("^[a-zA-Z_][a-zA-Z0-9_]*", RegexOptions.Compiled)]
        private static partial Regex GetIDRegex();
        [GeneratedRegex("^\\(", RegexOptions.Compiled)]
        private static partial Regex GetPARegex();
        [GeneratedRegex("^\\)", RegexOptions.Compiled)]
        private static partial Regex GetPCRegex();
        [GeneratedRegex("^=", RegexOptions.Compiled)]
        private static partial Regex GetASGRegex();
        [GeneratedRegex("^,", RegexOptions.Compiled)]
        private static partial Regex GetCOMMARegex();
        [GeneratedRegex("^[0-9]+(\\.[0-9]+)?", RegexOptions.Compiled)]
        private static partial Regex GetNUMRegex();
        [GeneratedRegex("^[ ]+", RegexOptions.Compiled)]
        private static partial Regex GetIGNORERegex();

        internal List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();
            int index = 0;
            while (index < input.Length)
            {
                bool matchFound = false;
                foreach (var pattern in regPatterns)
                {
                    var regex = pattern.Value;
                    var match = regex.Match(input.Substring(index));

                    if (match.Success)
                    {
                        if (pattern.Key != TokenType.IGNORE) tokens.Add(new Token(match.Value, pattern.Key, index));
                        index += match.Length;
                        matchFound = true;
                        break;
                    }
                }
                if (!matchFound)
                {
                    var errPos = index;
                    index++;
                    throw new LexerException(input[errPos], errPos);
                }
            }
            tokens.Add(new Token("", TokenType.EOF, -1));
            return tokens;
        }
    }
}