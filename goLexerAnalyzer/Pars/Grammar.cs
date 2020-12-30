
using System;
using System.Linq;
using System.Collections.Generic;

namespace goLexerAnalyzer
{
    /*Nonterminals:
    
        Arguments,
        Operand1lvl,
        Operand2lvl,
        Operand3lvl,
        Operand4lvl,
        Operand45lvl,
        Operand46lvl,

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
        Id,
        Literal,
        BoolLiteral,
        IntegerLiteral,
        Exponent,
        FloatLiteral,
        SymbolSet,
        StringLiteral,
        ConditionOperator,
        AdditionOperator,
        MultiplyOperator,
        OrOperator,
        AndOperator,
        NotOperator,
*/
    public class Grammar
    {
        private List<Production> productions;
        private Dictionary<Nonterminal, List<Production>> ntTable;
        Nonterminal start;
        
        // задаем каждую продукцию однострочно в виде A -> B
        private void InitGrammar()
        {
            List<Production> productions = new List<Production> {

            new Production(
                Nonterminal.Of(TokenType.Axiom),
                new Sentence
                {
                    Terminal.Of(TokenType.Import),
                    Terminal.Of(TokenType.Fmt),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Declarations),
                    //Terminal.Eof
                }, 0),

            // --------------- ВЫРАЖЕНИЯ ---------------

            new Production(
                Nonterminal.Of(TokenType.Arguments),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand1lvl)
                }, 1),

