
using System;
using System.Collections.Generic;

namespace goLexerAnalyzer
{
	public class State
	{
		private readonly List<Item> _items;
		private readonly Dictionary<Item, Item> _seenItems;
		private readonly HashSet<Nonterminal> _nonterminalsPredicted;

		public bool Contains(Item item)
		{
			if (item == null) throw new ArgumentNullException();
			return _items.Contains(item);
		}

		public void Add(Item item)
		{
			if (item == null) throw new ArgumentNullException();
			if (!Contains(item)) _items.Add(item);
		}
		public State(Nonterminal predictionSeedNonterminal = null)
		{
			_items = new List<Item>();
			_nonterminalsPredicted = new HashSet<Nonterminal>();
			if (predictionSeedNonterminal != null)
			{
				_nonterminalsPredicted.Add(predictionSeedNonterminal);
			}
		}

		public int Count
		{
			get
			{
				return _items.Count;
			}
		}

		public Item this[int index]
		{
			get
			{
				return _items[index];
			}
			set
			{
				_items[index] = value;
			}
		}

		public List<Item>.Enumerator GetEnumerator()
		{
			return _items.GetEnumerator();
		}

        internal void Print()
        {
            foreach(Item item in _items) {
				Console.Out.WriteLine(item.ToString());
            }
        }

        public void Insert(Item item)
		{
			_seenItems[item] = item;
			_items.Add(item);
		}
	}
}