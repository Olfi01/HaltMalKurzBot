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
        private const long flomsId = 267376056;
        private static readonly string projectPath = System.Windows.Forms.Application.StartupPath + "\\";
        #endregion
        #region Variables
        Random rnd = new Random();
        public static ArrayList gameIds = new ArrayList();
        public static ArrayList games = new ArrayList();
        public static ArrayList groups = new ArrayList();
        private static ArrayList callbacks = new ArrayList();
        public static int lastUpdate;
        public static int newUpdate;
        private static Thread t;
        private static Thread t2;
        private static ArrayList permissionIds = new ArrayList();
        #endregion
        #region Main Method
        static void Main(string[] args)
        {
            readPermissionFile();
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
                        t2 = new Thread(handleCallbacks);
                        t2.Start();
                        Console.WriteLine("Bot started");
                        break;
                    case "stopbot":
                        if (t != null) t.Abort();
                        if (t2 != null) t2.Abort();
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
                        if (t != null) t.Abort();
                        if (t2 != null) t2.Abort();
                        Console.WriteLine("Bot stopped");
                        consoleCycle = false;
                        break;
                    case "listgames":
                        Console.WriteLine("Alle Spiele:");
                        foreach (Game g in games)
                        {
                            Console.WriteLine("Spiel " + g.Id + ":");
                            Console.WriteLine("In der Gruppe " + g.Group.Title + " (" + g.Id + ")");
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
        #region Random Methods
        private static void handleCallbacks()
        {
            while (true)
            {
                ArrayList callbacksCopy = callbacks;
                if (callbacksCopy.Count > 0)
                {
                    foreach (CallbackQuery q in callbacksCopy)
                    {
                        answerCallbackQuery(q);
                        callbacks.Remove(q);
                        break;
                    }
                }
                Thread.Sleep(500);
            }
        }

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
                        if (u.Message.Text.StartsWith("!") && !u.Message.Text.Contains(" "))
                        {
                            runCommand(cmd: u.Message.Text, msg: u.Message, upd: u);
                        }
                    }
                    if (u.CallbackQuery != null)    //If the update is a callback query
                    {
                        callbacks.Add(u.CallbackQuery);
                        string[] args = u.CallbackQuery.Data.Split(',');
                        foreach (Game g in games)
                        {
                            if (args[0] == g.Group.Id.ToString())
                            {
                                if (args[1] == "multiple")
                                {
                                    g.AllCalledBackData.Add(args);
                                }
                                else if (args[1] == "process")
                                {
                                    if (args[2] == "getFrom")
                                    {
                                        args[2] = u.CallbackQuery.From.Id.ToString();
                                    }
                                    g.AllCalledBackDataToProcess.Add(args);
                                }
                                else if (args[1] == "numbered")
                                {
                                    args[1] = g.AllCalledBackData.Count.ToString();
                                    if (args[2] == "getFrom")
                                    {
                                        args[2] = u.CallbackQuery.From.Id.ToString();
                                    }
                                    g.AllCalledBackData.Add(args);
                                }
                                else
                                {
                                    g.CalledBackData = args;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool hasPermission(long id)
        {
            foreach (long l in permissionIds)
            {
                if (l == id) return true;
            }
            return false;
        }

        private static void runCommand(string cmd, Message msg, Update upd)
        {
            switch (cmd)
            {
                case "/startgame":
                case "/startgame" + botUsername:
                    if (hasPermission(msg.From.Id))
                    {
                        if (msg.Chat.Id < 0)
                        {
                            bool contains = false;
                            foreach (Chat c in groups)
                            {
                                if (c.Id == msg.Chat.Id) contains = true;
                            }
                            if (contains)
                            {
                                foreach (Game g in games)
                                {
                                    if (g.Group.Id == msg.Chat.Id)
                                    {
                                        g.addPlayer(new Player(msg.From), msg);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Game g = new Game(msg.Chat);
                                games.Add(g);
                                groups.Add(msg.Chat);
                                Program.sendMessage(txt: "Spiel wurde gestartet. Nutzt /join um beizutreten und /go um das Spiel zu beginnen.",
                                    chatid: msg.Chat.Id, replyto: msg);
                                g.addPlayer(new Player(msg.From), msg);
                            }
                        }
                        else
                        {
                            sendMessage(txt: "Dieser Befehl funktioniert nur in einer Gruppe!", chatid: msg.Chat.Id, replyto: msg);
                        }
                    }
                    else
                    {
                        sendMessage(txt: "Leider habe ich vom Verlag nur die Erlaubnis bekommen, den Bot privat zu nutzen, deshalb " +
                            "muss ich die Erlaubnis leider persönlich vergeben.", chatid: msg.Chat.Id, replyto: msg);
                    }
                    break;
                case "/join":
                case "/join" + botUsername:
                    if (hasPermission(msg.From.Id))
                    {
                        bool foundGame = false;
                        foreach (Game g in games)
                        {
                            if (g.Group.Id == msg.Chat.Id)
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
                    }
                    else
                    {
                        sendMessage(txt: "Leider habe ich vom Verlag nur die Erlaubnis bekommen, den Bot privat zu nutzen, deshalb " +
                            "muss ich die Erlaubnis leider persönlich vergeben.", chatid: msg.Chat.Id, replyto: msg);
                    }
                    break;
                case "/go":
                case "/go" + botUsername:
                    if (hasPermission(msg.From.Id))
                    {
                        bool fGame = false;
                        foreach (Game g in games)
                        {
                            if (g.Group.Id == msg.Chat.Id)
                            {
                                if (3 <= g.Players.Count)
                                {
                                    g.start();
                                }
                                else
                                {
                                    sendMessage(txt: "Es sind noch nicht genügend Spieler im Spiel, mindestens drei müssen es sein.",
                                        chatid: msg.Chat.Id, replyto: msg);
                                }
                                fGame = true;
                                break;
                            }
                        }
                        if (!fGame)
                        {
                            sendMessage(txt: "Es läft gerade kein Spiel. Starte ein neues Spiel mit /startgame",
                                chatid: msg.Chat.Id, replyto: msg);
                        }
                    }
                    else
                    {
                        sendMessage(txt: "Leider habe ich vom Verlag nur die Erlaubnis bekommen, den Bot privat zu nutzen, deshalb " +
                            "muss ich die Erlaubnis leider persönlich vergeben.", chatid: msg.Chat.Id, replyto: msg);
                    }
                    break;
                case "/anleitung":
                case "/anleitung" + botUsername:
                    InlineKeyboardButton b = new InlineKeyboardButton("Anleitung");
                    b.Url = "www.halt-mal-kurz.de";
                    InlineKeyboardButton[] bs = { b };
                    InlineKeyboardMarkup im = new InlineKeyboardMarkup(bs);
                    sendMessage(txt: "Hier ist die Anleitung.", chatid: msg.Chat.Id, replyto: msg, inlineMarkup: im);
                    break;
                case "!addpermission":
                    if (msg.From.Id == flomsId)
                    {
                        if (msg.ReplyToMessage != null)
                        {
                            addIdToPermissionList(msg.ReplyToMessage.From.Id);
                            sendMessage(txt: "Erlaubnis hinzugefügt.", chatid: msg.Chat.Id, replyto: msg);
                        }
                        else
                        {
                            sendMessage(txt: "Antworte auf eine Nachricht", chatid: msg.Chat.Id, replyto: msg);
                        }
                    }
                    else
                    {
                        sendMessage(txt: "Du bist nicht Flom!", chatid: msg.Chat.Id, replyto: msg);
                    }
                    break;
                default:
                    break;
            }
        }
        private static void addIdToPermissionList(long id)
        {
            if (!permissionIds.Contains(id))
            {
                permissionIds.Add(id);
                writePermissionFile();
            }
        }
        #endregion
        #region File Methods
        private static void writePermissionFile()
        {
            if (!System.IO.File.Exists(projectPath + "permission.txt"))
            {
                System.IO.File.Create(projectPath + "permission.txt");
            }
            System.IO.File.WriteAllText(projectPath + "permission.txt", "", System.Text.Encoding.UTF8);
            foreach (long l in permissionIds)
            {
                System.IO.File.AppendAllText(projectPath + "permission.txt", l + "\n");
            }
        }
        private static void readPermissionFile()
        {
            try
            {
                if (!System.IO.File.Exists(projectPath + "permission.txt"))
                {
                    System.IO.File.Create(projectPath + "permission.txt");
                }
                using (StreamReader sr = new StreamReader(projectPath + "permission.txt", System.Text.Encoding.UTF8))
                {
                    string s;
                    Console.WriteLine("Permissions are being loaded...");
                    do
                    {
                        s = sr.ReadLine();
                        if (s != null)
                        {
                            permissionIds.Add((long) Convert.ToInt32(s));
                            Console.WriteLine(s);
                        }
                    } while (s != null);
                    Console.WriteLine("Permissions loaded.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
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

        public static bool answerCallbackQuery(CallbackQuery q)
        {
            editMessageReplyMarkup(chatid: q.Message.Chat.Id, messageid: q.Message.MessageId, inlineMarkup: null);
            string append = "answerCallbackQuery?callback_query_id=" + q.Id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + append);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                return DecodeRaw<bool>(resStream).Ok;
            }
            catch
            {
                return false;
            }
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
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                return DecodeRaw<Message>(resStream).Ok;
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static Message sendAndReturnMessage(string txt, long chatid, Message replyto = null, string parsemode = null, InlineKeyboardMarkup inlineMarkup = null)
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
            return Decode<Message>(resStream);
        }

        public static bool sendSticker(long chatid, string sticker, InlineKeyboardMarkup inlineMarkup = null)
        {
            string append = "sendSticker?chat_id=" + chatid;
            append += "&sticker=" + sticker;
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

        public static bool editMessageReplyMarkup(long chatid, int messageid, InlineKeyboardMarkup inlineMarkup)
        {
            string append = "editMessageReplyMarkup?chat_id=" + chatid.ToString();
            if (inlineMarkup != null)
            {
                append += "&reply_markup=" + JsonConvert.SerializeObject(inlineMarkup);
            }
            append += "&message_id=" + messageid;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + append);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            return DecodeRaw<Message>(resStream).Ok;
        }

        public static bool editMessage(long chatid, int messageid, string txt, InlineKeyboardMarkup inlineMarkup = null)
        {
            string append = "editMessageText?chat_id=" + chatid.ToString();
            append += "&message_id=" + messageid;
            append += "&text=" + txt;
            if (inlineMarkup != null)
            {
                append += "&reply_markup=" + JsonConvert.SerializeObject(inlineMarkup);
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + append);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            return DecodeRaw<Message>(resStream).Ok;
        }

        public static Message editAndReturnMessage(long chatid, int messageid, string txt, InlineKeyboardMarkup inlineMarkup = null)
        {
            string append = "editMessageText?chat_id=" + chatid.ToString();
            append += "&message_id=" + messageid;
            append += "&text=" + txt;
            if (inlineMarkup != null)
            {
                append += "&reply_markup=" + JsonConvert.SerializeObject(inlineMarkup);
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + append);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            return Decode<Message>(resStream);
        }
        #endregion
        #endregion
    }
    #endregion

    #region Custom Classes

    #region Card
#pragma warning disable CS0660 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.Equals(Objekt o)
#pragma warning disable CS0661 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.GetHashCode()
    class Card
#pragma warning restore CS0661 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.GetHashCode()
#pragma warning restore CS0660 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.Equals(Objekt o)
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

        #region Operators
        public static bool operator == (Card c1, Card c2)
        {
            try
            {
                return (c1.ColorId == c2.ColorId && c1.SymbolId == c2.SymbolId && c1.TypeId == c2.TypeId);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool operator != (Card c1, Card c2)
        {
            try
            {
                return !(c1.ColorId == c2.ColorId && c1.SymbolId == c2.SymbolId && c1.TypeId == c2.TypeId);
            }
            catch (NullReferenceException)
            {
                return true;
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
        private bool initialized = false;

        #region Constructor
        public CardStack(CardPile pile)
        {
            Cards = new ArrayList();
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
            initialized = true;
        }
        #endregion

        #region Methods
        public void addCard(Card card)
        {
            Cards.Add(card);
            if (initialized) shuffle();
        }

        public void shuffle()
        {
            if (Cards.Count < 1)
            {
                renewStack();
            }
            object[] objArray = Cards.ToArray();
            Card[] oldCard = new Card[objArray.Length];
            for (int i = 0; i < objArray.Length; i++)
            {
                oldCard[i] = (Card)objArray[i];
            }
            Card[] newCard = new Card[oldCard.Length];
            ArrayList usedInt = new ArrayList();
            int r;
            for (int i = 0; i < oldCard.Length; i++)
            {
                do
                {
                    r = rnd.Next(oldCard.Length);
                } while (usedInt.IndexOf(r) > -1);
                newCard[r] = oldCard[i];
                usedInt.Add(r);
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
        public CardPile()
        {
            Cards = new ArrayList();
        }
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

        public CardHand()
        {
            Cards = new ArrayList();
        }
        public void addCard(Card c)
        {
            Cards.Add(c);
        }

        public Card takeCard(Card c)
        {
            Card returnCard = null;
            foreach (Card c2 in Cards)
            {
                if (c2 == c) returnCard = c2;
            }
            Cards.Remove(returnCard);
            return returnCard;
        }
    }
    #endregion

    #region Game

    class Game
    {
        #region Variables
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
        public int Runde { get; set; }
        public ArrayList AllCalledBackData { get; set; }
        public ArrayList AllCalledBackDataToProcess { get; set; }
        private Random rnd = new Random();
        #endregion
        #region Constructor
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
            Runde = 1;
            Players = new ArrayList();
            allPlayers = new ArrayList();
            AllCalledBackData = new ArrayList();
            Pile = new CardPile();
            Stack = new CardStack(Pile);
        }
        #endregion
        #region Destructor
        ~Game()
        {
            Program.gameIds.Remove(Id);
            Program.games.Remove(this);
            Program.groups.Remove(Group);
        }
        #endregion
        #region Method Stuff
        public bool addPlayer(Player p, Message m)
        {
            if (!running)
            {
                if(Program.sendMessage(txt: "Du bist einem Spiel beigetreten", chatid: p.User.Id))
                {
                    Players.Add(p);
                    p.Group = Group;
                    p.GameIn = this;
                    Program.sendMessage(txt: p.FullName + " ist dem Spiel beigetreten. Es sind jetzt *" + Players.Count +
                        "* Spieler. Mit *3* bis *5* Spielern kann das Spiel beginnen!", chatid: Group.Id, parsemode: "Markdown");
                    if (Players.Count == 5)
                    {
                        start();
                    }
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

        private bool processVollversammlung(out Player p, int timeInSeconds = 30)
        {
            ArrayList allArgs = new ArrayList();
            for (int i = 0; i < timeInSeconds * 2; i++)
            {
                foreach (string[] args in AllCalledBackDataToProcess)
                {
                    AllCalledBackDataToProcess.Remove(args);
                    allArgs.Add(args);
                    string playerNameVoteFrom = "";
                    string playerNameVotedFor = "";
                    foreach (Player p2 in Players)
                    {
                        if (p2.Id.ToString() == args[2])
                        {
                            playerNameVoteFrom = p2.FullName;
                        }
                        if (p2.Id.ToString() == args[3])
                        {
                            playerNameVotedFor = p2.FullName;
                        }
                    }
                    Program.sendMessage(txt: playerNameVoteFrom + " hat für " + playerNameVotedFor + " gestimmt.", chatid: Group.Id);
                }
                Thread.Sleep(500);
            }
            bool remis = false;
            int maxNumberOfVotes = -1;
            foreach (Player p2 in Players)
            {
                int votesForThisPlayer = 0;
                foreach (string[] s in allArgs)
                {
                    if (s[3] == p2.Id.ToString()) votesForThisPlayer++;
                }
                if (votesForThisPlayer > maxNumberOfVotes)
                {
                    maxNumberOfVotes = votesForThisPlayer;
                    remis = false;
                }
                else if (votesForThisPlayer == maxNumberOfVotes) remis = true;
            }
            if (remis)
            {
                p = null;
                return false;
            }
            else
            {
                foreach (Player p2 in Players)
                {
                    int votesForThisPlayer = 0;
                    foreach (string[] s in allArgs)
                    {
                        if (s[3] == p2.Id.ToString()) votesForThisPlayer++;
                    }
                    if (votesForThisPlayer == maxNumberOfVotes)
                    {
                        p = p2;
                        return true;
                    }
                }
            }
            p = null;
            return false;
        }

        public void start()
        {
            running = true;
            int c = 10 - Players.Count;
            allPlayers = Players;
            Program.sendMessage(txt: "Karten werden ausgeteilt...", chatid: Group.Id);
            foreach (Player p in Players)
            {
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
        private void waitForCallback(int timeInSeconds = 30)
        {
            for (int i = 0; i < timeInSeconds * 2; i++)
            {
                //Wait for callback
                if (CalledBackData != null)
                {
                    return;
                }
                Thread.Sleep(500);
            }
            string[] s = { "Timeout" };
            CalledBackData = s;
        }

        private void waitForBoxCallback(int timeInSeconds = 30)
        {
            for (int i = 0; i < timeInSeconds * 2; i++)
            {
                //Wait for callback
                if (AllCalledBackData.Count >= Players.Count)
                {
                    return;
                }
                Thread.Sleep(500);
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
                msg += "\n" + p.FullName + " (" + p.Hand.CardCount + " Karten)";
            }
            Program.sendMessage(txt: msg, chatid: Group.Id);
            Runde++;
        }

        public void removePlayer(Player p)
        {
            Players.Remove(p);
            Program.sendMessage(txt: p.FullName + " hat das Spiel verlassen", chatid: Group.Id);
        }
        #endregion
        #region Game Thread
        public void gameThread()
        {
            Card firstCard = null;
            do
            {
                firstCard = Stack.drawCard();
                if (firstCard.TypeId == Type.IdRazupaltuff) Stack.addCard(firstCard);
            } while (firstCard.TypeId == Type.IdRazupaltuff);
            Pile.addCard(firstCard);
            Program.sendMessage(txt: "Das Spiel beginnt!\nMomentan hat jeder " + TurnPlayer.Hand.CardCount + " Karten",
                chatid: Group.Id);
            //send the sticker
            while (running)
            {
                Player turnPlayer = TurnPlayer;
                ArrayList messages = new ArrayList();
                foreach (Player p in Players)
                {
                    messages.Add(Program.sendAndReturnMessage(txt: "Runde " + Runde, chatid: p.Id));
                }
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
                    turnPlayer.Hand.takeCard(selectedCard);
                    Pile.addCard(selectedCard);
                    //play Card
                    switch (selectedCard.TypeId)
                    {
                        #region Ach, mein, dein...
                        case Type.RawIdAchMeinDein:
                            switch (selectedCard.SymbolId)
                            {
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.achMeinDeinKleinkünstlerFileId);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.achMeinDeinKänguruFileId);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.achMeinDeinPinguinFileId);
                                    break;
                            }
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
                                    + " Karten zu tauschen.\nEr / Sie hat zehn Sekunden Zeit, eine Not-To-Do-Liste einzusetzen.", 
                                    chatid: Group.Id);
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
                            switch (selectedCard.SymbolId)
                            {
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.gruppenSchnickSchnackSchnuckKleinkünstlerFileId);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.gruppenSchnickSchnackSchnuckKänguruFileId);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.gruppenSchnickSchnackSchnuckPinguinFileId);
                                    break;
                            }
                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Gruppen-Schnick!", chatid: Group.Id, 
                                parsemode: "Markdown");
                            Message msg = null;
                            foreach (Message m in messages)
                            {
                                InlineKeyboardButton button = new InlineKeyboardButton("Ohne Brunnen!", Group.Id + ",ohnebrunnen," 
                                    + m.Chat.FirstName + " " + m.Chat.LastName);
                                InlineKeyboardButton[] buttons = { button };
                                InlineKeyboardMarkup im = new InlineKeyboardMarkup(buttons);
                                msg = Program.editAndReturnMessage(chatid: m.Chat.Id, messageid: m.MessageId, txt: "Ohne Brunnen?",
                                    inlineMarkup: im);
                            }
                            waitForCallback(10);
                            string[] args = CalledBackData;
                            CalledBackData = null;
                            bool ohneBrunnen = false;
                            if (!(args[0] == "Timeout") && args[1] == "ohnebrunnen")
                            {
                                Program.sendMessage(txt: "*" + args[2] + ":* Ohne Brunnen!", chatid: Group.Id, parsemode: "Markdown");
                                ohneBrunnen = true;
                            }
                            messages = null;
                            foreach (Player p in Players)
                            {
                                InlineKeyboardButton b1 = new InlineKeyboardButton("Schere", Group.Id + ",multiple,schere," + p.Id);
                                InlineKeyboardButton[] bs1 = { b1 };
                                InlineKeyboardButton b2 = new InlineKeyboardButton("Stein", Group.Id + ",multiple,stein," + p.Id);
                                InlineKeyboardButton[] bs2 = { b2 };
                                InlineKeyboardButton b3 = new InlineKeyboardButton("Papier", Group.Id + ",multiple,papier," + p.Id);
                                InlineKeyboardButton[] bs3 = { b3 };
                                InlineKeyboardMarkup im;
                                if (!ohneBrunnen)
                                {
                                    InlineKeyboardButton b4 = new InlineKeyboardButton("Brunnen", Group.Id + ",multiple,brunnen," + p.Id);
                                    InlineKeyboardButton[] bs4 = { b4 };
                                    InlineKeyboardButton[][] buttons = { bs1, bs2, bs3, bs4 };
                                    im = new InlineKeyboardMarkup(buttons);
                                }
                                else
                                {
                                    InlineKeyboardButton[][] buttons = { bs1, bs2, bs3 };
                                    im = new InlineKeyboardMarkup(buttons);
                                }
                                messages.Add(Program.sendAndReturnMessage(txt: "Schere, Stein oder Papier?", chatid: p.Id, inlineMarkup: im));
                            }
                            Thread.Sleep(15 * 1000);
                            ArrayList argsList = AllCalledBackData;
                            AllCalledBackData = null;
                            //handle those arguments, determine winners and losers and so on... sigh
                            string[] turnPlayerArgs = null;
                            foreach (string[] s in argsList)
                            {
                                if (s[3] == turnPlayer.Id.ToString())
                                {
                                    turnPlayerArgs = s;
                                    argsList.Remove(s);
                                }
                            }
                            ArrayList playerIdsWonAgainst = new ArrayList();
                            ArrayList playerIdsLostAgainst = new ArrayList();
                            switch (turnPlayerArgs[2])
                            {
                                case "schere":
                                    Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Schere!", chatid: Group.Id, 
                                        parsemode: "Markdown");
                                    foreach (string[] arg2 in argsList)
                                    {
                                        switch (arg2[2])
                                        {
                                            case "schere":
                                                break;
                                            case "stein":
                                                playerIdsLostAgainst.Add(arg2[3]);  //still string
                                                break;
                                            case "papier":
                                                playerIdsWonAgainst.Add(arg2[3]);
                                                break;
                                            case "brunnen":
                                                playerIdsLostAgainst.Add(arg2[3]);
                                                break;
                                        }
                                    }
                                    break;
                                case "stein":
                                    foreach (string[] arg2 in argsList)
                                    {
                                        switch (arg2[2])
                                        {
                                            case "schere":
                                                playerIdsWonAgainst.Add(arg2[3]);
                                                break;
                                            case "stein":
                                                break;
                                            case "papier":
                                                playerIdsLostAgainst.Add(arg2[3]);
                                                break;
                                            case "brunnen":
                                                playerIdsLostAgainst.Add(arg2[3]);
                                                break;
                                        }
                                    }
                                    break;
                                case "papier":
                                    foreach (string[] arg2 in argsList)
                                    {
                                        switch (arg2[2])
                                        {
                                            case "schere":
                                                playerIdsLostAgainst.Add(arg2[3]);
                                                break;
                                            case "stein":
                                                playerIdsWonAgainst.Add(arg2[3]);
                                                break;
                                            case "papier":
                                                break;
                                            case "brunnen":
                                                playerIdsWonAgainst.Add(arg2[3]);
                                                break;
                                        }
                                    }
                                    break;
                                case "brunnen":
                                    foreach (string[] arg2 in argsList)
                                    {
                                        switch (arg2[2])
                                        {
                                            case "schere":
                                                playerIdsWonAgainst.Add(arg2[3]);
                                                break;
                                            case "stein":
                                                playerIdsWonAgainst.Add(arg2[3]);
                                                break;
                                            case "papier":
                                                playerIdsLostAgainst.Add(arg2[3]);
                                                break;
                                            case "brunnen":
                                                break;
                                        }
                                    }
                                    break;
                            }
                            foreach (string s in playerIdsLostAgainst)
                            {
                                foreach (Player p in Players)
                                {
                                    if (p.Id.ToString() == s)
                                    {
                                        Program.sendMessage(txt: turnPlayer.FullName + " hat gegen " + p.FullName +
                                            " verloren und erhält daher eine Karte von ihm / ihr.", chatid: Group.Id);
                                        Card c = p.selectCardNoDraw(p.Hand.Cards, "Wähle eine Karte, die du " + turnPlayer.FullName
                                            + " geben willst.", 10);
                                        if (c == null)
                                        {
                                            Program.sendMessage(txt: p.FullName + " hat es wohl verpasst, eine Karte zu wählen...\n"
                                                + "Pech gehabt!", chatid: Group.Id);
                                        }
                                        else
                                        {
                                            p.Hand.takeCard(c);
                                            turnPlayer.Hand.addCard(c);
                                            p.tellCards();
                                            turnPlayer.tellCards();
                                        }
                                    }
                                }
                            }
                            foreach (string s in playerIdsWonAgainst)
                            {
                                foreach (Player p in Players)
                                {
                                    if (p.Id.ToString() == s)
                                    {
                                        Program.sendMessage(txt: turnPlayer.FullName + " hat gegen " + p.FullName +
                                            " gewonnen und darf ihm / ihr eine Karte geben.\n Es sei denn, er / sie setzt eine Not-To-Do-Liste ein.", 
                                            chatid: Group.Id);
                                        bool blocked = false;
                                        Card notToDo = null;
                                        if (p.hasNotToDoList(Pile))
                                        {
                                            notToDo = p.askNotToDoList(out blocked, Pile);
                                        }
                                        if (blocked)
                                        {
                                            Program.sendMessage(txt: "*" + p.FullName + ":* Das steht auf meiner Not-To-Do-Liste!",
                                                chatid: Group.Id, parsemode: "Markdown");
                                            Card taken = p.Hand.takeCard(notToDo);
                                            Pile.addCard(taken);
                                            p.drawCard(Stack);
                                            continue;
                                        }
                                        Card c = turnPlayer.selectCardNoDraw(turnPlayer.Hand.Cards, "Wähle eine Karte, die du " + p.FullName
                                            + " geben willst.", 10);
                                        if (c == null)
                                        {
                                            Program.sendMessage(txt: turnPlayer.FullName + " hat es wohl verpasst, eine Karte zu wählen...\n"
                                                + "Pech gehabt!", chatid: Group.Id);
                                        }
                                        else
                                        {
                                            turnPlayer.Hand.takeCard(c);
                                            p.Hand.addCard(c);
                                            p.tellCards();
                                            turnPlayer.tellCards();
                                        }
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region Halt mal kurz
                        case Type.RawIdHaltMalKurz:
                            switch (selectedCard.SymbolId)
                            {
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.haltMalKurzKleinkünstlerFileId);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.haltMalKurzKänguruFileId);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.haltMalKurzPinguinFileId);
                                    break;
                            }
                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Halt mal kurz!", chatid: Group.Id);
                            ArrayList selection = Players;
                            selection.Remove(turnPlayer);
                            Player sp = turnPlayer.selectPlayer(selection, "Wähle einen Spieler, der "
                                + "mal kurz deine Karten halten soll.");
                            if (sp == null)
                            {
                                Program.sendMessage(txt: "Sieht so aus, als hätte " + turnPlayer.FullName + " keine Karte ausgewählt!",
                                    chatid: Group.Id);
                                turn--;
                                removePlayer(turnPlayer);
                                checkRoutine();
                                continue;
                            }
                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Halt mal kurz, " + sp.FullName + "!",
                                chatid: Group.Id, parsemode: "Markdown");
                            if (sp.hasNotToDoList(Pile))
                            {
                                bool blocked;
                                Card notToDo = sp.askNotToDoList(out blocked, Pile);
                                if (blocked)
                                {
                                    Program.sendMessage(txt: "*" + sp.FullName + ":* Das steht auf meiner Not-To-Do-Liste!",
                                        chatid: Group.Id, parsemode: "Markdown");
                                    Pile.addCard(sp.Hand.takeCard(notToDo));
                                    sp.drawCard(Stack);
                                    checkRoutine();
                                    continue;
                                }
                            }
                            ArrayList cardsToGive = new ArrayList();
                            int cc = turnPlayer.Hand.CardCount;
                            for (int i = 0; i < cc/2; i++)
                            {
                                Card c = (Card) turnPlayer.Hand.Cards[rnd.Next(cc)];
                                cardsToGive.Add(turnPlayer.Hand.takeCard(c));
                            }
                            foreach (Card c in cardsToGive)
                            {
                                sp.Hand.addCard(c);
                            }
                            sp.tellCards();
                            turnPlayer.tellCards();
                            break;
                        #endregion
                        #region Kapitalismus
                        case Type.RawIdKapitalismus:
                            switch (selectedCard.SymbolId)
                            {
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.kapitalismusKleinkünstlerFileId);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.kapitalismusKänguruFileId);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.kapitalismusPinguinFileId);
                                    break;
                            }
                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Wer hat, dem wird gegeben!", chatid: Group.Id,
                                parsemode: "Markdown");
                            int maxCards = 0;
                            foreach (Player p in Players)
                            {
                                if (p.Hand.CardCount > maxCards)
                                {
                                    maxCards = p.Hand.CardCount;
                                }
                            }
                            foreach (Player p in Players)
                            {
                                if (p.Hand.CardCount == maxCards)
                                {
                                    Program.sendMessage(txt: p.FullName + " muss zwei Karten ziehen!", chatid: Group.Id);
                                    if (p.hasNotToDoList(Pile))
                                    {
                                        bool blocked;
                                        Card notToDo = p.askNotToDoList(out blocked, Pile);
                                        if (blocked)
                                        {
                                            Program.sendMessage(txt: "*" + p.FullName + ":* Das steht auf meiner Not-To-Do-Liste!",
                                                chatid: Group.Id, parsemode: "Markdown");
                                            Pile.addCard(p.Hand.takeCard(notToDo));
                                            p.drawCard(Stack);
                                            continue;
                                        }
                                    }
                                    p.drawCard(Stack);
                                    p.drawCard(Stack);
                                }
                            }
                            break;
                        #endregion
                        #region Kommunismus
                        case Type.RawIdKommunismus:
                            Program.sendSticker(chatid: Group.Id, sticker: Program.kommunismusFileId);
                            ArrayList playersToRemix = Players;
                            ArrayList playersToAsk = Players;
                            playersToAsk.Remove(turnPlayer);
                            ArrayList messagesSent = new ArrayList();
                            foreach (Player p in playersToAsk)
                            {
                                if (p.hasNotToDoList(Pile))
                                {
                                    InlineKeyboardButton button = new InlineKeyboardButton("Not-To-Do-Liste verwenden",
                                        Group.Id + ",multiple,nope," + p.Id);
                                    InlineKeyboardButton[] buttons = { button };
                                    InlineKeyboardMarkup markup = new InlineKeyboardMarkup(buttons);
                                    messagesSent.Add(Program.sendAndReturnMessage(txt: "Willst du eine Not-To-Do-Liste verwenden?",
                                        chatid: p.Id, inlineMarkup: markup));
                                }
                            }
                            Thread.Sleep(10 * 1000);
                            ArrayList allArgs = AllCalledBackData;
                            AllCalledBackData = null;
                            foreach (string[] s in allArgs)
                            {
                                foreach (Player p in playersToAsk)
                                {
                                    if (s[3] == p.Id.ToString())
                                    {
                                        bool blocked;
                                        Card c = p.askNotToDoList(out blocked, Pile, nono: true);
                                        Program.sendMessage(txt: "*" + p.FullName + ":* Das steht auf meiner Not-To-Do-Liste!",
                                            chatid: Group.Id, parsemode: "Markdown");
                                        playersToRemix.Remove(p);
                                        Pile.addCard(p.Hand.takeCard(c));
                                        p.drawCard(Stack);
                                    }
                                }
                            }
                            ArrayList cardsCollected = new ArrayList();
                            foreach (Player p in playersToRemix)
                            {
                                foreach (Card c in p.Hand.Cards)
                                {
                                    cardsCollected.Add(p.Hand.takeCard(c));
                                }
                            }
                            int cardsReturned = 0;
                            foreach (Card c in cardsCollected)
                            {
                                Player p = (Player)playersToRemix[cardsReturned % playersToRemix.Count];
                                p.Hand.addCard(c);
                            }
                            foreach (Player p in playersToRemix)
                            {
                                p.tellCards();
                            }
                            break;
                        #endregion
                        #region Nazi
                        case Type.RawIdNazi:
                            InlineKeyboardButton b = new InlineKeyboardButton("Boxen!", Group.Id + ",numbered,getFrom,box");
                            InlineKeyboardButton[] bs = { b };
                            InlineKeyboardMarkup m1 = new InlineKeyboardMarkup(bs);
                            switch (selectedCard.SymbolId)
                            {
                                //add button "Boxen!" to the stickers
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.naziKleinkünstlerFileId, inlineMarkup: m1);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.naziKänguruFileId, inlineMarkup: m1);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.naziPinguinFileId, inlineMarkup: m1);
                                    break;
                            }
                            waitForBoxCallback(10);
                            ArrayList argList = AllCalledBackData;
                            AllCalledBackData = null;
                            bool playerFailed = false;
                            ArrayList playersFailed = new ArrayList();
                            foreach (Player p in Players)
                            {
                                bool successful = false;
                                foreach (string[] s in argList)
                                {
                                    if (s[2] == p.Id.ToString()) successful = true;
                                }
                                if (!successful)
                                {
                                    playersFailed.Add(p);
                                    playerFailed = true;
                                }
                            }
                            if (playerFailed)
                            {
                                foreach (Player p in playersFailed)
                                {
                                    Program.sendMessage(txt: p.FullName +
                                        " hat nicht rechtzeitig auf den Nazi geschlagen! Er / sie muss eine Karte ziehen!",
                                        chatid: Group.Id);
                                    p.drawCard(Stack);
                                }
                                checkRoutine();
                                continue;
                            }
                            else
                            {
                                foreach (string[] s in argList)
                                {
                                    if (s[1] == (Players.Count - 1).ToString())
                                    {
                                        foreach (Player p in Players)
                                        {
                                            if (p.Id.ToString() == s[2])
                                            {
                                                Program.sendMessage(txt: p.FullName +
                                                    " hat als Letzter auf den Nazi geschlagen! Er / sie muss eine Karte ziehen!",
                                                    chatid: Group.Id);
                                                p.drawCard(Stack);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region Not-To-Do-Liste
                        case Type.RawIdNotToDoListe:
                            switch (selectedCard.SymbolId)
                            {
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.notToDoListeKleinkünstlerFileId);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.notToDoListeKänguruFileId);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.notToDoListePinguinFileId);
                                    break;
                            }
                            break;
                        #endregion
                        #region Polizei
                        case Type.RawIdPolizei:
                            InlineKeyboardButton b5 = new InlineKeyboardButton("Boxen!", Group.Id + ",numbered,getFrom,box");
                            InlineKeyboardButton[] bs5 = { b5 };
                            InlineKeyboardMarkup m5= new InlineKeyboardMarkup(bs5);
                            switch (selectedCard.SymbolId)
                            {
                                //add button "Boxen!" to the stickers
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.polizeiKleinkünstlerFileId, inlineMarkup: m5);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.polizeiKänguruFileId, inlineMarkup: m5);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.polizeiPinguinFileId, inlineMarkup: m5);
                                    break;
                            }
                            waitForBoxCallback(10);
                            ArrayList policeArgs = AllCalledBackData;
                            AllCalledBackData = null;
                            foreach (string[] s in policeArgs)
                            {
                                foreach (Player p in Players)
                                {
                                    if (p.Id.ToString() == s[2])
                                    {
                                        Program.sendMessage(txt: p.FullName +
                                            " hat die Polizei geschlagen! Das gibt STRESS... Eine Karte ziehen!", chatid: Group.Id);
                                        p.drawCard(Stack);
                                    }
                                }
                            }
                            break;
                        #endregion
                        #region SchnickSchnackSchnuck
                        case Type.RawIdSchnickSchnackSchnuck:
                            switch (selectedCard.SymbolId)
                            {
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.schnickSchnackSchnuckKleinkünstlerFileId);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.schnickSchnackSchnuckKänguruFileId);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.schnickSchnackSchnuckPinguinFileId);
                                    break;
                            }
                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Schnick-Schnack-Schnuck!", chatid: Group.Id,
                                parsemode: "Markdown");
                            ArrayList whodoyouwant = Players;
                            whodoyouwant.Remove(turnPlayer);
                            Player selec = turnPlayer.selectPlayer(whodoyouwant,
                                "Wähle einen Spieler, mit dem du Schnick-Schnack-Schnuck spielen willst");
                            if (selec == null)
                            {
                                Program.sendMessage(txt: turnPlayer.FullName + " hat nichts ausgewählt.", chatid: Group.Id);
                                Players.Remove(turnPlayer);
                                turn--;
                                checkRoutine();
                                continue;
                            }
                            Program.sendMessage(txt: turnPlayer.FullName + " hat " + selec.FullName + " herausgefordert.",
                                chatid: Group.Id);
                            foreach (Message m in messages)
                            {
                                if (m.Chat.Id == turnPlayer.Id || m.Chat.Id == selec.Id)
                                {
                                    InlineKeyboardButton button = new InlineKeyboardButton("Ohne Brunnen!", Group.Id + ",ohnebrunnen,"
                                        + m.Chat.FirstName + " " + m.Chat.LastName);
                                    InlineKeyboardButton[] buttons = { button };
                                    InlineKeyboardMarkup im = new InlineKeyboardMarkup(buttons);
                                    msg = Program.editAndReturnMessage(chatid: m.Chat.Id, messageid: m.MessageId, txt: "Ohne Brunnen?",
                                        inlineMarkup: im);
                                }
                            }
                            waitForCallback(10);
                            string[] cb = CalledBackData;
                            CalledBackData = null;
                            bool noBrunnen = false;
                            if (!(cb[0] == "Timeout") && cb[1] == "ohnebrunnen")
                            {
                                Program.sendMessage(txt: "*" + cb[2] + ":* Ohne Brunnen!", chatid: Group.Id, parsemode: "Markdown");
                                noBrunnen = true;
                            }
                            messages = null;
                            int size = 3;
                            if (!noBrunnen) size++;
                            InlineKeyboardButton[][] butts = new InlineKeyboardButton[size][];
                            InlineKeyboardButton b6 = new InlineKeyboardButton("Schere", Group.Id + ",schere");
                            InlineKeyboardButton[] bs6 = { b6 };
                            butts[0] = bs6;
                            InlineKeyboardButton b7 = new InlineKeyboardButton("Stein", Group.Id + ",stein");
                            InlineKeyboardButton[] bs7 = { b7 };
                            butts[1] = bs7;
                            InlineKeyboardButton b8 = new InlineKeyboardButton("Papier", Group.Id + ",papier");
                            InlineKeyboardButton[] bs8 = { b8 };
                            butts[2] = bs8;
                            if (!noBrunnen)
                            {
                                InlineKeyboardButton b9 = new InlineKeyboardButton("Brunnen", Group.Id + ",brunnen");
                                InlineKeyboardButton[] bs9 = { b9 };
                                butts[3] = bs9;
                            }
                            InlineKeyboardMarkup ssp = new InlineKeyboardMarkup(butts);
                            bool hasWon = false;
                            bool remis = false;
                            bool cont = true;
                            do
                            {
                                Program.sendMessage(txt: "Schere, Stein oder Papier?", chatid: turnPlayer.Id, inlineMarkup: ssp);
                                waitForCallback(10);
                                string[] tpArgs = CalledBackData;
                                CalledBackData = null;
                                if (tpArgs == null)
                                {
                                    cont = false;
                                    Program.sendMessage(txt: turnPlayer.FullName + " hat nichts ausgewählt.", chatid: Group.Id);
                                    Players.Remove(turnPlayer);
                                }
                                Program.sendMessage(txt: "Schere, Stein oder Papier?", chatid: selec.Id, inlineMarkup: ssp);
                                waitForCallback(10);
                                string[] selecArgs = CalledBackData;
                                CalledBackData = null;
                                if (selecArgs == null)
                                {
                                    cont = false;
                                    Program.sendMessage(txt: selec.FullName + " hat nichts ausgewählt.", chatid: Group.Id);
                                    Players.Remove(selec);
                                }
                                hasWon = false;
                                remis = false;
                                if (cont)
                                {
                                    switch (tpArgs[1])
                                    {
                                        case "schere":
                                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Schere!", chatid: Group.Id,
                                                parsemode: "Markdown");
                                            if (selecArgs[1] == "stein" || selecArgs[1] == "brunnen") hasWon = false;
                                            if (selecArgs[1] == "papier") hasWon = true;
                                            if (selecArgs[1] == "schere") remis = true;
                                            break;
                                        case "stein":
                                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Stein!", chatid: Group.Id,
                                                parsemode: "Markdown");
                                            if (selecArgs[1] == "papier" || selecArgs[1] == "brunnen") hasWon = false;
                                            if (selecArgs[1] == "schere") hasWon = true;
                                            if (selecArgs[1] == "stein") remis = true;
                                            break;
                                        case "papier":
                                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Papier!", chatid: Group.Id,
                                                parsemode: "Markdown");
                                            if (selecArgs[1] == "schere") hasWon = false;
                                            if (selecArgs[1] == "stein" || selecArgs[1] == "brunnen") hasWon = true;
                                            if (selecArgs[1] == "brunnen") remis = true;
                                            break;
                                        case "brunnen":
                                            Program.sendMessage(txt: "*" + turnPlayer.FullName + ":* Brunnen!", chatid: Group.Id,
                                                parsemode: "Markdown");
                                            if (selecArgs[1] == "papier") hasWon = false;
                                            if (selecArgs[1] == "schere" || selecArgs[1] == "stein") hasWon = true;
                                            if (selecArgs[1] == "brunnen") remis = true;
                                            break;
                                    }
                                    string text = "";
                                    switch (selecArgs[1])
                                    {
                                        case "schere":
                                            text = "Schere";
                                            break;
                                        case "stein":
                                            text = "Stein";
                                            break;
                                        case "papier":
                                            text = "Papier";
                                            break;
                                        case "brunnen":
                                            text = "Brunnen";
                                            break;
                                    }
                                    Program.sendMessage(txt: "*" + selec.FullName + ":* " + text + "!", chatid: Group.Id, 
                                        parsemode: "Markdown");
                                }
                            } while (remis);
                            if (!cont)
                            {
                                turn--;
                                checkRoutine();
                                continue;
                            }
                            if (hasWon)
                            {
                                Program.sendMessage(txt: turnPlayer.FullName + " hat gewonnen. Gib " + selec.FullName + " eine Karte.",
                                    chatid: Group.Id);
                                Card c = turnPlayer.selectCardNoDraw(turnPlayer.Hand.Cards, "Welche Karte möchtest du " +
                                    selec.FullName + " geben?", 15);
                                if (c == null)
                                {
                                    Program.sendMessage(txt: turnPlayer.FullName + " hat nichts gewählt. Pech gehabt!",
                                        chatid: Group.Id);
                                }
                                else
                                {
                                    selec.Hand.addCard(turnPlayer.Hand.takeCard(c));
                                }
                            }
                            else
                            {
                                Program.sendMessage(txt: selec.FullName + " hat gewonnen. Gib " + turnPlayer.FullName + " eine Karte.",
                                    chatid: Group.Id);
                                Card c = selec.selectCardNoDraw(selec.Hand.Cards, "Welche Karte möchtest du " +
                                    turnPlayer.FullName + " geben?", 15);
                                if (c == null)
                                {
                                    Program.sendMessage(txt: selec.FullName + " hat nichts gewählt. Pech gehabt!",
                                        chatid: Group.Id);
                                }
                                else
                                {
                                    turnPlayer.Hand.addCard(selec.Hand.takeCard(c));
                                }
                            }
                            break;
                        #endregion
                        #region Vollversammlung
                        case Type.RawIdVollversammlung:
                            switch (selectedCard.SymbolId)
                            {
                                case Symbol.RawIdKleinkünstler:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.vollversammlungKleinkünstlerFileId);
                                    break;
                                case Symbol.RawIdKänguru:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.vollversammlungKänguruFileId);
                                    break;
                                case Symbol.RawIdPinguin:
                                    Program.sendSticker(chatid: Group.Id, sticker: Program.vollversammlungPinguinFileId);
                                    break;
                            }
                            //last card, yay!   Oh...   Well.   So...   We'll need to...    Hm.
                            //First, ask everyone who should lose a Card. Then, ask everyone who should take the card.
                            //Create a new field called CalledBackDataToProcess and a method that processes it
                            Program.sendMessage(txt: "Diese Karte ist noch nicht implementiert, weil das verdammt schwer wird!",
                                chatid: Group.Id);
                            Program.sendMessage(txt: "Die Vollversammlung hat begonnen! Zuerst wird ein Spieler gewählt," +
                                "der eine Karte abgeben soll, dann einer, der sie erhalten soll.", chatid: Group.Id);
                            Program.sendMessage(txt: "Es wird gewählt, wer eine Karte abgeben soll.", chatid: Group.Id);
                            foreach (Player p in Players)
                            {
                                ArrayList sel = Players;
                                sel.Remove(p);
                                InlineKeyboardButton[][] bss9 = new InlineKeyboardButton[sel.Count][];
                                int index = 0;
                                foreach (Player p2 in sel)
                                {
                                    InlineKeyboardButton b9 = new InlineKeyboardButton(p2.FullName, Group.Id + ",process,getFrom," + p2.Id);
                                    InlineKeyboardButton[] bs9 = { b9 };
                                    bss9[index] = bs9;
                                }
                                InlineKeyboardMarkup im = new InlineKeyboardMarkup(bss9);
                                Program.sendMessage(txt: "Welcher Spieler soll eine Karte abgeben?", chatid: p.Id, inlineMarkup: im);
                            }
                            Player playerToLoseCard;
                            Player playerToGainCard;
                            if (processVollversammlung(out playerToLoseCard))
                            {
                                Program.sendMessage(txt: "Es wird gewählt, wer die Karte erhalten soll.", chatid: Group.Id);
                                foreach (Player p in Players)
                                {
                                    InlineKeyboardButton[][] bss9 = new InlineKeyboardButton[Players.Count][];
                                    int index = 0;
                                    foreach (Player p2 in Players)
                                    {
                                        InlineKeyboardButton b9 = new InlineKeyboardButton(p2.FullName, Group.Id + ",process,getFrom," + p2.Id);
                                        InlineKeyboardButton[] bs9 = { b9 };
                                        bss9[index] = bs9;
                                    }
                                    InlineKeyboardMarkup im = new InlineKeyboardMarkup(bss9);
                                    Program.sendMessage(txt: "Welcher Spieler soll die Karte erhalten?", chatid: p.Id, inlineMarkup: im);
                                }
                            }
                            else
                            {
                                Program.sendMessage(txt: "Es wurde keine Entscheidung getroffen. Es passiert NÜSCHT.", chatid: Group.Id);
                                checkRoutine();
                                continue;
                            }
                            if (processVollversammlung(out playerToGainCard))
                            {
                                Program.sendMessage(txt: "Eine Entscheidung wurde getroffen. " + playerToLoseCard.FullName + " gibt " +
                                    playerToGainCard.FullName + " eine Karte.", chatid: Group.Id);
                                Card cardToLose = playerToLoseCard.selectCardNoDraw(playerToLoseCard.Hand.Cards, "Wähle eine Karte, die du "
                                    + playerToGainCard.FullName + " geben willst.");
                                playerToGainCard.Hand.addCard(playerToLoseCard.Hand.takeCard(cardToLose));
                            }
                            else
                            {
                                Program.sendMessage(txt: "Es wurde keine Entscheidung getroffen. Es passiert NÜSCHT.", chatid: Group.Id);
                                checkRoutine();
                                continue;
                            }
                            break;
                        #endregion
                    }
                    checkRoutine();
                }
                #endregion
            }
        }
        #endregion
    }
    #endregion

    #region Player

