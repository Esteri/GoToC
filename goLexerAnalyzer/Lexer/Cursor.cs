
using System.IO;

namespace goLexerAnalyzer
{
    public class Cursor
    {
        public Cursor(StreamReader sr)
        {
            input = sr;
            this.Move(); // Inits currChar
        }

        public void Move()
        {
            if (input.Peek() != -1)
                currChar = (char)input.Read();
            else
                currChar = '\0';
        }

        public void Move(uint steps)
        {
            for (uint i = steps; i > 0 && currChar != '\0'; i--)
            {
                this.Move();
            }
        }

        public char CurrChar()
        {
            return currChar;
        }

        public char NextChar()
        {
            if (input.Peek() != -1)
                return (char)input.Peek();
            else
                return '\0';
        }

        //----------------------------------------------

        private StreamReader input;
        private char currChar;
    }
}