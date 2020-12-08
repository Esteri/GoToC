
namespace goLexerAnalyzer
{
    public static partial class Parser 
    {    
        public static Token ProcessEndOfStatement(Cursor cursor, ref LexicalError err, ref uint skippedLines)
        {
            err = LexicalError.None;
            skippedLines = 0;

            if (!Utils.IsNewLine(cursor.CurrChar()))
            {
                err = LexicalError.UnexpectedChar;
                return null;
            }

            if (cursor.CurrChar() == '\r' && cursor.NextChar() == '\n')
            {
                cursor.Move(2);
                skippedLines++;
            }
            else if (cursor.CurrChar() == '\n')
            {
                cursor.Move();
                skippedLines++;
            }
            else
            {
                err = LexicalError.UnexpectedChar;
                return null;
            }

            return new Token(TokenType.EndOfStatement, null);
        }
    }
}