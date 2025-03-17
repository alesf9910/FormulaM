using System;

namespace FormulaM.Exceptions
{
    internal class ParserException : Exception
    {
        public ParserException(Token token) :
            base($"Syntax error: Unexpected token Token(Type: {token.Type}, Value: \"{token.Value}\", Position: {token.Position}).") {
            
        }
    }
}