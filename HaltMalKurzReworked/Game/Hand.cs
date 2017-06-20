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
            if (c.Type == Type.Razupaltuff) OnRazupaltuff?.Invoke(this, c);
        }
        public void AddCard(List<Card> c)
        {
            Cards.AddRange(c);
            foreach (Card c2 in c.FindAll(x => x.Type == Type.Razupaltuff)) OnRazupaltuff?.Invoke(this, c2);
        }
        public event RazupaltuffEvent OnRazupaltuff;
        public delegate void RazupaltuffEvent(object sender, Card card);
    }
}
