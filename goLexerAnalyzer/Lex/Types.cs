

namespace goLexerAnalyzer
{
    public enum TokenType
    {
        Arguments,
        Operand1lvl,
        Operand2lvl,
        Operand3lvl,
        Operand4lvl,
        Operand5lvl,
        Operand6lvl,

        Statement,
        Statements,
        VarDeclaration,
        FuncDeclaration,
        Parameters,

        StatementIf,
        ElseBrunch,
        StatementFor,

        StatementSwitch,
        CaseClause,
        CaseBrunches,

        Axiom,
        Declarations,

        Digit,
        DigitWithoutZero,
        Digits,
        Letter,
        EscapedSequences,
        Symbol,
        LetterDigitSet,

        // Identifiers
        Identifier,

        // Literals
        Literal,
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
        Import,
        Fmt,
        EndOfFile,
        Undefined,
        IdentifierReturn,
        IdentifierConst,
        IdentifierType,
        IdentifierVar,
        IdentifierFunc,
        IdentifierIf,
        IdentifierElse,
        IdentifierFor,
        IdentifierSwitch,
        IdentifierDefault,
        IdentifierCase
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