

namespace goLexerAnalyzer
{
    public enum TokenType
    {
        // Identifiers
        Identifier,

        // Literals
        BoolLiteral,
        IntLiteral,
        FloatLiteral,
        StringLiteral,

        // Operators
        Comparison,
        Addition,
        Multiplication,
        LogicalOr,
        LogicalAnd, 
        LogicalNegation,

        OpenningRoundBracket,
        ClosingRoundBracket,

        OpenningCurlyBracket,
        ClosingCurlyBracket,

        Colon,
        Comma,
        Semicolon,

        Assignment,
        ShortAssignment,

        // Service symbol
        EndOfStatement,

        Undefined
    }

    public enum LexicalError
    {
        InvalidIdentifier,
        UnexpectedChar,
        InvalidFloatLiteral,
        MissingClosingQuote,

        None
    }
}