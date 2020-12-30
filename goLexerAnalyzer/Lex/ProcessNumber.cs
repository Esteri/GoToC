
using System.Text;

namespace goLexerAnalyzer
{
    public static partial class Parser
    {
        public static Token ProcessNumber(Cursor cursor, ref LexicalError err)
        {
            err = LexicalError.None;

            if (!Utils.IsDigit(cursor.CurrChar()))
            {
                err = LexicalError.UnexpectedChar;
                return null;
            }

            StringBuilder buffer  = new StringBuilder();

            while (Utils.IsDigit(cursor.CurrChar()))
            {
                buffer.Append(cursor.CurrChar());
                cursor.Move();
            }

            if (cursor.CurrChar() != '.')
            {
                return new Token(TokenType.IntLiteral, buffer.ToString());
            }
            else
            {
                // Append '.'
                buffer.Append(cursor.CurrChar());
                cursor.Move();

                while (Utils.IsDigit(cursor.CurrChar()))
                {
                    buffer.Append(cursor.CurrChar());
                    cursor.Move();
                }

                if (cursor.CurrChar() == 'e' || cursor.CurrChar() == 'E')
                {
                    buffer.Append(cursor.CurrChar());
                    cursor.Move();
                    if (cursor.CurrChar() == '+' || cursor.CurrChar() == '-')
                    {
                        buffer.Append(cursor.CurrChar());
                        cursor.Move(); 
                    }
                    if (!Utils.IsDigit(cursor.CurrChar()))
                    {
                        err = LexicalError.InvalidFloatLiteral;
                        return null;
                    }
                    else
                    {
                        while (Utils.IsDigit(cursor.CurrChar()))
                        {
                            buffer.Append(cursor.CurrChar());
                            cursor.Move();
                        }
                        return new Token(TokenType.FloatLiteral, buffer.ToString());
                    }
                }

                return new Token(TokenType.FloatLiteral, buffer.ToString());
            }
        }
    }
}