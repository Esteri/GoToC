
namespace goLexerAnalyzer
{
    public static partial class Parser
    {
        public static void SkipSpaces(Cursor cursor)
        {
            while ( Utils.IsSpaceOrTab(cursor.CurrChar()) )
            {
                cursor.Move();
            }
        }
    }
}
