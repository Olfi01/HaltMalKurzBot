using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaltMalKurzReworked.Game
{
    public class Card
    {
        #region Fields
        public Color Color
        {
            get
            {
                switch (Type)
                {
                    case Type.AchMeinDein:
                    case Type.GruppenSchnickSchnackSchnuck:
                    case Type.HaltMalKurz:
                    case Type.Kommunismus:
                    case Type.NotToDoListe:
                    case Type.SchnickSchnackSchnuck:
                    case Type.Vollversammlung:
                        return Color.Witzig;
                    case Type.Nazi:
                    case Type.Kapitalismus:
                    case Type.Polizei:
                        return Color.NichtWitzig;
                    case Type.Razupaltuff:
                    default:
                        return Color.Razupaltuff;
                }
            }
        }
        public Symbol Symbol { get; }
        public Type Type { get; }
        #endregion
        public Card(Symbol symbol, Type type)
        {
            Symbol = symbol;
            Type = type;
        }
    }

    #region Enums
    public enum Color
    {
        Witzig,
        NichtWitzig,
        Razupaltuff
    }

    public enum Symbol
    {
        Känguru,
        Pinguin,
        Kleinkünstler,
        All,
        None
    }

    public enum Type
    {
        HaltMalKurz,
        Vollversammlung,
        SchnickSchnackSchnuck,
        GruppenSchnickSchnackSchnuck,
        AchMeinDein,
        Kommunismus,
        NotToDoListe,
        Nazi,
        Polizei,
        Kapitalismus,
        Razupaltuff
    }
    #endregion
}
