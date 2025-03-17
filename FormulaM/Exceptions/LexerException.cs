using System;

namespace FormulaM.Exceptions
{
    internal class LexerException : Exception
    {
        public LexerException(char character, int position) :
            base($"Error in position {position} character {character}.") {
            
        }
    }
}
