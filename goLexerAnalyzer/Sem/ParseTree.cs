using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goLexerAnalyzer
{
    public class ParseTree
    {
        public SemantGrammar grammar = new SemantGrammar();
        public List<int> rulesList;
        public List<Token> slice = new List<Token>();

        public ParseTree(List<int> rulesList)
        {
            this.rulesList = rulesList;
            Token fstToken = new Token(grammar.axiom, "");
            slice.Add(fstToken);
        }

        /*public List<Token> NextSlice()
        {
            bool flag = false;
            int i = slice.Count()-1;
            while (flag == false)
            {                
                if (slice[i].Type == grammar.rules[rulesList[0]].antecedens)
                {
                    int j = i;
                    while(j < i + grammar.rules[rulesList[0]].consequens.Count())
                    {
                        Token newToken = new Token(grammar.rules[rulesList[0]].consequens[j - i], "");
                        slice.Insert(j, newToken);
                        j++;
                    }
                    slice.RemoveAt(j);
                    flag = true;
                }
                if (i > 0)
                    i--;
            }

            rulesList.RemoveAt(0);
            return slice;
        }*/

        public List<Token> NextSlice()
        {
            if (rulesList.Count > 0)
            {
                bool flag = false;
                while (flag == false)
                {
                    int i = 0;
                    while (i < slice.Count())
                    {
                        if (slice[i].Type == grammar.rules[rulesList[0]].antecedens)
                        {
                            int j = i;
                            while (j < i + grammar.rules[rulesList[0]].consequens.Count())
                            {
                                Token newToken = new Token(grammar.rules[rulesList[0]].consequens[j - i], "");
                                slice.Insert(j, newToken);
                                j++;
                            }
                            slice.RemoveAt(j);
                            rulesList.RemoveAt(0);
                            flag = true;
                            i = 0;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
            }            
            return slice;
        }

        public void CheckingExpressions()
        {
            Token expression = new Token(TokenType.Operand1lvl, "");
            List<Token> expressionList = new List<Token>();            
            expressionList.Add(expression);
            for (int l = 0; l < slice.Count; l++)
            {
                if (slice[l].Type == expression.Type)
                {
                    int i = 0;
                    while (i < expressionList.Count)
                    {
                        List<Token> Bufer = expressionList.GetRange(0, expressionList.Count);
                        for (int j = 0; j < rulesList.Count; j++)
                        {
                            if (expressionList[i].Type == grammar.rules[rulesList[j]].antecedens)
                            {
                                int k = 0;
                                while (k < grammar.rules[rulesList[j]].consequens.Count)
                                {
                                    Token newToken = new Token(grammar.rules[rulesList[j]].consequens[k], "");
                                    k++;
                                }
                            }
                            if (expressionList == Bufer)
                            {

                                i++;
                            }
                            else
                            {
                                i = 0;
                            }

                        }
                    }
                }
            }
            Console.Out.WriteLine();
            for (int i = 0; i<expressionList.Count; i++)
            {
                Console.Out.Write(expressionList[i]);
            }
        }
    }
}
