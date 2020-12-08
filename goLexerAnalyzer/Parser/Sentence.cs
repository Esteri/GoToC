using System.Collections;
using System.Collections.Generic;

namespace goLexerAnalyzer
{
    public class Sentence : IEnumerable<Symbol>
    {
        private List<Symbol> _sentence;

        public Sentence()
        {
            _sentence = new List<Symbol>();
        }

        public Sentence(IEnumerable<Symbol> l)
        {
            _sentence = new List<Symbol>(l);
        }
        public IEnumerator<Symbol> GetEnumerator()
        {
            return _sentence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sentence.GetEnumerator();
        }

        public void Add(Symbol item)
        {
            _sentence.Add(item);
        }

        public override string ToString()
        {
            string sentence = "";
            foreach(var word in _sentence) {
                //Symbol unit.IsNonterminal)
                sentence += ("<" + word.Name.ToString() + "> ");
            }
            return sentence;
        }

        public int Count
        {
            get {
                return _sentence.Count;
            }
        }

        public bool Equals(Sentence other)
        {
            // Would still want to check for null etc. first.
            for (int i = 0; i < _sentence.Count; i++) {
                if(_sentence[i] != other[i])
                    return false;
            }
            return true;
        }

        public Symbol this[int index]
        {
            get {
                return _sentence[index];
            }
            set {
                _sentence[index] = value;
            }
        }
    }
}