using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaltMalKurzReworked.Game
{
    public class Stack
    {
        private List<Card> Cards = new List<Card>();
        private Pile Pile;
        private Random random = new Random();
        public Stack(Pile pile)
        {
            GenerateNewCards();
            Pile = pile;
        }

        #region Card Generation
        private void GenerateNewCards()
        {
            AddCards(Type.AchMeinDein, 3);
            AddCards(Type.GruppenSchnickSchnackSchnuck, 3);
            AddCards(Type.HaltMalKurz, 6);
            AddCards(Type.Kapitalismus, 3);
            Cards.Add(new Card(Symbol.All, Type.Kommunismus));
            AddCards(Type.Nazi, 15);
            AddCards(Type.NotToDoListe, 6);
            AddCards(Type.Polizei, 9);
            Cards.Add(new Card(Symbol.None, Type.Razupaltuff));
            Cards.Add(new Card(Symbol.None, Type.Razupaltuff));
            AddCards(Type.SchnickSchnackSchnuck, 6);
            AddCards(Type.Vollversammlung, 6);
        }

        private void AddCards(Type type, int n)
        {
            for (int i=1; i <= n; i++)
            {
                Symbol symbol;
                switch(i % 3)
                {
                    case 1:
                        symbol = Symbol.Kleinkünstler;
                        break;
                    case 2:
                        symbol = Symbol.Känguru;
                        break;
                    case 0:
                        symbol = Symbol.Pinguin;
                        break;
                    default:
                        symbol = Symbol.None;
                        break;
                }
                Cards.Add(new Card(symbol, type));
            }
        }
        #endregion

        public Card Draw()
        {
            if (Cards.Count < 1)
            {
                Cards.AddRange(Pile.Collect());
            }
            int i = random.Next(0, Cards.Count - 1);
            var temp = Cards[i];
            Cards.Remove(temp);
            return temp;
        }

        public List<Card> Draw(int count)
        {
            var temp = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                temp.Add(Draw());
            }
            return temp;
        }
    }
}
