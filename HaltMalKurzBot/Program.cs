using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace HaltMalKurzBot
{
    #region Main Class
    class Program
    {
        //SYNTACTICAL INFORMATION:  Fields (the ones with get and set) start with UPPERCASE letters.    Example: public int Bla { get; }
        //                          Variables or Constants start with lowercase letters.                Example: public int bla = 0;
        #region Constants
        #region File Ids
        private const string achMeinDeinKänguruFileId = "BQADAgAD4QADuNXvD7DVvOPPLJ9FAg";
        private const string achMeinDeinKleinkünstlerFileId = "BQADAgAD4wADuNXvD0s8uqEC4GaoAg";
        private const string achMeinDeinPinguinFileId = "BQADAgAD5QADuNXvD4d55YERfBxOAg";
        private const string gruppenSchnickSchnackSchnuckKänguruFileId = "BQADAgAD5wADuNXvD92soH-U6_CdAg";
        private const string gruppenSchnickSchnackSchnuckKleinkünstlerFileId = "BQADAgAD6QADuNXvD5CXgSytenphAg";
        private const string gruppenSchnickSchnackSchnuckPinguinFileId = "BQADAgAD6wADuNXvD2NaohEJcbgLAg";
        private const string haltMalKurzKänguruFileId = "BQADAgAD7QADuNXvD9AZy4cPVhiKAg";
        private const string haltMalKurzKleinkünstlerFileId = "BQADAgAD7wADuNXvDyjwPV3Kw965Ag";
        private const string haltMalKurzPinguinFileId = "BQADAgAD8QADuNXvDwRzCZZI2Yt2Ag";
        #endregion
        public const string emoji_witzig = "😂";
        public const string emoji_nichtwitzig = "😐";
        public const string emoji_razupaltuff = "💩";
        public const string emoji_känguru = "🐨";
        public const string emoji_pinguin = "🐧";
        public const string emoji_kleinkünstler = "🎸";
        private const string apiToken = "278139117:AAFl13vIj7dVwbep1wQs1_Mw4mQZDa3yR3Y";    //the bot's api token
        private const string url = "https://api.telegram.org/bot" + apiToken + "/"; //should just stay like this
        #endregion
        #region Variables
        Random rnd = new Random();
        public static ArrayList GameIds;
        public static int lastUpdate;
        public static int newUpdate;
        #endregion
        #region Main Method
        static void Main(string[] args)
        {
            //Not implemented
        }
        #endregion

        #region Methods

        #region Api Methods
        private static Stream getUpdates()
        {
            newUpdate = lastUpdate + 1;
            string append = "getUpdates";
            if (newUpdate != 1)
            {
                append += "?offset=" + newUpdate.ToString();
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + append);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            return resStream;
        }
        private static bool sendFile(string file_id, string type, long chatid, Message replyto = null)
        {
            string append = "";
            if (type == "audio")
            {
                append = "sendAudio?chat_id=" + chatid.ToString() + "&audio=" + file_id;
                if (replyto != null)
                {
                    append += "&reply_to_message_id=" + replyto.MessageId;
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + append);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                return DecodeRaw<Message>(resStream).Ok;
            }
            else
            {
                NotImplementedException e = new NotImplementedException("Bisher sind nur folgende Typen implementiert: audio");
                throw e;
            }
        }

        private static bool sendMessage(string txt, long chatid, Message replyto = null, string parsemode = null)
        {
            string append = "sendMessage?text=" + txt + "&chat_id=" + chatid.ToString();
            if (replyto != null)
            {
                append += "&reply_to_message_id=" + replyto.MessageId;
            }
            if (parsemode != null)
            {
                append += "&parse_mode=" + parsemode;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + append);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            return DecodeRaw<Message>(resStream).Ok;
        }

        private static T Decode<T>(Stream str)
        {
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(str))
            using (var jtr = new JsonTextReader(sr))
            {
                var response = serializer.Deserialize<ApiResponse<T>>(jtr);
                return response.ResultObject;
            }
        }

        private static ApiResponse<T> DecodeRaw<T>(Stream str)
        {
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(str))
            using (var jtr = new JsonTextReader(sr))
            {
                return serializer.Deserialize<ApiResponse<T>>(jtr);
            }
        }
        #endregion
        #endregion
    }
    #endregion

    #region Custom Classes

    #region Card
    class Card
    {
        public int SymbolId { get; }
        public int ColorId { get; }
        public int TypeId { get; }
        public Card(int symbolId, int typeId)
        {
            SymbolId = symbolId;
            if (typeId < 0)
            {
                ColorId = Color.IdNichtWitzig;
            }
            else if (typeId == 0)
            {
                ColorId = Color.IdRazupaltuff;
            }
            else if (typeId > 0)
            {
                ColorId = Color.IdWitzig;
            }
            TypeId = typeId;
        }
    }
    #endregion

    #region Card Stack

    class CardStack //Na(ch)zi(eh)stapel
    {
        public ArrayList Cards { get; }
        private Random rnd = new Random();
        private CardPile pile;

        #region Constructor
        public CardStack(CardPile pile)
        {
            this.pile = pile;
            init();
        }
        #endregion

        #region Initializer
        private void init(int haltMalKurzKarten = 6,
            int vollversammlungKarten = 6,
            int schnickSchnackSchnuckKarten = 6,
            int gruppenSchnickSchnackSchnuckKarten = 3,
            int achMeinDeinKarten = 3,
            int kommunismusKarten = 1,
            int notToDoListeKarten = 6,
            int naziKarten = 15,
            int polizeiKarten = 9,
            int kapitalismusKarten = 3,
            int razupaltuffKarten = 2)
        {
            int t;
            for (int i = 1; i < haltMalKurzKarten + 1; i++)
            {
                t = Type.IdHaltMalKurz;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < vollversammlungKarten + 1; i++)
            {
                t = Type.IdVollversammlung;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < schnickSchnackSchnuckKarten + 1; i++)
            {
                t = Type.IdSchnickSchnackSchnuck;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < gruppenSchnickSchnackSchnuckKarten + 1; i++)
            {
                t = Type.IdGruppenSchnickSchnackSchnuck;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < achMeinDeinKarten + 1; i++)
            {
                t = Type.IdAchMeinDein;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < kommunismusKarten + 1; i++)
            {
                t = Type.IdKommunismus;
                addCard(new Card(Symbol.IdAll, t));
            }
            for (int i = 1; i < notToDoListeKarten + 1; i++)
            {
                t = Type.IdNotToDoListe;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < naziKarten + 1; i++)
            {
                t = Type.IdNazi;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < polizeiKarten + 1; i++)
            {
                t = Type.IdPolizei;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < kapitalismusKarten + 1; i++)
            {
                t = Type.IdKapitalismus;
                switch (i % 3)
                {
                    case 1:
                        addCard(new Card(Symbol.IdKleinkünstler, t));
                        break;
                    case 2:
                        addCard(new Card(Symbol.IdKänguru, t));
                        break;
                    case 0:
                        addCard(new Card(Symbol.IdPinguin, t));
                        break;
                }
            }
            for (int i = 1; i < razupaltuffKarten + 1; i++)
            {
                t = Type.IdRazupaltuff;
                addCard(new Card(Symbol.IdRazupaltuff, t));
            }
        }
        #endregion

        #region Methods
        private void addCard(Card card)
        {
            Cards.Add(card);
            shuffle();
        }

        private void shuffle()
        {
            if (Cards.Count < 1)
            {
                renewStack();
            }
            Card[] oldCard = (Card[]) Cards.ToArray();
            Card[] newCard = new Card[oldCard.Length];
            int[] usedInt = new int[oldCard.Length];
            int r;
            for (int i = 0; i < oldCard.Length; i++)
            {
                do
                {
                    r = rnd.Next(oldCard.Length);
                } while (Array.IndexOf(usedInt, r)>-1);
                newCard[r] = oldCard[i];
            }
            Cards.Clear();
            foreach (Card c in newCard)
            {
                Cards.Add(c);
            }
        }

        private Card drawCard()
        {
            if (Cards.Count < 1)
            {
                renewStack();
            }
            Card c = (Card) Cards[0];
            Cards.RemoveAt(Cards.IndexOf(c));
            return c;
        }

        private void renewStack()
        {
            ArrayList pileCards = pile.Cards;
            Card topCardPile = pile.getTopCardAndClear();
            pile.addCard(topCardPile);
            pileCards.Remove(topCardPile);
            foreach (Card c in pileCards)
            {
                Cards.Add(c);
            }
            shuffle();
        }
        #endregion
    }
    #endregion

    #region Card Pile

    class CardPile  //Ablagestapel
    {
        public ArrayList Cards { get; }
        public Card TopCard
        {
            get
            {
                Card c = (Card)Cards[Cards.Count - 1];
                return c;
            }
        }
        #region Methods

        public void addCard(Card c)
        {
            Cards.Add(c);
        }

        public Card getTopCardAndClear()
        {
            Card tempCard = TopCard;
            Cards.Clear();
            return tempCard;
        }
        #endregion
    }
    #endregion

    #region Card Hand

    class CardHand
    {
        public ArrayList Cards { get; }
        public int CardCount { get { return Cards.Count; } }

        public void addCard(Card c)
        {
            Cards.Add(c);
        }

        public Card takeCard(Card c)
        {
            Card returnCard = (Card) Cards[Cards.IndexOf(c)];
            Cards.Remove(returnCard);
            return returnCard;
        }
    }
    #endregion

    #region Game

    class Game
    {
        //Not fully implemented
        public int Id { get; }
        public ArrayList Players { get; }
        private bool running;
        public bool IsRunning { get { return running; } }
        private int turn;
        public Player TurnPlayer { get { return (Player) Players[turn]; } }
        public Game()
        {
            running = false;
            throw new NotImplementedException();    //Not implemented
        }

        public bool addPlayer(Player p)
        {
            if (!running)
            {
                Players.Add(p);
                return true;
            }
            return false;
        }

        public void start()
        {
            throw new NotImplementedException(); //Not Implemented
        }

        public void stop()
        {
            throw new NotImplementedException(); //Not Implemented
        }

        public void gameThread()
        {
            throw new NotImplementedException();    //Not Implemented
        }
    }
    #endregion

    #region Player

    class Player
    {
        //Not fully implemented
        public long Id { get; }
        public User User { get; }
        public string FirstName { get; }
        public string FullName { get; }
        public Player(User u)
        {
            User = u;
            Id = u.Id;
            FirstName = u.FirstName;
            FullName = FirstName;
            if (u.LastName != null)
            {
                FullName += " " + u.LastName;
            }
        }
    }
    #endregion

    #region Color

    static class Color
    {
        public static int IdRazupaltuff { get { return 0; } }
        public static int IdWitzig { get { return 1; } }
        public static int IdNichtWitzig { get { return 2; } }
    }
    #endregion

    #region Symbol

    static class Symbol
    {
        public static int IdRazupaltuff { get { return -1; } }
        public static int IdAll { get { return 0; } }
        public static int IdKänguru { get { return 1; } }
        public static int IdPinguin { get { return 2; } }
        public static int IdKleinkünstler { get { return 3; } }
    }
    #endregion

    #region Types

    static class Type
    {
        //Witzige Karten haben eine positive Id, nicht witzige eine negative und die Ausnahme Razupaltuff ist 0
        public static int IdKapitalismus { get { return -3; } }
        public static int IdPolizei { get { return -2; } }
        public static int IdNazi { get { return -1; } }
        public static int IdRazupaltuff { get { return 0; } }
        public static int IdHaltMalKurz { get { return 1; } }
        public static int IdVollversammlung { get { return 2; } }
        public static int IdSchnickSchnackSchnuck { get { return 3; } }
        public static int IdGruppenSchnickSchnackSchnuck { get { return 4; } }
        public static int IdAchMeinDein { get { return 5; } }
        public static int IdKommunismus { get { return 6; } }
        public static int IdNotToDoListe { get { return 7; } }
    }
    #endregion
    #endregion
}
