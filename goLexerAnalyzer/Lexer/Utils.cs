
using System;

namespace goLexerAnalyzer
{
    static class Utils 
    {
        public static bool IsLetter(char ch)
        {
            return Char.IsLetter(ch) || ch == '_';
        }

        public static bool IsDigit(char ch)
        {
            return Char.IsDigit(ch);
        }

        public static bool IsSymbol(char ch)
        {
            char[] symbols = { '(', ')', '{', '}', ';', ':', ',', '+', '-', '/', '%', '*', '<', '>', '!', '=', '&', '|' };
            foreach (char s in symbols)
            {
                if (ch == s) return true;
            }
            return false;
        }

        public static bool IsQuote(char ch)
        {
            return ch == '"';
        }

        public static bool IsSpaceOrTab(char ch)
        {
            return ch == ' ' || ch == '\t';
        }

        public static bool IsZeroChar(char ch)
        {
            return ch == '\0';
        }

        public static bool IsNewLine(char ch)
        {
            return ch == '\n' || ch == '\r';
        }

        public static bool IsAllowedInEscapeSequence(char ch)
        {
            return ch == '\''
                || ch == '"'
                || ch == '?'
                || ch == '\\'
                || ch == 'a'
                || ch == 'b'
                || ch == 'f'
                || ch == 'n'
                || ch == 'r'
                || ch == 't'
                || ch == 'v'
                || ch == 'x'
                || ch == 'u'
                || ch == 'U';
        }
    }
}