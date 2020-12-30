using System;

namespace goLexerAnalyzer
{
	public class Item : IEquatable<Item> {
		public Production Production;
		public int Pos;
		public int Table;
		
		// (правило, позиция точки, принадлежность таблице (номер))
		public Item(Production production, int pos, int table)
		{
			if (Pos < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			Production = production;
			Pos = pos;
			Table = table;
		}

		public Symbol PrevWord
		{
			get
			{
				if (Pos - 1 < 0)
				{
					return null;
				}
				if (Production.RightSide.Count == 0)
				{
					return null;
				}
				return Production.RightSide[Pos - 1];
			}
		}
		public Symbol NextWord
		{
			get
			{
				if (Pos >= Production.RightSide.Count)
				{
					return null;
				}
				return Production.RightSide[Pos];
			}
		}

		/*internal Symbol Symbol
		{
			get {
				if(IsComplete()) throw new InvalidOperationException();
				return Production.RightSide[Pos];
			}
		}*/

		public bool Equals(Item other)
		{
			// Would still want to check for null etc. first.
			return this.Production == other.Production &&
				   this.Pos == other.Pos &&
				   this.Table == other.Table;
		}

		internal Item Increment(int k = -1)
		{
			int tableNumber = (k == -1) ? Table : k;
			var copy = new Item(Production, Pos + 1, tableNumber);
			return copy;
		}

		public override string ToString()
		{
			string s = "<" + Production.LeftSide.Name + "> ->";
			Sentence sent = Production.RightSide;
			int i;
			for(i = 0; i < Pos; i++) {
				if(i == sent.Count) continue;
				s += " <" + sent[i].Name + ">";
			}
			//if ((Pos != sent.Count) ) s += sent[i].Name;
			s += " dot";
			for(i = Pos; i < sent.Count; i++) {
				s += " <" + sent[i].Name + ">";
			}
			s += (", FROM " + Table);
				return s;
        }

		internal Item Decrement()
		{
			var copy = new Item(Production, Pos - 1, Table);
			return copy;
		}

		internal bool IsComplete()
		{
			return this.NextWord == null;
		}
	}
}