#pragma warning disable CS0660 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.Equals(Objekt o)
#pragma warning disable CS0661 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.GetHashCode()
    class Player
#pragma warning restore CS0661 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.GetHashCode()
#pragma warning restore CS0660 // Typ definiert Operator == oder Operator !=, überschreibt jedoch nicht Object.Equals(Objekt o)
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
        private int RazupaltuffCount { get; set; }
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
            RazupaltuffCount = 0;
            Hand = new CardHand();
        }

        #region Operators
        public static bool operator == (Player p1, Player p2)
        {
            try
            {
                return (p1.Id == p2.Id);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool operator != (Player p1, Player p2)
        {
            try
            {
                return !(p1.Id == p2.Id);
            }
            catch (NullReferenceException)
            {
                return true;
            }
        }
        #endregion

        #region Methods
        public void tellCards()
        {
            string message = "Deine Karten sind:";
            int razupaltuffsSeen = 0;
            foreach (Card c in Hand.Cards)
            {
                if (c == null) continue;
                message += "\n" + c.ColorEmoji + c.SymbolEmoji + " " + c.TypeName;
                if (c.TypeId == Type.IdRazupaltuff && razupaltuffsSeen >= RazupaltuffCount)
                {
                    Program.sendMessage(txt: "*" + FullName + ":* _Razupaltuff!_", chatid: Group.Id, parsemode: "Markdown");
                    razupaltuffsSeen++;
                    RazupaltuffCount++;
                }
                else if (c.TypeId == Type.IdRazupaltuff)
                {
                    razupaltuffsSeen++;
                }
            }
            Program.sendMessage(txt: message, chatid: Id);
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
            Message msg = Program.sendAndReturnMessage(txt: "Wähle eine Karte.", chatid: Id, inlineMarkup: im);
            waitForCallback(msg, 30);
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

        public Card selectCardNoDraw(ArrayList selection, string txt, int timeout = 30)
        {
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[selection.Count][];
            int i = 0;
            foreach (Card c in selection)
            {
                InlineKeyboardButton b = new InlineKeyboardButton(c.ColorEmoji + c.SymbolEmoji + " " + c.TypeName);
                b.CallbackData = Group.Id + "," + c.ColorId + "," + c.SymbolId + "," + c.TypeName;
                InlineKeyboardButton[] ba = { b };
                buttons[i] = ba;
                i++;
            }
            InlineKeyboardMarkup im = new InlineKeyboardMarkup(buttons);
            Message msg = Program.sendAndReturnMessage(txt: txt, chatid: Id, inlineMarkup: im);
            waitForCallback(msg, timeout);
            string[] args = CalledBackData;
            CalledBackData = null;
            if (args[0] == "Timeout")
            {
                return null;
            }
            else
            {
                foreach (Card c in Hand.Cards)
                {
                    if (c.ColorId.ToString() == args[1] && c.SymbolId.ToString() == args[2] && c.TypeId.ToString() == args[3])
                    {
                        return c;
                    }
                }
                return null;
            }
        }

        public void waitForCallback(Message msg, int timeInSeconds = 30)
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
            Message massage = Program.sendAndReturnMessage(txt: msg, chatid: Id, inlineMarkup: im);
            waitForCallback(massage, 30);
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

        public Card askNotToDoList(out bool yesIWant, CardPile pile, bool nono = false)
        {
            ArrayList selection = Hand.Cards;
            foreach (Card c in selection)
            {
                if (c.TypeId != Type.IdNotToDoListe || !c.fitsOn(pile.TopCard))
                {
                    selection.Remove(c);
                }
            }
            int count = selection.Count;
            if (!nono) count++;
            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[count][];
            int i = 0;
            foreach (Card c in selection)
            {
                InlineKeyboardButton b = new InlineKeyboardButton(c.ColorEmoji + c.SymbolEmoji + " " + c.TypeName);
                b.CallbackData = Group.Id + ",yes," + c.ColorId + "," + c.SymbolId + "," + c.TypeName;
                InlineKeyboardButton[] ba = { b };
                buttons[i] = ba;
                i++;
            }
            if (!nono)
            {
                InlineKeyboardButton b2 = new InlineKeyboardButton("Keine Not-To-Do-Liste einsetzen", Group.Id + ",no");
                InlineKeyboardButton[] bs = { b2 };
                buttons[i] = bs;
            }
            InlineKeyboardMarkup im = new InlineKeyboardMarkup(buttons);
            string txt = "Willst du eine Not-To-Do-Liste einsetzen?\nWenn ja, welche?";
            if (nono)
            {
                txt = "Welche Not-To-Do-Liste willst du einsetzen?";
            }
            Message msg = Program.sendAndReturnMessage(txt: txt, 
                chatid: Id, inlineMarkup: im);
            waitForCallback(msg, 10);
            string[] args = CalledBackData;
            CalledBackData = null;
            if (args[0] == "Timeout")
            {
                yesIWant = false;
                if (nono)
                {
                    return (Card) selection[0];
                }
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
