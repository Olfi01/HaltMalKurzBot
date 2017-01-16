using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
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
        public const string achMeinDeinKänguruFileId = "BQADAgAD4QADuNXvD7DVvOPPLJ9FAg";
        public const string achMeinDeinKleinkünstlerFileId = "BQADAgAD4wADuNXvD0s8uqEC4GaoAg";
        public const string achMeinDeinPinguinFileId = "BQADAgAD5QADuNXvD4d55YERfBxOAg";
        public const string gruppenSchnickSchnackSchnuckKänguruFileId = "BQADAgAD5wADuNXvD92soH-U6_CdAg";
        public const string gruppenSchnickSchnackSchnuckKleinkünstlerFileId = "BQADAgAD6QADuNXvD5CXgSytenphAg";
        public const string gruppenSchnickSchnackSchnuckPinguinFileId = "BQADAgAD6wADuNXvD2NaohEJcbgLAg";
        public const string haltMalKurzKänguruFileId = "BQADAgAD7QADuNXvD9AZy4cPVhiKAg";
        public const string haltMalKurzKleinkünstlerFileId = "BQADAgAD7wADuNXvDyjwPV3Kw965Ag";
        public const string haltMalKurzPinguinFileId = "BQADAgAD8QADuNXvDwRzCZZI2Yt2Ag";
        public const string kapitalismusKänguruFileId = "BQADAgAD8wADuNXvD0AfzlnAnDcBAg";
        public const string kapitalismusKleinkünstlerFileId = "BQADAgAD9QADuNXvD0ThX6to2JdqAg";
        public const string kapitalismusPinguinFileId = "BQADAgAD9wADuNXvDzw_kIVSZQ1TAg";
        public const string kommunismusFileId = "BQADAgAD-QADuNXvDybdi8lkuQZQAg";
        public const string naziKänguruFileId = "BQADAgAD-wADuNXvD3Q3fLfoyHO8Ag";
        public const string naziKleinkünstlerFileId = "BQADAgAD_QADuNXvD3ZMEwHbIey6Ag";
        public const string naziPinguinFileId = "BQADAgAD_wADuNXvD95PjnX34SFmAg";
        public const string notToDoListeKänguruFileId = "BQADAgADAQEAArjV7w9TBJCNyVVwugI";
        public const string notToDoListeKleinkünstlerFileId = "BQADAgADAwEAArjV7w_9XHpoMREvtQI";
        public const string notToDoListePinguinFileId = "BQADAgADBQEAArjV7w-TOOnmjYoSgAI";
        public const string polizeiKänguruFileId = "BQADAgADBwEAArjV7w8jLlfop-K6DgI";
        public const string polizeiKleinkünstlerFileId = "BQADAgADCQEAArjV7w9Bmy1WHQlbLQI";
        public const string polizeiPinguinFileId = "BQADAgADCwEAArjV7w8_OvK8YGuSKgI";
        public const string razupaltuffFileId = "BQADAgADDQEAArjV7w9c3w18rW2kIgI";
        public const string schnickSchnackSchnuckKänguruFileId = "BQADAgADDwEAArjV7w9XEYFbZCvsxwI";
        public const string schnickSchnackSchnuckKleinkünstlerFileId = "BQADAgADEQEAArjV7w_BxM73LDagLgI";
        public const string schnickSchnackSchnuckPinguinFileId = "BQADAgADEwEAArjV7w_IHY6ObhVJqgI";
        public const string vollversammlungKänguruFileId = "BQADAgADFQEAArjV7w-xyus4DoA1tgI";
        public const string vollversammlungKleinkünstlerFileId = "BQADAgADFwEAArjV7w_UmAHHSysVCAI";
        public const string vollversammlungPinguinFileId = "BQADAgADGQEAArjV7w-ZcGk7g_ZyTQI";
        #endregion
        public const string emoji_witzig = "😂";
        public const string emoji_nichtwitzig = "😐";
        public const string emoji_razupaltuff = "💩";
        public const string emoji_känguru = "🐨";
        public const string emoji_pinguin = "🐧";
        public const string emoji_kleinkünstler = "🎸";
        private const string apiToken = "278139117:AAFl13vIj7dVwbep1wQs1_Mw4mQZDa3yR3Y";    //the bot's api token
        private const string url = "https://api.telegram.org/bot" + apiToken + "/"; //should just stay like this
        public const string botUsername = "@HaltMalKurzBot";
        #endregion
        #region Variables
        Random rnd = new Random();
        public static ArrayList gameIds = new ArrayList();
        public static ArrayList games = new ArrayList();
        public static ArrayList groups = new ArrayList();
        public static int lastUpdate;
        public static int newUpdate;
        private static Thread t;
        #endregion
        #region Main Method
        static void Main(string[] args)
        {
            bool consoleCycle = true;
            while (consoleCycle)
            {
                Console.WriteLine("Befehl eingeben");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "startbot":
                        t = new Thread(updateThread);
                        t.Start();
                        Console.WriteLine("Bot started");
                        break;
                    case "stopbot":
                        t.Abort();
                        Console.WriteLine("Bot stopped");
                        break;
                    case "help":
                        Console.WriteLine("startbot");
                        Console.WriteLine("Startet den Bot");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("stopbot");
                        Console.WriteLine("Stoppt den Bot");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("listgames");
                        Console.WriteLine("Listet alle Spiele auf");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("exit");
                        Console.WriteLine("Beendet das gesamte Programm");
                        break;
                    case "exit":
                        consoleCycle = false;
                        break;
                    case "listgames":
                        Console.WriteLine("Alle Spiele:");
                        foreach (Game g in games)
                        {
                            Console.WriteLine("Spiel " + g.Id + ":");
                            Console.WriteLine("In der Gruppe " + g.Group.FirstName + " " + g.Group.LastName + " (" + g.Id + ")");
                        }
                        Console.WriteLine("Ende der Liste");
                        break;
                    default:
                        Console.WriteLine("Befehl nicht erkannt");
                        break;
                }
            }
        }
        #endregion

        #region Methods
        private static void updateThread()
        {
            while (true)
            {
                Stream stream = getUpdates();
                Update[] updates = Decode<Update[]>(stream);
                foreach (Update u in updates)
                {
                    lastUpdate = u.Id;
                    if (u.Message != null)  //If update is a message
                    {
                        if (u.Message.Entities != null) //If the message contains an entity
                        {
                            foreach (MessageEntity me in u.Message.Entities)    //Search for every entity
                            {
                                if (me.Type == MessageEntityType.BotCommand)    //If the entity is a command
                                {
                                    if (me.Offset == 0) //If the command is at the beginning
                                    {
                                        if (me.Length == u.Message.Text.Length) //if it's the only thing in the Message
                                        {
                                            runCommand(cmd: u.Message.Text, msg: u.Message, upd: u);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (u.CallbackQuery != null)    //If the update is a callback query
                    {
                        string[] args = u.CallbackQuery.Data.Split(',');
                        foreach (Game g in games)
                        {
                            if (args[0] == g.Group.Id.ToString())
                            {
                                g.CalledBackData = args;
                            }
                        }
                    }
                }
            }
        }

        private static void runCommand(string cmd, Message msg, Update upd)
        {
            switch (cmd)
            {
                case "/startgame":
                case "/startgame" + botUsername:
                    if (msg.Chat.Id < 0)
                    {
                        if (groups.Contains(msg.Chat))
                        {
                            foreach (Game g in games)
                            {
                                if (g.Group == msg.Chat)
                                {
                                    g.addPlayer(new Player(msg.From), msg);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Game g = new Game(msg.Chat);
                            g.addPlayer(new Player(msg.From), msg);
                            games.Add(g);
                            groups.Add(msg.Chat);
                        }
                    }
                    break;
                case "/join":
                case "/join" + botUsername:
                    bool foundGame = false;
                    foreach (Game g in games)
                    {
                        if (g.Group == msg.Chat)
                        {
                            g.addPlayer(new Player(msg.From), msg);
                            foundGame = true;
                            break;
                        }
                    }
                    if (!foundGame)
                    {
                        sendMessage(txt: "Es läft gerade kein Spiel. Starte ein neues Spiel mit /startgame",
                            chatid: msg.Chat.Id, replyto: msg);
                    }
                    break;
                default:
                    break;
            }
        }

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
        public static bool sendFile(string file_id, string type, long chatid, Message replyto = null)
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

        public static bool sendMessage(string txt, long chatid, Message replyto = null, string parsemode = null, InlineKeyboardMarkup inlineMarkup = null)
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
            if (inlineMarkup != null)
            {
                append += "&reply_markup=" + JsonConvert.SerializeObject(inlineMarkup);
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

        public static bool editMessageReplyMarkup(long chatid, int messageid, object inlineMarkup)
        {
            string append = "editMessageReplyMarkup?chat_id=" + chatid.ToString();
            append += "&reply_markup=" + JsonConvert.SerializeObject(inlineMarkup);
            append += "&messag_id=" + messageid;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + append);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            return DecodeRaw<Message>(resStream).Ok;
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
        public string SymbolEmoji { get; }
        public string ColorEmoji { get; }
        public string TypeName { get; }
        #region Constructor
        public Card(int symbolId, int typeId)
        {
            SymbolId = symbolId;
            switch (symbolId)
            {
                case Symbol.RawIdAll:
                    SymbolEmoji = Program.emoji_kleinkünstler + Program.emoji_känguru + Program.emoji_pinguin;
                    break;
                case Symbol.RawIdKleinkünstler:
                    SymbolEmoji = Program.emoji_kleinkünstler;
                    break;
                case Symbol.RawIdKänguru:
                    SymbolEmoji = Program.emoji_känguru;
                    break;
                case Symbol.RawIdPinguin:
                    SymbolEmoji = Program.emoji_pinguin;
                    break;
                case Symbol.RawIdRazupaltuff:
                    SymbolEmoji = Program.emoji_razupaltuff;
                    break;
            }
            if (typeId < 0)
            {
                ColorId = Color.IdNichtWitzig;
                ColorEmoji = Program.emoji_nichtwitzig;
            }
            else if (typeId == 0)
            {
                ColorId = Color.IdRazupaltuff;
                ColorEmoji = Program.emoji_razupaltuff;
            }
            else if (typeId > 0)
            {
                ColorId = Color.IdWitzig;
                ColorEmoji = Program.emoji_witzig;
            }
            TypeId = typeId;
            switch (typeId)
            {
                case Type.RawIdAchMeinDein:
                    TypeName = "Ach, mein, dein...";
                    break;
                case Type.RawIdGruppenSchnickSchnackSchnuck:
                    TypeName = "Gruppen-Schnick-Schnack-Schnuck";
                    break;
                case Type.RawIdHaltMalKurz:
                    TypeName = "Halt mal kurz";
                    break;
                case Type.RawIdKapitalismus:
                    TypeName = "Der Kapitalismus";
                    break;
                case Type.RawIdKommunismus:
                    TypeName = "Der Kommunismus";
                    break;
                case Type.RawIdNazi:
                    TypeName = "Nazi";
                    break;
                case Type.RawIdNotToDoListe:
                    TypeName = "Not-To-Do-Liste";
                    break;
                case Type.RawIdPolizei:
                    TypeName = "Polizei";
                    break;
                case Type.RawIdRazupaltuff:
                    TypeName = "Razupaltuff!";
                    break;
                case Type.RawIdSchnickSchnackSchnuck:
                    TypeName = "Schnick-Schnack-Schnuck";
                    break;
                case Type.RawIdVollversammlung:
                    TypeName = "Vollversammlung";
                    break;
            }
        }
        #endregion

        #region Methods
        public bool fitsOn(Card anotherCard)
        {
            if (SymbolId == anotherCard.SymbolId || ColorId == anotherCard.ColorId)
            {
                return true;
            }
            if (SymbolId == Symbol.IdAll)
            {
                return true;
            }
            return false;
        }
        #endregion
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
            shuffle();
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
        public void addCard(Card card)
        {
            Cards.Add(card);
            shuffle();
        }

        public void shuffle()
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

        public Card drawCard()
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
        public ArrayList Cards { get; set; }
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
        public int Id { get; }
        public ArrayList Players { get; }
        private ArrayList allPlayers { get; set; }
        private bool running;
        public bool IsRunning { get { return running; } }
        private int turn;
        public Chat Group { get; }
        public Player TurnPlayer { get { return (Player) Players[turn]; } }
        public CardStack Stack { get; }
        public CardPile Pile { get; }
        private Thread threadGame;
        public string[] CalledBackData { get; set; }
        public int MaxRankWon { get; set; }
        public Game(Chat group)
        {
            running = false;
            ArrayList gi = Program.gameIds;
            int c = gi.Count;
            bool hasId = false;
            for (int i = 0; i <= c && !hasId; i++)
            {
                if (!gi.Contains(i))
                {
                    Id = i;
                    hasId = true;
                }
            }
            Group = group;
            turn = 0;
            Program.gameIds.Add(Id);
            threadGame = new Thread(gameThread);
            MaxRankWon = 0;
        }

        ~Game()
        {
            Program.gameIds.Remove(Id);
            Program.games.Remove(this);
            Program.groups.Remove(Group);
        }

        public bool addPlayer(Player p, Message m)
        {
            if (!running)
            {
                if(Program.sendMessage(txt: "Du bist einem Spiel beigetreten", chatid: p.User.Id))
                {
                    Players.Add(p);
                    p.Group = Group;
                    p.GameIn = this;
                    return true;
                }
                else
                {
                    InlineKeyboardButton button = new InlineKeyboardButton("Starte mich");
                    button.Url = "t.me/" + Program.botUsername.Remove(0, 1);
                    InlineKeyboardButton[] buttons = { button };
                    InlineKeyboardMarkup im = new InlineKeyboardMarkup(buttons);
                    Program.sendMessage(txt: "Du musst mich zuerst starten, damit ich dich privat anschreiben kann.\nTippe dazu auf Starte mich oder auf " + Program.botUsername + " und dann auf Starten.",
                        inlineMarkup: im, chatid: m.Chat.Id, replyto: m);
                    return false;
                }
            }
            else
            {
                Program.sendMessage(txt: "Das Spiel läuft bereits, bitte warte auf das nächste Spiel", chatid: m.Chat.Id, replyto: m);
            }
            return false;
        }

        public void start()
        {
            int c = 10 - Players.Count;
            allPlayers = Players;
            foreach (Player p in Players)
            {
                Program.sendMessage(txt: "Karten werden ausgeteilt...", chatid: Group.Id);
                dealCards(player: p, count: c);
            }
            threadGame.Start();
        }

        private void dealCards(Player player, int count)
        {
            player.Hand = new CardHand();
            for (int i = 0; i < count; i++)
            {
                player.Hand.addCard(Stack.drawCard());
                player.tellCards();
            }
        }

        public void stop()
        {
            threadGame.Abort();
            running = false;
            Program.gameIds.Remove(Id);
            Program.games.Remove(this);
            Program.groups.Remove(Group);
            //show results
        }

        public void gameThread()
        {
            Pile.addCard(Stack.drawCard());
            Program.sendMessage(txt: "Das Spiel beginnt!\nMomentan hat jeder " + TurnPlayer.Hand.CardCount + " Karten",
                chatid: Group.Id);
            while (running)
            {
                Player turnPlayer = TurnPlayer;
                #region Card Selection
                ArrayList possibleCards = new ArrayList();
                Program.sendMessage(txt: turnPlayer.FullName + " wählt eine Karte...\nEr hat dreißig Sekunden Zeit",
                        chatid: Group.Id);
                foreach (Card c in turnPlayer.Hand.Cards)
                {
                    if (c.fitsOn(Pile.TopCard)) //see whether the person is allowed to play the card
                    {
                        possibleCards.Add(c);
                    }
                }
                bool drawCard;
                Card selectedCard = turnPlayer.selectCard(possibleCards, out drawCard);
                if (selectedCard == null && !drawCard)
                {
                    Program.sendMessage(txt: "Da hat wohl jemand vergessen, eine Karte zu legen...", chatid: Group.Id);
                    removePlayer(turnPlayer);
                    turn--;
                    checkRoutine();
                    continue;
                }
                else if (drawCard)
                {
                    //draw Card
                    turnPlayer.drawCard(Stack);
                    checkRoutine();
                    continue;
                }
                #endregion
                #region Play Card
                else
                {
                    //play Card
                    switch (selectedCard.TypeId)
                    {
                        #region Ach, mein, dein...
                        case Type.RawIdAchMeinDein:
                            turnPlayer.Hand.takeCard(selectedCard);
                            Pile.addCard(selectedCard);
                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Ach, mein, dein... das sind doch bürgerliche Kategorien.",
                                chatid: Group.Id, parsemode: "Markdown");
                            Player selectedPlayer = turnPlayer.selectPlayer(list: Players, 
                                msg: "Wähle einen Spieler, mit dem du Karten tauschen willst.");
                            if (selectedPlayer == null)
                            {
                                Program.sendMessage(txt: "Sieht aus, als hätte " + turnPlayer.FullName + " nichts ausgewählt!", 
                                    chatid: Group.Id);
                                removePlayer(turnPlayer);
                                turn--;
                                checkRoutine();
                                continue;
                            }
                            else
                            {
                                Program.sendMessage(txt: turnPlayer.FullName + " hat entschieden, mit " + selectedPlayer.FullName
                                    + " Karten zu tauschen.\nEr hat zehn Sekunden Zeit, eine Not-To-Do-Liste einzusetzen.", chatid: Group.Id);
                                bool blocked = false;
                                if (selectedPlayer.hasNotToDoList(Pile))
                                {
                                    bool yesIWant;
                                    Card selectedNotToDoList = selectedPlayer.askNotToDoList(out yesIWant, Pile);
                                    if (yesIWant)
                                    {
                                        blocked = true;
                                        Program.sendMessage(txt: "*" + selectedPlayer.FullName + ":* Das steht auf meiner Not-To-Do-Liste!",
                                            chatid: Group.Id, parsemode: "Markdown");
                                        selectedPlayer.Hand.takeCard(selectedNotToDoList);
                                        Pile.addCard(selectedNotToDoList);
                                        selectedPlayer.drawCard(Stack);
                                    }
                                }
                                if (!blocked)
                                {
                                    var temp = turnPlayer.Hand.Cards;
                                    turnPlayer.Hand.Cards = selectedPlayer.Hand.Cards;
                                    selectedPlayer.Hand.Cards = temp;
                                    Program.sendMessage(txt: turnPlayer.FullName + " und " + selectedPlayer.FullName
                                        + " haben Karten getauscht.", chatid: Group.Id);
                                    turnPlayer.tellCards();
                                    selectedPlayer.tellCards();
                                    checkRoutine();
                                    continue;
                                }
                            }
                            break;
                        #endregion
                        #region Gruppen-Schnick-Schnack-Schnuck
                        case Type.RawIdGruppenSchnickSchnackSchnuck:

                            break;
                        #endregion
                        #region Halt mal kurz
                        case Type.RawIdHaltMalKurz:

                            break;
                        #endregion
                        #region Kapitalismus
                        case Type.RawIdKapitalismus:

                            break;
                        #endregion
                        #region Kommunismus
                        case Type.RawIdKommunismus:

                            break;
                        #endregion
                        #region Nazi
                        case Type.RawIdNazi:

                            break;
                        #endregion
                        #region Not-To-Do-Liste
                        case Type.RawIdNotToDoListe:

                            break;
                        #endregion
                        #region Polizei
                        case Type.RawIdPolizei:

                            break;
                        #endregion
                        #region SchnickSchnackSchnuck
                        case Type.RawIdSchnickSchnackSchnuck:

                            break;
                        #endregion
                        #region Vollversammlung
                        case Type.RawIdVollversammlung:

                            break;
                        #endregion
                    }
                    checkRoutine();
                }
                #endregion
                //Not implemented
            }
        }

        private void checkRoutine()
        {
            turn++;
            if (turn >= Players.Count)
            {
                turn = 0;
            }
            foreach (Player p in Players)
            {
                if (p.Hand.CardCount < 1)
                {
                    Program.sendMessage(txt: p.FullName + " ist fertig!", chatid: Group.Id);
                    p.RankWon = ++MaxRankWon;
                    removePlayer(p);
                }
            }
            if (Players.Count < 2)
            {
                stop();
            }
            string msg = "Spieler-Übersicht:";
            foreach (Player p in Players)
            {
                msg += "\n" + p.FullName + " (" + p.Hand.CardCount + " Karten";
            }
            Program.sendMessage(txt: msg, chatid: Group.Id);
        }

        public void removePlayer(Player p)
        {
            Players.Remove(p);
            Program.sendMessage(txt: p.FullName + " hat das Spiel verlassen", chatid: Group.Id);
        }
    }
    #endregion

    #region Player

    class Player
    {
        public long Id { get; }
        public User User { get; }
        public string FirstName { get; }
        public string FullName { get; }
        public CardHand Hand { get; set; }
        public Chat Group { get; set; }
        public Game GameIn { get; set; }
        private string[] CalledBackData { get; set; }
        public int RankWon { get; set; }
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

        #region Methods
        public void tellCards()
        {
            string message = "Deine Karten sind:";
            foreach (Card c in Hand.Cards)
            {
                message += "\n" + c.ColorEmoji + c.SymbolEmoji + " " + c.TypeName;
            }
            throw new NotImplementedException();
        }

        public void drawCard(CardStack cs)
        {
            Hand.addCard(cs.drawCard());
            Program.sendMessage(txt: FullName + " hat eine Karte gezogen.", chatid: Group.Id);
            tellCards();
        }

        public Card selectCard(ArrayList selection, out bool drawCard)
        {
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[selection.Count + 1][];
            int i = 0;
            foreach (Card c in selection)
            {
                InlineKeyboardButton b = new InlineKeyboardButton(c.ColorEmoji + c.SymbolEmoji + " " + c.TypeName);
                b.CallbackData = Group.Id + "," + c.ColorId + "," + c.SymbolId + "," + c.TypeName;
                InlineKeyboardButton[] ba = { b };
                buttons[i] = ba;
                i++;
            }
            InlineKeyboardButton b2 = new InlineKeyboardButton("Karte ziehen");
            b2.CallbackData = Group.Id + "," + "DrawCard";
            InlineKeyboardButton[] bs = { b2 };
            buttons[i] = bs;
            InlineKeyboardMarkup im = new InlineKeyboardMarkup(buttons);
            Program.sendMessage(txt: "Wähle eine Karte.", chatid: Id, inlineMarkup: im);
            waitForCallback(30);
            string[] args = CalledBackData;
            CalledBackData = null;
            if (args[0] == "Timeout")
            {
                drawCard = false;
                return null;
            }
            else if (args[1] == "DrawCard")
            {
                drawCard = true;
                return null;
            }
            else
            {
                foreach (Card c in Hand.Cards)
                {
                    if (c.ColorId.ToString() == args[1] && c.SymbolId.ToString() == args[2] && c.TypeId.ToString() == args[3])
                    {
                        drawCard = false;
                        return c;
                    }
                }
                drawCard = false;
                return null;
            }
        }

        public void waitForCallback(int timeInSeconds = 30, Message msg)
        {
            for (int i = 0; i < timeInSeconds*2; i++)
            {
                if (GameIn.CalledBackData != null)
                {
                    CalledBackData = GameIn.CalledBackData;
                    GameIn.CalledBackData = null;
                    return;
                }
                Thread.Sleep(500);
            }
            string[] s = { "Timeout" };
            CalledBackData = s;
            Program.editMessageReplyMarkup(chatid: msg.Chat.Id, messageid: msg.MessageId, inlineMarkup: null);
        }

        public Player selectPlayer(ArrayList list, string msg)
        {
            list.Remove(this);
            InlineKeyboardButton[][] players = new InlineKeyboardButton[list.Count][];
            int i = 0;
            foreach (Player p in list)
            {
                InlineKeyboardButton b = new InlineKeyboardButton(p.FullName, Group.Id + "," + p.Id);
                InlineKeyboardButton[] ba = { b };
                players[i] = ba;
            }
            InlineKeyboardMarkup im = new InlineKeyboardMarkup(players);
            Program.sendMessage(txt: msg, chatid: Id, inlineMarkup: im);
            waitForCallback(30);
            string[] args = CalledBackData;
            CalledBackData = null;
            if (args[0] == "Timeout")
            {
                return null;
            }
            foreach (Player p in list)
            {
                if (p.Id.ToString() == args[1])
                {
                    return p;
                }
            }
            return null;
        }

        public bool hasNotToDoList(CardPile pile)
        {
            foreach (Card c in Hand.Cards)
            {
                if (c.TypeId == Type.IdNotToDoListe && c.fitsOn(pile.TopCard))
                {
                    return true;
                }
            }
            return false;
        }

        public Card askNotToDoList(out bool yesIWant, CardPile pile)
        {
            ArrayList selection = Hand.Cards;
            foreach (Card c in selection)
            {
                if (c.TypeId != Type.IdNotToDoListe || !c.fitsOn(pile.TopCard))
                {
                    selection.Remove(c);
                }
            }
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[selection.Count][];
            int i = 0;
            foreach (Card c in selection)
            {
                InlineKeyboardButton b = new InlineKeyboardButton(c.ColorEmoji + c.SymbolEmoji + " " + c.TypeName);
                b.CallbackData = Group.Id + ",yes," + c.ColorId + "," + c.SymbolId + "," + c.TypeName;
                InlineKeyboardButton[] ba = { b };
                buttons[i] = ba;
                i++;
            }
            InlineKeyboardButton b2 = new InlineKeyboardButton("Keine Not-To-Do-Liste einsetzen", Group.Id + ",no");
            InlineKeyboardButton[] bs = { b2 };
            buttons[i] = bs;
            InlineKeyboardMarkup im = new InlineKeyboardMarkup(buttons);
            Program.sendMessage(txt: "Willst du eine Not-To-Do-Liste einsetzen?\nWenn ja, welche?", chatid: Id, inlineMarkup: im);
            waitForCallback(10);
            string[] args = CalledBackData;
            CalledBackData = null;
            if (args[0] == "Timeout")
            {
                yesIWant = false;
                return null;
            }
            else
            {
                if (args[1] == "no")
                {
                    yesIWant = false;
                    return null;
                }
                else if (args[1] == "yes")
                {
                    yesIWant = true;
                    foreach (Card c in Hand.Cards)
                    {
                        if (c.ColorId.ToString() == args[2] && c.SymbolId.ToString() == args[3] && c.TypeId.ToString() == args[4])
                        {
                            return c;
                        }
                    }
                }
            }
            yesIWant = false;
            return null;
        }
        #endregion
    }
    #endregion

    #region Color

    static class Color
    {
        public static int IdRazupaltuff { get { return 0; } }
        public static int IdWitzig { get { return 1; } }
        public static int IdNichtWitzig { get { return 2; } }
        public const int RawIdRazupaltuff = 0;
        public const int RawIdWitzig = 1;
        public const int RawIdNichtWitzig = 2;
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
        public const int RawIdRazupaltuff = -1;
        public const int RawIdAll = 0;
        public const int RawIdKänguru = 1;
        public const int RawIdPinguin = 2;
        public const int RawIdKleinkünstler = 3;
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
        public const int RawIdKapitalismus = -3;
        public const int RawIdPolizei = -2;
        public const int RawIdNazi = -1;
        public const int RawIdRazupaltuff = 0;
        public const int RawIdHaltMalKurz = 1;
        public const int RawIdVollversammlung = 2;
        public const int RawIdSchnickSchnackSchnuck = 3;
        public const int RawIdGruppenSchnickSchnackSchnuck = 4;
        public const int RawIdAchMeinDein = 5;
        public const int RawIdKommunismus = 6;
        public const int RawIdNotToDoListe = 7;
    }
    #endregion
    #endregion
}
