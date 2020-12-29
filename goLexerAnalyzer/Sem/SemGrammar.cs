using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goLexerAnalyzer
{
    public class Rule
    {
        public TokenType antecedens;
        public List<TokenType> consequens;

        public Rule(TokenType antecedens, List<TokenType> consequens)
        {
            this.antecedens = antecedens;
            this.consequens = consequens;
        }
    }
    public class SemantGrammar
    {
        public List<Rule> rules;
        public TokenType axiom = TokenType.Axiom;

        public SemantGrammar()
        {
            List<TokenType> curConsequens = new List<TokenType>();
            TokenType curAntecedens = new TokenType();
            List<Rule> rulesBuf= new List<Rule>();

            //0
            curAntecedens = TokenType.Axiom;
            curConsequens.Add(TokenType.Import); curConsequens.Add(TokenType.Fmt); curConsequens.Add(TokenType.EndOfStatement); curConsequens.Add(TokenType.Declarations);            
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

        // --------------- ВЫРАЖЕНИЯ ---------------

            //1
            curAntecedens = TokenType.Arguments;
            curConsequens.Add(TokenType.Operand1lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //2
            curAntecedens = TokenType.Arguments;
            curConsequens.Add(TokenType.Operand1lvl); curConsequens.Add(TokenType.Comma); curConsequens.Add(TokenType.Arguments);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //3
            curAntecedens = TokenType.Operand1lvl;
            curConsequens.Add(TokenType.Operand2lvl); curConsequens.Add(TokenType.Comma); curConsequens.Add(TokenType.Arguments);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //4
            curAntecedens = TokenType.Operand1lvl;
            curConsequens.Add(TokenType.Operand2lvl); 
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //5
            curAntecedens = TokenType.Operand2lvl;
            curConsequens.Add(TokenType.Operand2lvl); curConsequens.Add(TokenType.LogicalAnd); curConsequens.Add(TokenType.Operand2lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //6
            curAntecedens = TokenType.Operand2lvl;
            curConsequens.Add(TokenType.Operand3lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //7
            curAntecedens = TokenType.Operand3lvl;
            curConsequens.Add(TokenType.Operand4lvl); curConsequens.Add(TokenType.Comparison); curConsequens.Add(TokenType.Operand4lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //8
            curAntecedens = TokenType.Operand3lvl;
            curConsequens.Add(TokenType.Literal);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //9
            curAntecedens = TokenType.Operand3lvl;
            curConsequens.Add(TokenType.OpenningRoundBracket); curConsequens.Add(TokenType.Operand1lvl); curConsequens.Add(TokenType.ClosingRoundBracket);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //10
            curAntecedens = TokenType.Operand3lvl;
            curConsequens.Add(TokenType.LogicalNegation); curConsequens.Add(TokenType.OpenningRoundBracket); curConsequens.Add(TokenType.Operand1lvl); curConsequens.Add(TokenType.ClosingRoundBracket);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //11
            curAntecedens = TokenType.Operand3lvl;
            curConsequens.Add(TokenType.Operand4lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //12
            curAntecedens = TokenType.Operand3lvl;
            curConsequens.Add(TokenType.Operand4lvl); curConsequens.Add(TokenType.Comparison); curConsequens.Add(TokenType.Operand4lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //13
            curAntecedens = TokenType.Operand4lvl;
            curConsequens.Add(TokenType.Operand5lvl); curConsequens.Add(TokenType.Addition); curConsequens.Add(TokenType.Operand4lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //14
            curAntecedens = TokenType.Operand4lvl;
            curConsequens.Add(TokenType.Operand5lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //15
            curAntecedens = TokenType.Operand4lvl;
            curConsequens.Add(TokenType.Operand5lvl); curConsequens.Add(TokenType.Multiplication); curConsequens.Add(TokenType.Operand5lvl); 
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //16
            curAntecedens = TokenType.Operand5lvl;
            curConsequens.Add(TokenType.Operand6lvl);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //17
            curAntecedens = TokenType.Operand6lvl;
            curConsequens.Add(TokenType.Identifier);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //18
            curAntecedens = TokenType.Operand6lvl;
            curConsequens.Add(TokenType.Identifier);  curConsequens.Add(TokenType.OpenningRoundBracket); curConsequens.Add(TokenType.ClosingRoundBracket);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //19
            curAntecedens = TokenType.Operand6lvl;
            curConsequens.Add(TokenType.Identifier); curConsequens.Add(TokenType.OpenningRoundBracket); curConsequens.Add(TokenType.Arguments); curConsequens.Add(TokenType.ClosingRoundBracket);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //20
            curAntecedens = TokenType.Operand6lvl;
            curConsequens.Add(TokenType.Literal);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //21
            curAntecedens = TokenType.Operand6lvl;
            curConsequens.Add(TokenType.OpenningRoundBracket); curConsequens.Add(TokenType.Operand4lvl); curConsequens.Add(TokenType.ClosingRoundBracket);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

        // --------------- ВЫСКАЗЫВАНИЯ ---------------

            //22
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.VarDeclaration);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //23
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.StatementIf);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //24
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.StatementSwitch);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //25
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.StatementFor);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();


            //26
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.IdentifierReturn); curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //27
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.IdentifierReturn); curConsequens.Add(TokenType.Operand1lvl); curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //28
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.Identifier); curConsequens.Add(TokenType.Assignment); curConsequens.Add(TokenType.Operand1lvl); curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //29
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.Identifier); curConsequens.Add(TokenType.OpenningRoundBracket); curConsequens.Add(TokenType.ClosingRoundBracket); curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //30
            curAntecedens = TokenType.Statement;
            curConsequens.Add(TokenType.Identifier); curConsequens.Add(TokenType.OpenningRoundBracket); curConsequens.Add(TokenType.Arguments); curConsequens.Add(TokenType.ClosingRoundBracket); curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //31
            curAntecedens = TokenType.Statements;
            curConsequens.Add(TokenType.Statement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //32
            curAntecedens = TokenType.Statements;
            curConsequens.Add(TokenType.Statement); curConsequens.Add(TokenType.Statements);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

        // --------------- ОБЪЯВЛЕНИЯ ---------------

            //33
            curAntecedens = TokenType.Declarations;
            curConsequens.Add(TokenType.VarDeclaration); curConsequens.Add(TokenType.Declarations);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //34
            curAntecedens = TokenType.Declarations;
            curConsequens.Add(TokenType.FuncDeclaration); curConsequens.Add(TokenType.Declarations);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //35
            curAntecedens = TokenType.Declarations;
            curConsequens.Add(TokenType.FuncDeclaration);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //36
            curAntecedens = TokenType.VarDeclaration;
            curConsequens.Add(TokenType.IdentifierVar); curConsequens.Add(TokenType.Identifier); curConsequens.Add(TokenType.IdentifierType); curConsequens.Add(TokenType.Assignment); curConsequens.Add(TokenType.Operand1lvl); curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //37
            curAntecedens = TokenType.VarDeclaration;
            curConsequens.Add(TokenType.IdentifierConst); curConsequens.Add(TokenType.Identifier); curConsequens.Add(TokenType.IdentifierType); curConsequens.Add(TokenType.Assignment); curConsequens.Add(TokenType.Operand1lvl); curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //38
            curAntecedens = TokenType.FuncDeclaration;
            curConsequens.Add(TokenType.IdentifierFunc); curConsequens.Add(TokenType.Identifier); curConsequens.Add(TokenType.OpenningRoundBracket); curConsequens.Add(TokenType.Parameters); curConsequens.Add(TokenType.ClosingRoundBracket); curConsequens.Add(TokenType.IdentifierType); curConsequens.Add(TokenType.OpenningCurlyBracket); curConsequens.Add(TokenType.EndOfStatement); curConsequens.Add(TokenType.Statements); curConsequens.Add(TokenType.ClosingCurlyBracket); curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //39
            curAntecedens = TokenType.FuncDeclaration;
            curConsequens.Add(TokenType.IdentifierFunc); 
            curConsequens.Add(TokenType.Identifier); 
            curConsequens.Add(TokenType.OpenningRoundBracket); 
            curConsequens.Add(TokenType.Parameters); 
            curConsequens.Add(TokenType.ClosingRoundBracket); 
            curConsequens.Add(TokenType.OpenningCurlyBracket); 
            curConsequens.Add(TokenType.EndOfStatement); 
            curConsequens.Add(TokenType.Statements); 
            curConsequens.Add(TokenType.ClosingCurlyBracket); 
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //40
            curAntecedens = TokenType.FuncDeclaration;
            curConsequens.Add(TokenType.IdentifierFunc);
            curConsequens.Add(TokenType.Identifier);
            curConsequens.Add(TokenType.OpenningRoundBracket);
            curConsequens.Add(TokenType.ClosingRoundBracket);
            curConsequens.Add(TokenType.IdentifierType);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //41
            curAntecedens = TokenType.FuncDeclaration;
            curConsequens.Add(TokenType.IdentifierFunc);
            curConsequens.Add(TokenType.Identifier);
            curConsequens.Add(TokenType.OpenningRoundBracket);
            curConsequens.Add(TokenType.ClosingRoundBracket);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //42
            curAntecedens = TokenType.Parameters;
            curConsequens.Add(TokenType.Identifier);
            curConsequens.Add(TokenType.IdentifierType);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //43
            curAntecedens = TokenType.Parameters;
            curConsequens.Add(TokenType.Identifier);
            curConsequens.Add(TokenType.IdentifierType);
            curConsequens.Add(TokenType.Comma);
            curConsequens.Add(TokenType.Parameters);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

        // ------------------ IF ------------------

            //44
            curAntecedens = TokenType.StatementIf;
            curConsequens.Add(TokenType.IdentifierIf);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //45
            curAntecedens = TokenType.StatementIf;
            curConsequens.Add(TokenType.IdentifierIf);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //46
            curAntecedens = TokenType.StatementIf;
            curConsequens.Add(TokenType.IdentifierIf);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.ElseBrunch);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //47
            curAntecedens = TokenType.StatementIf;
            curConsequens.Add(TokenType.IdentifierIf);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.ElseBrunch);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //48
            curAntecedens = TokenType.ElseBrunch;
            curConsequens.Add(TokenType.IdentifierElse);
            curConsequens.Add(TokenType.StatementIf);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //49
            curAntecedens = TokenType.ElseBrunch;
            curConsequens.Add(TokenType.IdentifierElse);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //50
            curAntecedens = TokenType.ElseBrunch;
            curConsequens.Add(TokenType.IdentifierElse);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

        // ----------------- FOR ------------------

            //51
            curAntecedens = TokenType.StatementFor;
            curConsequens.Add(TokenType.IdentifierFor);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //52
            curAntecedens = TokenType.StatementFor;
            curConsequens.Add(TokenType.IdentifierFor);
            curConsequens.Add(TokenType.Identifier);
            curConsequens.Add(TokenType.ShortAssignment);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.Semicolon);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.Semicolon);
            curConsequens.Add(TokenType.Identifier);
            curConsequens.Add(TokenType.Assignment);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //53
            curAntecedens = TokenType.StatementFor;
            curConsequens.Add(TokenType.IdentifierFor);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //54
            curAntecedens = TokenType.StatementFor;
            curConsequens.Add(TokenType.IdentifierFor);
            curConsequens.Add(TokenType.Identifier);
            curConsequens.Add(TokenType.ShortAssignment);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.Semicolon);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.Semicolon);
            curConsequens.Add(TokenType.Identifier);
            curConsequens.Add(TokenType.Assignment);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            // ---------------- SWITCH ----------------

            //55
            curAntecedens = TokenType.StatementSwitch;
            curConsequens.Add(TokenType.IdentifierSwitch);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.OpenningCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.CaseClause);
            curConsequens.Add(TokenType.ClosingCurlyBracket);
            curConsequens.Add(TokenType.EndOfStatement);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //56
            curAntecedens = TokenType.CaseClause;
            curConsequens.Add(TokenType.CaseBrunches);
            curConsequens.Add(TokenType.IdentifierDefault);
            curConsequens.Add(TokenType.Colon);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //57
            curAntecedens = TokenType.CaseClause;
            curConsequens.Add(TokenType.CaseBrunches);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //58
            curAntecedens = TokenType.CaseBrunches;
            curConsequens.Add(TokenType.IdentifierCase);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.Colon);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //59
            curAntecedens = TokenType.CaseBrunches;
            curConsequens.Add(TokenType.IdentifierCase);
            curConsequens.Add(TokenType.Operand1lvl);
            curConsequens.Add(TokenType.Colon);
            curConsequens.Add(TokenType.EndOfStatement);
            curConsequens.Add(TokenType.Statements);
            curConsequens.Add(TokenType.CaseBrunches);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))) ; curConsequens.Clear();

            // --------------- ЛИТЕРАЛЫ ---------------

            //60
            curAntecedens = TokenType.Literal;
            curConsequens.Add(TokenType.BoolLiteral);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //61
            curAntecedens = TokenType.Literal;
            curConsequens.Add(TokenType.FloatLiteral);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //62
            curAntecedens = TokenType.Literal;
            curConsequens.Add(TokenType.IntLiteral);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            //63
            curAntecedens = TokenType.Literal;
            curConsequens.Add(TokenType.StringLiteral);
            rulesBuf.Add(new Rule(curAntecedens, curConsequens.GetRange(0, curConsequens.Count))); curConsequens.Clear();

            this.rules = rulesBuf;
        }
    }
}
