
using System.Text;

namespace goLexerAnalyzer
{
    public static partial class Parser
    {
        public static Token ProcessSymbol(Cursor cursor, ref LexicalError err)
        {
            err = LexicalError.None;

            if (!Utils.IsSymbol(cursor.CurrChar()))
            {
                err = LexicalError.UnexpectedChar;
                return null;
            }

            StringBuilder buffer = new StringBuilder();
            buffer.Append(cursor.CurrChar()); // currChar is Symbol, so append it, then check TokenType;
            TokenType tt = TokenType.Undefined;

            switch (cursor.CurrChar())
            {
                case '(': tt = TokenType.OpenningRoundBracket; break;
                case ')': tt = TokenType.ClosingRoundBracket; break;
                case '{': tt = TokenType.OpenningCurlyBracket; break;
                case '}': tt = TokenType.ClosingCurlyBracket; break;
                case ',': tt = TokenType.Comma; break;
                case ';': tt = TokenType.Semicolon; break;
                case '+': tt = TokenType.Addition; break;
                case '-': tt = TokenType.Addition; break;
                case '*': tt = TokenType.Multiplication; break;
                case '/': tt = TokenType.Multiplication; break;
                case '%': tt = TokenType.Multiplication; break;
                case '=':
                    if (cursor.NextChar() == '=')
                    {
                        tt = TokenType.Comparison;
                        buffer.Append(cursor.NextChar());
                        cursor.Move();
                    }
                    else
                    {
                        tt = TokenType.Assignment;
                    }
                    break;
                case ':':
                    if (cursor.NextChar() == '=')
                    {
                        tt = TokenType.ShortAssignment;
                        buffer.Append(cursor.NextChar());
                        cursor.Move();
                    }
                    else
                    {
                        tt = TokenType.Colon;
                    }
                    break;
                case '>':
                    tt = TokenType.Comparison;
                    if (cursor.NextChar() == '=')
                    {
                        buffer.Append(cursor.NextChar());
                        cursor.Move();
                    }
                    break;
                case '<':
                    tt = TokenType.Comparison;
                    if (cursor.NextChar() == '=')
                    {
                        buffer.Append(cursor.NextChar());
                        cursor.Move();
                    }
                    break;
                case '!':
                    if (cursor.NextChar() == '=')
                    {
                        tt = TokenType.Comparison;
                        buffer.Append(cursor.NextChar());
                        cursor.Move();
                    }
                    else
                    {
                        tt = TokenType.LogicalNegation;
                    }
                    break;
                case '|':
                    if (cursor.NextChar() == '|')
                    {
                        tt = TokenType.LogicalOr;
                        buffer.Append(cursor.NextChar());
                        cursor.Move();
                    }
                    else
                    {
                        err = LexicalError.UnexpectedChar;
                        return null;
                    }
                    break;
                case '&':
                    if (cursor.NextChar() == '&')
                    {
                        tt = TokenType.LogicalAnd;
                        buffer.Append(cursor.NextChar());
                        cursor.Move();
                    }
                    else
                    {
                        err = LexicalError.UnexpectedChar;
                        return null;
                    }
                    break;
            }
            cursor.Move(); // so all the Appended chars are skipped

            return new Token(tt, buffer.ToString());
        }
    }
}