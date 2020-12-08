
using System.Text;

namespace goLexerAnalyzer
{
    public static partial class Parser
    {
        public static Token ProcessString(Cursor cursor, ref LexicalError err)
        {
            err = LexicalError.None;
            
            if (!Utils.IsQuote(cursor.CurrChar()))
            {
                err = LexicalError.UnexpectedChar;
                return null;
            }
            StringBuilder buffer = new StringBuilder();

            cursor.Move(); // Skip first "

            while (!Utils.IsZeroChar(cursor.CurrChar()) && !Utils.IsQuote(cursor.CurrChar()))
            {
                if (cursor.CurrChar() == '\\')
                {
                    if (Utils.IsAllowedInEscapeSequence(cursor.NextChar()))
                    {
                        // Append current '\\'
                        buffer.Append(cursor.CurrChar());
                        cursor.Move();
                        // Append escaped char
                        buffer.Append(cursor.CurrChar());
                        cursor.Move();
                    }
                    else
                    {
                        err = LexicalError.UnexpectedChar;
                        return null;
                    }                   
                }
                else
                {
                    buffer.Append(cursor.CurrChar());
                    cursor.Move();
                }
            }

            if (Utils.IsQuote(cursor.CurrChar()))
            {
                cursor.Move(); // Skip last "

                return new Token(TokenType.StringLiteral, buffer.ToString());
            } 
            else
            {
                err = LexicalError.MissingClosingQuote;
                return null;
            }
        }
    }
}
