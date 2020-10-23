// Comment following to turn off debug options
//#define DEBUG_STATE_SHIFTING
#define DEBUG_PARSED_LEXEM

using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace goLexerAnalyzer
{

    class Lexer
    {
        public Lexer() { }

        public List<Token> Parse(string inputFilePath)
        {
            Reset(inputFilePath);

            while (true)
            {
                switch (state)
                {
                    case State.Default:
                        // TODO: process \n\r as TokenType.EndOfStatement or whatever
                        // TODO: count lines and (optionally) symbols to indicate where error occured
                        while (IsSpaceOrTab(currChar) || currChar == '\n' || currChar == '\r') MoveCursor(); // Skip tabs and spaces

                        if (IsLetterOr_(currChar)) ShiftState(State.Identifier);
                        else if (IsDigit(currChar)) ShiftState(State.Number);
                        else if (IsQuote(currChar)) ShiftState(State.String);
                        else if (IsSymbol(currChar)) ShiftState(State.Symbol);
                        else if (IsZeroChar(currChar)) ShiftState(State.Final);
                        else
                        {
                            err = Error.UnexpectedChar;
                            ShiftState(State.Error);
                        }
                        break;

                    case State.Identifier:
                        ParseIdentifier();
                        break;

                    case State.Number:
                        ParseNumber();
                        break;

                    case State.String:
                        ParseString();
                        break;

                    case State.Symbol:
                        ParseSymbol();
                        break;

                    case State.Final:
                        Console.WriteLine("Lexical parsing success!");
                        // TODO: Close File
                        return tokens;
                        break;

                    case State.Error:
                        switch (err)
                        {
                            case Error.UnexpectedChar:
                                Console.WriteLine("ERROR: " + err.ToString() + ", char code = " + (uint)currChar);
                                break;
                            case Error.MissingClosingQuote:
                                Console.WriteLine("ERROR: " + err.ToString() + " in string literal");
                                break;
                            case Error.UndefinedTokenType:
                                Console.WriteLine("ERROR: " + err.ToString());
                                break;
                            case Error.WrongOperatorSyntax:
                                Console.WriteLine("ERROR: " + err.ToString());
                                break;

                            /*
                            default:
                                Console.WriteLine("ERROR: Unknown error");
                            */
                        }
                        // TODO: Close File
                        return null;
                        break;
                }
            }
        }

        private void ParseSymbol()
        {
            buffer.Append(currChar);
            TokenType tt = TokenType.Undefined;
            switch (currChar)
            {
                // TODO: add ==
                case '(': tt = TokenType.OpenningRoundBracket; break;
                case ')': tt = TokenType.ClosingRoundBracket; break;
                case '{': tt = TokenType.OpenningCurlyBracket; break;
                case '}': tt = TokenType.ClosingCurlyBracket; break;
                case ',': tt = TokenType.Comma; break;
                case ';': tt = TokenType.Semicolon; break;
                case '+': tt = TokenType.Arithmetic; break;
                case '-': tt = TokenType.Arithmetic; break;
                case '*': tt = TokenType.Multiply; break;
                case '/': tt = TokenType.Multiply; break;
                case '%': tt = TokenType.Multiply; break;


                case '=':
                    if(nextChar == '=')
                    {
                        tt = TokenType.Comparison;
                        buffer.Append(nextChar);
                        MoveCursor();
                    }
                    else
                    {
                        tt = TokenType.Assignment;
                    }
                    break;

                case ':':
                    if (nextChar == '=')
                    {
                        tt = TokenType.AssignmentInFor;
                        buffer.Append(nextChar);
                        MoveCursor();
                    }
                    else
                    {
                        tt = TokenType.Colon;
                    }
                    break;

                case '>':
                    tt = TokenType.Comparison;
                    if (nextChar == '=')
                    {
                        buffer.Append(nextChar);
                        MoveCursor();
                    }
                    break;

                case '<':
                    tt = TokenType.Comparison;
                    if (nextChar == '=')
                    {
                        buffer.Append(nextChar);
                        MoveCursor();
                    }
                    break;

                case '!':
                    tt = TokenType.Comparison;
                    if (nextChar == '=')
                    {
                        buffer.Append(nextChar);
                        MoveCursor();
                    }
                    else
                    {
                        err = Error.WrongOperatorSyntax; //TODO: maybe to do the same for every opearator?
                    }
                    break;
            }

            if (tt == TokenType.Undefined)
            {
                err = Error.UndefinedTokenType;
                ShiftState(State.Error);
            }
            else if (err != Error.None)
            {
                ShiftState(State.Error);
            }
            else
            {
                string lexem = buffer.ToString();
                tokens.Add(new Token(lexem, tt));
                #if DEBUG_PARSED_LEXEM
                    Console.WriteLine("Debug: Lexem parsed: <" + lexem + ", " + tt + ">");
                #endif
                MoveCursor();
                buffer.Clear();
                ShiftState(State.Default);
            }
        }

        private void ParseString()
        {
            do
            {
                buffer.Append(currChar);
                if (currChar == '\\')
                {
                    // TODO: Check if escape sequence is correct
                    MoveCursor();
                    buffer.Append(currChar);
                }
                MoveCursor();
            } while (!IsZeroChar(currChar) && !IsQuote(currChar));

            if (IsQuote(currChar))
            {
                buffer.Append(currChar); // Append last '"'
                MoveCursor();

                string lexem = buffer.ToString();
                TokenType tt = TokenType.StringLiteral;
                tokens.Add(new Token(lexem, tt));
                #if DEBUG_PARSED_LEXEM
                    Console.WriteLine("Debug: Lexem parsed: <" + lexem + ", " + tt + ">");
                #endif
                buffer.Clear();
                ShiftState(State.Default);
            } 
            else
            {
                err = Error.MissingClosingQuote;
                ShiftState(State.Error);
            }
        }

        private void ParseNumber()
        {
            // Integer numbers only (for now)
            // TODO: float numbers
            while (IsDigit(currChar))
            {
                buffer.Append(currChar);
                MoveCursor();
            } 
            
            string lexem = buffer.ToString();
            TokenType tt = TokenType.IntLiteral;
            tokens.Add(new Token(lexem, tt));
            #if DEBUG_PARSED_LEXEM
                Console.WriteLine("Debug: Lexem parsed: <" + lexem + ", " + tt + ">");
            #endif
            buffer.Clear();
            ShiftState(State.Default);
        }

        private void ParseIdentifier()
        {
            // TODO: parsing fmt.Print and fmt.Scan
            while (!IsZeroChar(currChar) && (IsDigit(currChar) || IsLetterOr_(currChar)))
            {
                buffer.Append(currChar);
                MoveCursor();
            }

            string lexem = buffer.ToString();
            TokenType tt = ProbeKeyword(lexem);
            tokens.Add(new Token(lexem, tt));
            #if DEBUG_PARSED_LEXEM
                Console.WriteLine("Debug: Lexem parsed: <" + lexem + ", " + tt + ">");
            #endif

            buffer.Clear();
            ShiftState(State.Default);
        }


        //-----------------------------------------

        private enum Error
        {
            None,
            UnexpectedChar,
            MissingClosingQuote,
            UndefinedTokenType,
            WrongOperatorSyntax
        }

        private enum State
        {
            Default,
            Identifier,
            Number,
            String,
            Symbol,
            Final,
            Error
        }

        private State state;
        private Error err;
        private List<Token> tokens;
        private StringBuilder buffer;
        private StreamReader input;
        private char currChar;
        private char nextChar;

        //-----------------------------------------

        private void Reset(string inputFilePath)
        {
            input = new StreamReader(inputFilePath);
            tokens = new List<Token>();
            state = State.Default;
            err = Error.None;

            if (buffer == null) buffer = new StringBuilder();
            else buffer.Clear();

            MoveCursor(); // Inits cursor
        }

        private void ShiftState(State st)
        {
            #if DEBUG_STATE_SHIFTING
                Console.WriteLine("Debug: Shifting state: " + state.ToString() + " -> " + st.ToString());
            #endif
            state = st;
        }

        private void MoveCursor()
        {
            if (input.Peek() != -1)
                currChar = (char)input.Read();
            else
                currChar = '\0';

            if (input.Peek() != -1)
                nextChar = (char)input.Peek();
            else
                nextChar = '\0';
        }

        private bool IsLetterOr_(char ch)
        {
            return Char.IsLetter(ch) || ch == '_';
        }

        private bool IsDigit(char ch)
        {
            return Char.IsDigit(ch);
        }

        private bool IsSymbol(char ch)
        {
            char[] symbols = { '(', ')', '{', '}', ';', ':', ',', '+', '-', '/', '%', '*', '<', '>', '!', '=', };
            foreach (char s in symbols)
            {
                if (ch == s) return true;
            }
            return false;
        }

        private bool IsQuote(char ch)
        {
            return ch == '"';
        }

        private bool IsSpaceOrTab(char ch)
        {
            return ch == ' ' || ch == '\t';
        }

        private bool IsZeroChar(char ch)
        {
            return ch == '\0';
        }

        private static TokenType ProbeKeyword(string str)
        {
            if (str == "for")
                return TokenType.ForKeyword;
            if (str == "switch")
                return TokenType.SwitchKeyword;
            if (str == "case")
                return TokenType.CaseKeyword;
            if (str == "default")
                return TokenType.DefaultKeyword;
            if (str == "if")
                return TokenType.IfKeyword;
            if (str == "else")
                return TokenType.ElseKeyword;
            if (str == "func")
                return TokenType.FuncKeyword;
            if (str == "return")
                return TokenType.ReturnKeyword;
            if (str == "var")
                return TokenType.VarKeyword;
            if (str == "const")
                return TokenType.ConstKeyword;
            if (str == "int")
                return TokenType.IntKeyword;
            if (str == "float")
                return TokenType.FloatKeyword;
            if (str == "bool")
                return TokenType.BoolKeyword;
            if (str == "string")
                return TokenType.StringKeyword;
            return TokenType.Identifier;
        }

    }

} // namespace go_lexer_analyzer