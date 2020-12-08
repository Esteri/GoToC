
namespace goLexerAnalyzer
{
    public static partial class Parser
    {
        public static void SkipEmptyLines(Cursor cursor, ref LexicalError err, ref uint skippedLines)
        {
            err = LexicalError.None;
            skippedLines = 0;
            
            while ( Utils.IsNewLine(cursor.CurrChar()) || Utils.IsSpaceOrTab(cursor.CurrChar()) )
            {
                if (Utils.IsSpaceOrTab(cursor.CurrChar()))
                {
                    cursor.Move();
                }
                else if (cursor.CurrChar() == '\r' && cursor.NextChar() == '\n')
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
                    return;
                }
            }
        }
    }
}
