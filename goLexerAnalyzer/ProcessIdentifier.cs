
using System.Text;

namespace goLexerAnalyzer
{
    public static partial class Parser
    {
        public static Token ProcessIdentifier(Cursor cursor, ref LexicalError err)
        {
            err = LexicalError.None;

            if (!Utils.IsLetter(cursor.CurrChar()))
            {
                err = LexicalError.UnexpectedChar;
                return null;
            }

            StringBuilder buffer = new StringBuilder();

            while (Utils.IsLetter(cursor.CurrChar()) || Utils.IsDigit(cursor.CurrChar()))
            {
                buffer.Append(cursor.CurrChar());
                cursor.Move();
            }

            if (cursor.CurrChar() == '.')
            {
                // Append '.'
                buffer.Append(cursor.CurrChar());
                cursor.Move();

                while (Utils.IsLetter(cursor.CurrChar()) || Utils.IsDigit(cursor.CurrChar()))
                {
                    buffer.Append(cursor.CurrChar());
                    cursor.Move();
                }

                string lexem = buffer.ToString();
                if (!lexem.Equals("fmt.Print") && !lexem.Equals("fmt.Scan"))
                {
                    err = LexicalError.InvalidIdentifier;
                    return null;
                }
            }

            return new Token(TokenType.Identifier, buffer.ToString());
        }
    }
}