            new Production(
                Nonterminal.Of(TokenType.Arguments),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Comma),
                    Nonterminal.Of(TokenType.Arguments)
                }, 2),

            new Production(
                Nonterminal.Of(TokenType.Operand1lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand2lvl),
                    Terminal.Of(TokenType.LogicalOr),
                    Nonterminal.Of(TokenType.Operand1lvl)
                }, 3),
            new Production(
                Nonterminal.Of(TokenType.Operand1lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand2lvl)
                }, 4),

            new Production(
                Nonterminal.Of(TokenType.Operand2lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand3lvl),
                    Terminal.Of(TokenType.LogicalAnd),
                    Nonterminal.Of(TokenType.Operand2lvl)
                }, 5),

            new Production(
                Nonterminal.Of(TokenType.Operand2lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand3lvl)
                }, 6),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand4lvl),
                    Terminal.Of(TokenType.Comparison),
                    Nonterminal.Of(TokenType.Operand4lvl)
                }, 7),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Literal)
                }, 8),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }, 9),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.LogicalNegation),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }, 10),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand4lvl),
                }, 11),

            /*new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand4lvl),
                    Terminal.Of(TokenType.Comparison),
                    Nonterminal.Of(TokenType.Operand4lvl) //  DONE операнд 3ур> -->  <операнд 4ур> <знак операции сравнения> <операнд 4ур>
                }, 12),*/

            new Production(
                Nonterminal.Of(TokenType.Operand4lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand5lvl),
                    Terminal.Of(TokenType.Addition),
                    Nonterminal.Of(TokenType.Operand4lvl)
                }, 13),

            new Production(
                Nonterminal.Of(TokenType.Operand4lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand5lvl)
                }, 14),

            new Production(
                Nonterminal.Of(TokenType.Operand5lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand6lvl),
                    Terminal.Of(TokenType.Multiplication),
                    Nonterminal.Of(TokenType.Operand5lvl)
                }, 15),

            new Production(
                Nonterminal.Of(TokenType.Operand5lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand6lvl)
                }, 16),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier)
                }, 17),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }, 18),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Arguments),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }, 19),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Literal)
                }, 20),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Operand4lvl),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }, 21),

            // --------------- ВЫСКАЗЫВАНИЯ ---------------

            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.VarDeclaration)
                }, 22),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.StatementIf)
                }, 23),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.StatementSwitch)
                }, 24),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.StatementFor)
                }, 25),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierReturn),
                    Terminal.Of(TokenType.EndOfStatement),
                }, 26),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierReturn),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 27),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.Assignment),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 28),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Terminal.Of(TokenType.ClosingRoundBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 29),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Arguments),
                    Terminal.Of(TokenType.ClosingRoundBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 30),
            new Production(
                Nonterminal.Of(TokenType.Statements),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Statement)
                }, 31),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Statement),
                    Nonterminal.Of(TokenType.Statements),
                }, 32),

            // --------------- ОБЪЯВЛЕНИЯ ---------------

            new Production(
                Nonterminal.Of(TokenType.Declarations),
                new Sentence
                {
                    Nonterminal.Of(TokenType.VarDeclaration),
                    Nonterminal.Of(TokenType.Declarations)
                }, 33),
            new Production(
                Nonterminal.Of(TokenType.Declarations),
                new Sentence
                {
                    Nonterminal.Of(TokenType.FuncDeclaration),
                    Nonterminal.Of(TokenType.Declarations)
                }, 34),
            new Production(
                Nonterminal.Of(TokenType.Declarations),
                new Sentence
                {
                    Nonterminal.Of(TokenType.FuncDeclaration)
                }, 35),
            new Production(
                Nonterminal.Of(TokenType.VarDeclaration),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierConst),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.IdentifierType),
                    Terminal.Of(TokenType.Assignment),
                    Nonterminal.Of(TokenType.Literal),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 36),
            new Production(
                Nonterminal.Of(TokenType.VarDeclaration),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierVar),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.IdentifierType),
                    Terminal.Of(TokenType.Assignment),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 37),

            new Production(
                Nonterminal.Of(TokenType.FuncDeclaration),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFunc),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Parameters),
                    Terminal.Of(TokenType.ClosingRoundBracket),
                    Terminal.Of(TokenType.IdentifierType),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 38),
            new Production(
                Nonterminal.Of(TokenType.FuncDeclaration),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFunc),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Parameters),
                    Terminal.Of(TokenType.ClosingRoundBracket),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 39),
            new Production(
                Nonterminal.Of(TokenType.FuncDeclaration),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFunc),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Terminal.Of(TokenType.ClosingRoundBracket),
                    Terminal.Of(TokenType.IdentifierType),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 40),

            new Production(
                Nonterminal.Of(TokenType.FuncDeclaration),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFunc),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Terminal.Of(TokenType.ClosingRoundBracket),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 41),
            new Production(
                Nonterminal.Of(TokenType.Parameters),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.IdentifierType)

                }, 42),
            new Production(
                Nonterminal.Of(TokenType.Parameters),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.IdentifierType),
                    Terminal.Of(TokenType.Comma),
                    Nonterminal.Of(TokenType.Parameters)
                }, 43),

            // ------------------ IF ------------------

            new Production(
                Nonterminal.Of(TokenType.StatementIf),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierIf),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 44),
            new Production(
                Nonterminal.Of(TokenType.StatementIf),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierIf),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 45),

            new Production(
                Nonterminal.Of(TokenType.StatementIf),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierIf),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Nonterminal.Of(TokenType.ElseBrunch)
                }, 46),
            new Production(
                Nonterminal.Of(TokenType.StatementIf),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierIf),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Nonterminal.Of(TokenType.ElseBrunch)
                }, 47),
            new Production(
                Nonterminal.Of(TokenType.ElseBrunch),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierElse),
                    Nonterminal.Of(TokenType.StatementIf)
                }, 48),
            new Production(
                Nonterminal.Of(TokenType.ElseBrunch),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierElse),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 49),
            new Production(
                Nonterminal.Of(TokenType.ElseBrunch),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierElse),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 50),

            // ----------------- FOR ------------------

            new Production(
                Nonterminal.Of(TokenType.StatementFor),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFor),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 51),
            new Production(
                Nonterminal.Of(TokenType.StatementFor),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFor),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.ShortAssignment),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Semicolon),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Semicolon),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.Assignment),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 52),
            new Production(
                Nonterminal.Of(TokenType.StatementFor),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFor),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 53),
            new Production(
                Nonterminal.Of(TokenType.StatementFor),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFor),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.ShortAssignment),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Semicolon),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Semicolon),
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.Assignment),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 54),

            // ---------------- SWITCH ----------------

            new Production(
                Nonterminal.Of(TokenType.StatementSwitch),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierSwitch),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.CaseClause),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }, 55),
            new Production(
                Nonterminal.Of(TokenType.CaseClause),
                new Sentence
                {
                    Nonterminal.Of(TokenType.CaseBrunches),
                    Terminal.Of(TokenType.IdentifierDefault),
                    Terminal.Of(TokenType.Colon),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements)
                }, 56),
            new Production(
                Nonterminal.Of(TokenType.CaseClause),
                new Sentence
                {
                    Nonterminal.Of(TokenType.CaseBrunches)
                }, 57),
            new Production(
                Nonterminal.Of(TokenType.CaseBrunches),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierCase),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Colon),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements)
                }, 58),
            new Production(
                Nonterminal.Of(TokenType.CaseBrunches),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierCase),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Colon),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements),
                    Nonterminal.Of(TokenType.CaseBrunches)
                }, 59),

            // --------------- ЛИТЕРАЛЫ ---------------

            new Production(
                Nonterminal.Of(TokenType.Literal),
                new Sentence
                {
                    Terminal.Of(TokenType.BoolLiteral)
                }, 60),
            new Production(
                Nonterminal.Of(TokenType.Literal),
                new Sentence
                {
                    Terminal.Of(TokenType.FloatLiteral)
                }, 61),
            new Production(
                Nonterminal.Of(TokenType.Literal),
                new Sentence
                {
                    Terminal.Of(TokenType.IntLiteral)
                }, 62),
            new Production(
                Nonterminal.Of(TokenType.Literal),
                new Sentence
                {
                    Terminal.Of(TokenType.StringLiteral)
                }, 63)
            };
            /*foreach (var letter in new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" })
            {
                productions.Add(
                    new Production(Nonterminal.Of(TokenType.Letter), new Sentence {
                        Terminal.Of(letter),
                    })
                );
            }
            foreach (var digit in new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" })
            {
                productions.Add(
                    new Production(Nonterminal.Of(TokenType.Digit), new Sentence {
                        Terminal.Of(digit),
                    })
                );
            }*/

            this.start = Nonterminal.Of(TokenType.Axiom);
            this.productions = productions;

            //FillNtTable();
            ntTable = BuildTable(
                () => productions,
                (p) => p.LeftSide,
                (p) => p,
                () => new List<Production>(),
                (x, y) => x.Add(y)
            );
            
        }

        public void Print()
        {
            foreach(Production p in productions) {
                Console.Out.WriteLine(p.ToString());
            }
        }

        private void FillNtTable()
        {
            foreach(Production pr in productions) {
                Nonterminal nt = pr.LeftSide;
                List<Production> list;
                if(!ntTable.TryGetValue(nt, out list)) {
                    list = new List<Production>();
                    ntTable[nt] = list;
                }
                list.Add(pr);

            }
        }

        private Dictionary<TKey, TValue> BuildTable<TKey, TValue, T2, TElm>(
            Func<IEnumerable<TElm>> getInputListOfElements,
            Func<TElm, TKey> getKeyFromElement,
            Func<TElm, T2> getValueFromElement,
            Func<TValue> newEnumerable,
            Action<TValue, T2> updateStored
        ) where TValue : class
        {
            var table = new Dictionary<TKey, TValue>();
            foreach(var production in getInputListOfElements()) {
                var key = getKeyFromElement(production);
                var value = getValueFromElement(production);
                TValue result;
                if(!table.TryGetValue(key, out result)) {
                    result = newEnumerable();
                    table[key] = result;
                }
                updateStored(result, value);
            }
            return table;
        }

        int GetProductionNumber(Production pr)
        {
            for (int i = 0; i < productions.Count; i++) {
                if (productions[i].Equals(pr))
                    return i + 1;
            }
            return 0;
        }

        public List<Production> GetRulesForNt(Nonterminal nt)
        {
            List<Production> rules;
            ntTable.TryGetValue(nt, out rules);

            return rules;
        }

        public Nonterminal getAxiom()
        {
            return start;
        }

        public Grammar()
        {
            InitGrammar();
        }
    }
}