using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaltMalKurzReworked.Game
{
    public class Pile
    {
        private List<Card> Cards = new List<Card>();
        public Card TopCard { get { return Cards.Last(); } }
        public void AddCard(Card c)
        {
            Cards.Add(c);
        }
        public List<Card> Collect()
        {
            var temp = Cards;
            var tempc = TopCard;
            temp.Remove(tempc);
            Cards.Clear();
            Cards.Add(tempc);
            return temp;
        }
        public bool Fits(Card c)
        {
            return c.Color == TopCard.Color || c.Symbol == TopCard.Symbol || c.Symbol == Symbol.All || TopCard.Symbol == Symbol.All;
        }
    }
}
