using System;
using System.Collections.Generic;
using System.Text;

namespace goLexerAnalyzer
{
    public enum TokenType
    {
        Bool,
        Int,
        Float,
        String,
        BoolLiteral,
        IntLiteral,
        FloatLiteral,
        StringLiteral,

        Identifier,

        AssignmentInFor,
        Assignment,

        Semicolon,
        Colon,
        Comma,
        OpRoundBracket,
        ClRoundBracket,
        OpCurlyBracket,
        ClCurlyBracket,

        Compare,
        Arithmetic,
        Multiply,

        For,
        Return,
        Switch,
        Case,
        Default,
        If,
        Else,
        Func,
        Var,
        Const,
        Undefined,
    }
    struct Token
    {
        string lexem;
        TokenType type;

        public Token(string lexem, TokenType type) : this()
        {
            this.lexem = lexem;
            this.type = type;
        }

        public override string ToString()
        {
            return lexem.PadRight(15) + "\t\t" + type;
        }

    }
}
