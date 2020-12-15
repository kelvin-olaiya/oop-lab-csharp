namespace Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A factory class for building <see cref="ISet{T}">decks</see> of <see cref="Card"/>s.
    /// </summary>
    public class DeckFactory
    {
        private string[] seeds;

        private string[] names;

        public IList<string> Seeds
        {
            set { this.seeds = value.ToArray();  }
            get { return this.seeds.ToList();  }
        }

        public IList<string> Names
        {
            set { this.names = value.ToArray(); }
            get { return this.names.ToList(); }
        }

        public int DeckSize => this.Seeds.Count * this.Names.Count;
        public ISet<Card> Deck
        {
            get
            {
                if (this.Names == null || this.Seeds == null)
                {
                    throw new InvalidOperationException();
                }

                return new HashSet<Card>(Enumerable
                    .Range(0, this.Names.Count)
                    .SelectMany(i => Enumerable
                        .Repeat(i, this.Seeds.Count)
                        .Zip(
                            Enumerable.Range(0, this.Seeds.Count),
                            (n, s) => Tuple.Create(this.Names[n], this.Seeds[s], n)))
                    .Select(tuple => new Card(tuple))
                    .ToList());
            }
            
        }
    }
}
