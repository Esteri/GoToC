using System;
using System.IO;

namespace goLexerAnalyzer
{
    public class Production : IEquatable<Production> {

        public Nonterminal LeftSide { get; }
        public Sentence RightSide { get; }
        

        public Production(Nonterminal left, Sentence right)
        {
            LeftSide = left;
            RightSide = right;
        }

        public bool Equals(Production other)
        {
            // Would still want to check for null etc. first.
            return this.LeftSide == other.LeftSide &&
                   this.RightSide == other.RightSide;
        }

        public override string ToString()
        {
            return string.Format("<{0}> -> {1}", LeftSide.Name, RightSide);
        }
    }
}