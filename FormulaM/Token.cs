namespace FormulaM
{
    internal class Token
    {
        public string Value {  get; private set; }
        public TokenType Type { get; private set; }
        public int Position {  get; private set; }

        internal Token(string value, TokenType type, int position)
        {
            Value = value;
            Type = type;
            Position = position;
        }
    }

    internal enum TokenType
    {
        SUM, RES, MUL, DIVE, DIV, REST, POW, ID, PA, PC, ASG, COMMA, NUM, EOF, IGNORE
    }
}
