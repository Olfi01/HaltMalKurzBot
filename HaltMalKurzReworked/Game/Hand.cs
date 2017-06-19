using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaltMalKurzReworked.Game
{
    public class Hand
    {
        public List<Card> Cards = new List<Card>();
        public void AddCard(Card c)
        {
            Cards.Add(c);
        }
        public void AddCard(List<Card> c)
        {
            Cards.AddRange(c);
        }
    }
}
