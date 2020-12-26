using System;
using System.Collections.Generic;
using System.Text;

namespace goLexerAnalyzer
{
    class Generator
    {
        private List<Token> tokens;
        private Grammar grammar;
        private List<int> rules;
        private int tokenPos;
        private int rulePos;

        public string GenCppSource(List<Token> t, List<int> r)
        {
            tokens = t;
            rules = r;

            tokenPos = 0;
            rulePos = 0;

            return GetSrc(); // rules are read from the end
        }

        private string GetSrc()
        {
            Console.Out.WriteLine("Current token = " + tokens[tokenPos]);
            Console.Out.WriteLine("Current rulepos = " + rulePos);
            Console.Out.WriteLine("Current rule = " + rules[rulePos] + "\n//-------------------------------");

            switch (rules[rulePos])
            {
                case 0:
                    {
                        // done
                        tokenPos += 3; // skip 'format fmt <EndOfStatement>'
                        rulePos++;
                        return string.Format("#include <iostream>\n#include <string>\n{0}", GetSrc());
                    }
                case 1:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 2:
                    {
                        // done
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos++; // skip ','
                        rulePos++;
                        string ntArgs = GetSrc();
                        return string.Format("{0}, {1}", ntOp1, ntArgs);
                    }
                case 3:
                    {
                        // done
                        rulePos++;
                        string ntOp2 = GetSrc();
                        tokenPos++; // skip '||'
                        rulePos++;
                        string ntOp1 = GetSrc();
                        return string.Format("{0} || {1}", ntOp2, ntOp1);
                    }
                case 4:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 5:
                    {
                        // done
                        rulePos++;
                        string ntOp3 = GetSrc();
                        tokenPos++; // skip '&&'
                        rulePos++;
                        string ntOp2 = GetSrc();
                        return string.Format("{0} && {1}", ntOp3, ntOp2);
                    }
                case 6:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 7:
                case 12:
                    {
                        // done
                        rulePos++;
                        string ntOp4_1 = GetSrc();
                        string tComp = tokens[tokenPos++].Lexem;
                        rulePos++;
                        string ntOp4_2 = GetSrc();
                        return string.Format("{0} {1} {2}", ntOp4_1, tComp, ntOp4_2);
                    }
                case 8:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 9:
                    {
                        // done
                        tokenPos++; // skip '('
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos++; // skip ')'
                        return string.Format("({0})", ntOp1);
                    }
                case 10:
                    {
                        // done
                        tokenPos += 2; // skip '!('
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos++; // skip ')'
                        return string.Format("!({0})", ntOp1);
                    }
                case 11:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 13:
                    {
                        // done
                        rulePos++;
                        string ntOp5 = GetSrc();
                        string tAdd = tokens[tokenPos++].Lexem;
                        rulePos++;
                        string ntOp4 = GetSrc();
                        return string.Format("{0} {1} {2}", ntOp5, tAdd, ntOp4);
                    }
                case 14:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 15:
                    {
                        // done
                        rulePos++;
                        string ntOp6 = GetSrc();
                        string tMul = tokens[tokenPos++].Lexem;
                        rulePos++;
                        string ntOp5 = GetSrc();
                        return string.Format("{0} {1} {2}", ntOp6, tMul, ntOp5);
                    }
                case 16:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 17:
                    {
                        // done
                        return tokens[tokenPos++].Lexem;
                    }
                case 18:
                    {
                        // done
                        string id = tokens[tokenPos++].Lexem;
                        tokenPos += 2; // skip '()'
                        return id + "()";
                    }
                case 19:
                    {
                        // done
                        string id = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip '('
                        rulePos++;
                        string args = GetSrc();
                        tokenPos++; // skip ')'
                        return string.Format("{0}({1})", id, args);
                    }
                case 20:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 21:
                    {
                        // done
                        tokenPos++; // skip '('
                        rulePos++;
                        string ntOp4 = GetSrc();
                        tokenPos++; // skip '('
                        return string.Format("({0})", ntOp4);
                    }
                case 22:
                case 23:
                case 24:
                case 25:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 26:
                    {
                        // done
                        tokenPos += 2; // skip 'return <EndOfStatement>'
                        return "return;\n";
                    }
                case 27:
                    {
                        // done
                        tokenPos++; // skip 'return'
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos++; // skip '<EndOfStatement>'
                        return string.Format("return {0};\n", ntOp1);
                    }
                case 28:
                    {
                        // done
                        string id = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip '='
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos++; // skip '<EndOfStatement>'
                        return string.Format("{0} = {1};\n", id, ntOp1);
                    }
                case 29:
                    {
                        // done
                        string id = tokens[tokenPos++].Lexem;
                        tokenPos += 3; // skip '() <EndOfStatement>''
                        return string.Format("{0}();\n", id);
                    }
                case 30:
                    {
                        // done
                        string id = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip '('
                        rulePos++;
                        string args = GetSrc();
                        tokenPos += 2; // skip ') <EndOfStatement>'
                        return string.Format("{0}({1});\n", id, args);
                    }
                case 31:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 32:
                    {
                        // done
                        rulePos++;
                        string statement = GetSrc();
                        rulePos++;
                        string statements = GetSrc();
                        return statement + statements;
                    }
                case 33:
                    {
                        // done
                        rulePos++;
                        string ntVarDecl = GetSrc();
                        rulePos++;
                        string ntDecls = GetSrc();
                        return ntVarDecl + ntDecls;
                    }
                case 34:
                    {
                        // done
                        rulePos++;
                        string ntFuncDecl = GetSrc();
                        rulePos++;
                        string ntDecls = GetSrc();
                        return ntFuncDecl + ntDecls;
                    }
                case 35:
                    {
                        // done
                        rulePos++;
                        return GetSrc();
                    }
                case 36:
                    {
                        // done
                        tokenPos++; // skip idConst
                        string id = tokens[tokenPos++].Lexem;
                        string type = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip Assignment
                        rulePos++;
                        string operand = GetSrc();
                        tokenPos++; // skip <EndOfStatement>
                        return string.Format("const {0} {1} = {2};\n", type, id, operand);
                    }
                case 37:
                    {
                        // done
                        tokenPos++; // skip idVar
                        string id = tokens[tokenPos++].Lexem;
                        string type = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip Assignment
                        rulePos++;
                        string ntOperand = GetSrc();
                        tokenPos++; // skip <EndOfStatement>
                        return string.Format("{0} {1} = {2};\n", type, id, ntOperand);
                    }
                case 38:
                    {
                        // done
                        tokenPos++; // skip IdentifierFunc
                        string funcId = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip '('
                        rulePos++;
                        string ntParam = GetSrc();
                        tokenPos++; // skip ')'
                        string type = tokens[tokenPos++].Lexem;
                        tokenPos += 2; // skip '{ <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("{0} {1}({2}) {{\n{3}}}\n", type, funcId, ntParam, ntStatements);
                    }
                case 39:
                    {
                        // done
                        tokenPos++; // skip IdentifierFunc
                        string funcId = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip '('
                        rulePos++;
                        string ntParam = GetSrc();
                        tokenPos++; // skip ')'
                        tokenPos += 2; // skip ') { <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("void {0}({1}) {{\n{2}}}\n", funcId, ntParam, ntStatements);
                    }
                case 40:
                    {
                        // done
                        tokenPos++; // skip IdentifierFunc
                        string funcId = tokens[tokenPos++].Lexem;
                        tokenPos += 2; // skip '()'
                        string type = tokens[tokenPos++].Lexem;
                        tokenPos += 2; // skip '{ <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("{0} {1}() {{\n{2}}}\n", type, funcId, ntStatements);
                    }
                case 41:
                    {
                        // done
                        tokenPos++; // skip IdentifierFunc
                        string funcId = tokens[tokenPos++].Lexem;
                        tokenPos += 4; // skip '() { <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("void {0}() {{\n{1}}}\n", funcId, ntStatements);
                    }
                case 42:
                    {
                        // done
                        string id = tokens[tokenPos++].Lexem;
                        string type = tokens[tokenPos++].Lexem;
                        return string.Format("{0} {1}", type, id);
                    }
                case 43:
                    {
                        // done
                        string id = tokens[tokenPos++].Lexem;
                        string type = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip ','
                        rulePos++;
                        string ntParam = GetSrc();
                        return string.Format("{0} {1}, {2}", type, id, ntParam);
                    }
                case 44:
                    {
                        // done
                        tokenPos++; // skip 'if'
                        rulePos++;
                        string ntId1 = GetSrc();
                        tokenPos += 2; // skip '{ <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("if ({0}) {{\n{1}}}\n", ntId1, ntStatements);
                    }
                case 45:
                    {
                        // done
                        tokenPos++; // skip 'if'
                        rulePos++;
                        string ntId1 = GetSrc();
                        tokenPos += 3; // skip '{} <EndOfStatement>'
                        return string.Format("if ({0}) {{}}\n", ntId1);
                    }
                case 46:
                    {
                        // done
                        tokenPos++; // skip 'if'
                        rulePos++;
                        string ntId1 = GetSrc();
                        tokenPos += 2; // skip '{ <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos++; // skip '}'
                        rulePos++;
                        string ntElse = GetSrc();
                        return string.Format("if ({0}) {{\n{1}}} {2}", ntId1, ntStatements, ntElse);
                    }
                case 47:
                    {
                        // done
                        tokenPos++; // skip 'if'
                        rulePos++;
                        string ntId1 = GetSrc();
                        tokenPos += 3; // skip '{ <EndOfStatement> }'
                        rulePos++;
                        string ntElse = GetSrc();
                        return string.Format("if ({0}) {{\n}} {1}", ntId1, ntElse);
                    }
                case 48:
                    {
                        // done
                        tokenPos++; // skip 'else'
                        rulePos++;
                        string ntStIf = GetSrc();
                        return string.Format("else {0}", ntStIf);
                    }
                case 49:
                    {
                        // done
                        tokenPos += 3; // skip 'else { <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("else {{\n{0}}}\n", ntStatements);
                    }
                case 50:
                    {
                        // done
                        tokenPos += 4; // skip 'else {} <EndOfStatement>''
                        return "else {}\n";
                    }
                case 51:
                    {
                        // done
                        tokenPos++; // skip 'for'
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos += 2; // skip '{ <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("while ({0}) {{\n{1}}}\n", ntOp1, ntStatements);
                    }
                case 52:
                    {
                        // done
                        tokenPos++; // skip 'for'
                        string id_1 = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip ':='
                        rulePos++;
                        string ntOp1_1 = GetSrc();
                        tokenPos++; // skip ';'
                        rulePos++;
                        string ntOp1_2 = GetSrc();
                        tokenPos++; // skip ';'
                        string id_2 = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip '='
                        rulePos++;
                        string ntOp1_3 = GetSrc();
                        tokenPos += 2; // skip '{ <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("for (int {0} = {1}; {2}; {3} = {4}) {{\n{5}}}\n",
                            id_1,
                            ntOp1_1,
                            ntOp1_2,
                            id_2,
                            ntOp1_3,
                            ntStatements);
                    }
                case 53:
                    {
                        // done
                        tokenPos++; // skip 'for'
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos += 3; // skip '{} <EndOfStatement>'
                        return string.Format("while ({0}) {{}}\n", ntOp1);
                    }
                case 54:
                    {
                        // !done
                        tokenPos++; // skip 'for'
                        string id_1 = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip ':='
                        rulePos++;
                        string ntOp1_1 = GetSrc();
                        tokenPos++; // skip ';'
                        rulePos++;
                        string ntOp1_2 = GetSrc();
                        tokenPos++; // skip ';'
                        string id_2 = tokens[tokenPos++].Lexem;
                        tokenPos++; // skip '='
                        rulePos++;
                        string ntOp1_3 = GetSrc();
                        tokenPos += 3; // skip '{} <EndOfStatement>'
                        return string.Format("for (int {0} = {1}; {2}; {3} = {4}) {{}}\n",
                            id_1,
                            ntOp1_1,
                            ntOp1_2,
                            id_2,
                            ntOp1_3);
                    }
                case 55:
                    {
                        tokenPos++; // skip 'swith'
                        rulePos++;
                        string ntOp1_1 = GetSrc();
                        tokenPos += 2; // skip '{ <EndOfStatement>'
                        rulePos++;
                        string ntCaseCaluse = GetSrc();
                        tokenPos += 2; // skip '} <EndOfStatement>'
                        return string.Format("switch ({0}) {{\n{1}}}\n", ntOp1_1, ntCaseCaluse);
                    }
                case 56:
                    {
                        rulePos++;
                        string ntCaseBrunches = GetSrc();
                        tokenPos += 3; // skip 'default: <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        return string.Format("{0}default:\n{1}", ntCaseBrunches, ntStatements);
                    }
                case 57:
                    {
                        rulePos++;
                        return GetSrc();
                    }
                case 58:
                    {
                        tokenPos++; // skip 'case'
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos += 2; // skip ': <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        return string.Format("case {0}:\n{1}break;\n", ntOp1, ntStatements);
                    }
                case 59:
                    {
                        tokenPos++; // skip 'case'
                        rulePos++;
                        string ntOp1 = GetSrc();
                        tokenPos += 2; // skip ': <EndOfStatement>'
                        rulePos++;
                        string ntStatements = GetSrc();
                        rulePos++;
                        string ntCaseBrunches = GetSrc();
                        return string.Format("case {0}:\n{1}break;\n{2}", 
                            ntOp1, 
                            ntStatements, 
                            ntCaseBrunches);
                    }
                case 60:
                case 61:
                case 62:
                    {
                        // done
                        return tokens[tokenPos++].Lexem;
                    }
                case 63:
                    {
                        // done
                        return string.Format("\"{0}\"", tokens[tokenPos++].Lexem);
                    }
                default:
                    return "ERROR";
            }
        }
    }
}
