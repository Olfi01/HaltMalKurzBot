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
        #region Constants
        public const string emoji_witzig = "😂";
        public const string emoji_nichtwitzig = "😐";
        public const string emoji_razupaltuff = "💩";
        public const string emoji_känguru = "🐨";
        public const string emoji_pinguin = "🐧";
        public const string emoji_kleinkünstler = "🎸";
        #endregion
        public Card(Symbol symbol, Type type)
        {
            Symbol = symbol;
            Type = type;
        }
        #region To String
        public override string ToString()
        {
            string o = "";
            switch (Type)
            {
                case Type.AchMeinDein:
                    o += "Ach, mein, dein...";
                    break;
                case Type.GruppenSchnickSchnackSchnuck:
                    o += "Gruppen-Schnick";
                    break;
                case Type.HaltMalKurz:
                    o += "Halt mal kurz";
                    break;
                case Type.Kapitalismus:
                    o += "Kapitalismus";
                    break;
                case Type.Kommunismus:
                    o += "Kommunismus";
                    break;
                case Type.Nazi:
                    o += "Nazi";
                    break;
                case Type.NotToDoListe:
                    o += "Not-to-do-Liste";
                    break;
                case Type.Polizei:
                    o += "Polizei";
                    break;
                case Type.Razupaltuff:
                    o += "Razupaltuff";
                    break;
                case Type.SchnickSchnackSchnuck:
                    o += "Schnick-Schnack-Schnuck";
                    break;
                case Type.Vollversammlung:
                    o += "Vollversammlung";
                    break;
            }
            o += " ";
            switch (Color)
            {
                case Color.NichtWitzig:
                    o += emoji_nichtwitzig;
                    break;
                case Color.Razupaltuff:
                    o += emoji_razupaltuff;
                    break;
                case Color.Witzig:
                    o += emoji_witzig;
                    break;
            }
            o += " ";
            switch (Symbol)
            {
                case Symbol.All:
                    o += emoji_kleinkünstler + emoji_känguru + emoji_pinguin;
                    break;
                case Symbol.Kleinkünstler:
                    o += emoji_kleinkünstler;
                    break;
                case Symbol.Känguru:
                    o += emoji_känguru;
                    break;
                case Symbol.None:
                    break;
                case Symbol.Pinguin:
                    o += emoji_pinguin;
                    break;
            }
            return o;
        }
        #endregion
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
