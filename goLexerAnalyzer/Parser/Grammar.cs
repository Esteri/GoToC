
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
        
        // çàäàåì êàæäóþ ïðîäóêöèþ îäíîñòðî÷íî â âèäå A -> B
        private void InitGrammar()
        {
            List<Production> productions = new List<Production> {

            new Production(
                Nonterminal.Of(TokenType.Axiom),
                new Sentence
                {
                    //Terminal.Of(TokenType.Import),
                    //Terminal.Of(TokenType.Fmt),
                    //Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Declarations),
                    //Terminal.Eof
                }),

            // --------------- ÂÛÐÀÆÅÍÈß ---------------

            new Production(
                Nonterminal.Of(TokenType.Arguments),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand1lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Arguments),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Comma),
                    Nonterminal.Of(TokenType.Arguments)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand1lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand2lvl),
                    Terminal.Of(TokenType.LogicalOr),
                    Nonterminal.Of(TokenType.Operand1lvl)
                }),
            new Production(
                Nonterminal.Of(TokenType.Operand1lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand2lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand2lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand3lvl),
                    Terminal.Of(TokenType.LogicalAnd),
                    Nonterminal.Of(TokenType.Operand2lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand2lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand3lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand4lvl),
                    Terminal.Of(TokenType.Comparison),
                    Nonterminal.Of(TokenType.Operand4lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Literal)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.LogicalNegation),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand3lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand4lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand4lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand5lvl),
                    Terminal.Of(TokenType.Addition),
                    Nonterminal.Of(TokenType.Operand4lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand4lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand5lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand5lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand6lvl),
                    Terminal.Of(TokenType.Multiplication),
                    Nonterminal.Of(TokenType.Operand5lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand5lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Operand6lvl)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Arguments),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Literal)
                }),

            new Production(
                Nonterminal.Of(TokenType.Operand6lvl),
                new Sentence
                {
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Operand4lvl),
                    Terminal.Of(TokenType.ClosingRoundBracket)
                }),

            // --------------- ÂÛÑÊÀÇÛÂÀÍÈß ---------------

            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.VarDeclaration)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.StatementIf)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.StatementSwitch)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.StatementFor)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierReturn),
                    Terminal.Of(TokenType.EndOfStatement),
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierReturn),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.EndOfStatement)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.Assignment),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.EndOfStatement)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Terminal.Of(TokenType.ClosingRoundBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.OpenningRoundBracket),
                    Nonterminal.Of(TokenType.Arguments),
                    Terminal.Of(TokenType.ClosingRoundBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statements),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Statement)
                }),
            new Production(
                Nonterminal.Of(TokenType.Statement),
                new Sentence
                {
                    Nonterminal.Of(TokenType.Statement),
                    Nonterminal.Of(TokenType.Statements),
                }),

            // --------------- ÎÁÚßÂËÅÍÈß ---------------

            new Production(
                Nonterminal.Of(TokenType.Declarations),
                new Sentence
                {
                    Nonterminal.Of(TokenType.VarDeclaration),
                    Nonterminal.Of(TokenType.Declarations)
                }),
            new Production(
                Nonterminal.Of(TokenType.Declarations),
                new Sentence
                {
                    Nonterminal.Of(TokenType.FuncDeclaration),
                    Nonterminal.Of(TokenType.Declarations)
                }),
            new Production(
                Nonterminal.Of(TokenType.Declarations),
                new Sentence
                {
                    Nonterminal.Of(TokenType.FuncDeclaration)
                }),
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
                }),
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
                }),

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
                }),
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
                }),
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
                }),

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
                }),
            new Production(
                Nonterminal.Of(TokenType.Parameters),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.IdentifierType)

                }),
            new Production(
                Nonterminal.Of(TokenType.Parameters),
                new Sentence
                {
                    Terminal.Of(TokenType.Identifier),
                    Terminal.Of(TokenType.IdentifierType),
                    Terminal.Of(TokenType.Comma),
                    Nonterminal.Of(TokenType.Parameters)
                }),

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
                }),
            new Production(
                Nonterminal.Of(TokenType.StatementIf),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierIf),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }),

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
                }),
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
                }),
            new Production(
                Nonterminal.Of(TokenType.ElseBrunch),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierElse),
                    Nonterminal.Of(TokenType.StatementIf)
                }),
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
                }),
            new Production(
                Nonterminal.Of(TokenType.ElseBrunch),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierIf),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }),

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
                }),
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
                }),
            new Production(
                Nonterminal.Of(TokenType.StatementFor),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierFor),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.OpenningCurlyBracket),
                    Terminal.Of(TokenType.ClosingCurlyBracket),
                    Terminal.Of(TokenType.EndOfStatement)
                }),
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
                }),

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
                }),
            new Production(
                Nonterminal.Of(TokenType.CaseClause),
                new Sentence
                {
                    Nonterminal.Of(TokenType.CaseBrunches),
                    Terminal.Of(TokenType.IdentifierDefault),
                    Terminal.Of(TokenType.Colon),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements)
                }),
            new Production(
                Nonterminal.Of(TokenType.CaseClause),
                new Sentence
                {
                    Nonterminal.Of(TokenType.CaseBrunches)
                }),
            new Production(
                Nonterminal.Of(TokenType.CaseBrunches),
                new Sentence
                {
                    Terminal.Of(TokenType.IdentifierCase),
                    Nonterminal.Of(TokenType.Operand1lvl),
                    Terminal.Of(TokenType.Colon),
                    Terminal.Of(TokenType.EndOfStatement),
                    Nonterminal.Of(TokenType.Statements)
                }),
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
                }),

            // --------------- ËÈÒÅÐÀËÛ ---------------

            new Production(
                Nonterminal.Of(TokenType.Literal),
                new Sentence
                {
                    Terminal.Of(TokenType.BoolLiteral)
                }),
            new Production(
                Nonterminal.Of(TokenType.Literal),
                new Sentence
                {
                    Terminal.Of(TokenType.FloatLiteral)
                }),
            new Production(
                Nonterminal.Of(TokenType.Literal),
                new Sentence
                {
                    Terminal.Of(TokenType.IntLiteral)
                }),
            new Production(
                Nonterminal.Of(TokenType.Literal),
                new Sentence
                {
                    Terminal.Of(TokenType.StringLiteral)
                })
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