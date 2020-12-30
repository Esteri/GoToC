
#define DBG_SHIFTING_STATE

using System;
using System.Collections.Generic;
using System.IO;

namespace goLexerAnalyzer
{
    class LexicalAnalyzer
    {
        public LexicalAnalyzer()
        {
        }

        public List<Token> Parse(string filepath)
        {
            StreamReader input = new StreamReader(filepath);
            Cursor cursor = new Cursor(input);

            List<Token> tokens = new List<Token>();
            Token token;

            State state = State.ProceedToNextStatement;
            LexicalError err = LexicalError.None;

            uint line = 1;
            uint skippedLines = 0;

            while (true)
            {
                switch (state)
                {
                    case State.ProceedToNextStatement:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
                        #endif

                        Parser.SkipEmptyLines(cursor, ref err, ref skippedLines);

                        line += skippedLines;

                        if (err != LexicalError.None)
                        {
                            state = State.Error;
                        }
                        else
                        {
                            state = State.ProceedToNextToken;
                        }
                    break;

                    case State.ProceedToNextToken:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
                        #endif

                        Parser.SkipSpaces(cursor);

                        if      (Utils.IsLetter  (cursor.CurrChar())) state = State.Identifier;
                        else if (Utils.IsDigit   (cursor.CurrChar())) state = State.Number;
                        else if (Utils.IsQuote   (cursor.CurrChar())) state = State.String;
                        else if (Utils.IsSymbol  (cursor.CurrChar())) state = State.Symbol;
                        else if (Utils.IsNewLine (cursor.CurrChar())) state = State.EndOfStatement;
                        else if (Utils.IsZeroChar(cursor.CurrChar())) state = State.Final;
                        else
                        {
                            err = LexicalError.UnexpectedChar;
                            state = State.Error;
                        }
                    break;

                    case State.Identifier:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
                        #endif

                        token = Parser.ProcessIdentifier(cursor, ref err);
                        
                        if (err != LexicalError.None)
                        {
                            state = State.Error;
                        }
                        else
                        {
                            tokens.Add(token);
                            state = State.ProceedToNextToken;
                        }
                    break;

                    case State.Number:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
                        #endif

                        token = Parser.ProcessNumber(cursor, ref err);

                        if (err != LexicalError.None)
                        {
                            state = State.Error;
                        }
                        else
                        {
                            tokens.Add(token);
                            state = State.ProceedToNextToken;
                        }
                    break;
                    
                    case State.String:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
                        #endif

                        token = Parser.ProcessString(cursor, ref err);

                        if (err != LexicalError.None)
                        {
                            state = State.Error;
                        }
                        else
                        {
                            tokens.Add(token);
                            state = State.ProceedToNextToken;
                        }
                    break;

                    case State.Symbol:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
                        #endif

                        token = Parser.ProcessSymbol(cursor, ref err);

                        if (err != LexicalError.None)
                        {
                            state = State.Error;
                        }
                        else
                        {
                            tokens.Add(token);
                            state = State.ProceedToNextToken;
                        }
                    break;

                    case State.EndOfStatement:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
                        #endif

                        token = Parser.ProcessEndOfStatement(cursor, ref err, ref skippedLines);

                        line += skippedLines;

                        if (err != LexicalError.None)
                        {
                            state = State.Error;
                        }
                        else
                        {
                            tokens.Add(token);
                            state = State.ProceedToNextStatement;
                        }
                    break;

                    case State.Final:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
#endif
                        // maybe this will change
                        if (tokens[tokens.Count - 1].Type != TokenType.EndOfStatement)
                            tokens.Add(new Token(TokenType.EndOfStatement, ""));


                        Console.WriteLine("Lexical Parsing success!");

                        /*
                        for (Token t in tokens)
                        {
                            Console.WriteLine(t.ToString());
                        }
                        */

                        input.Close();
                        return tokens;
                    //break;

                    case State.Error:
                        #if DBG_SHIFTING_STATE
                            Console.WriteLine("dbg: Entering state " + state.ToString());
                        #endif
                        Console.WriteLine("Error: " + err.ToString() + " at line(" + line + ")");
                        input.Close();
                        return null;
                    //break;
                }
            }
        }

        //----------------------------------------------

        private enum State
        {
            ProceedToNextStatement,
            ProceedToNextToken,

            Identifier,
            Number,
            String,
            Symbol,
            EndOfStatement,

            Final,
            Error
        }
    }
}