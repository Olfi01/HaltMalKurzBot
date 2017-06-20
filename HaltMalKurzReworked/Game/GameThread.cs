using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HaltMalKurzReworked.Game
{
    public class GameThread
    {
        public HGame Game { get; }
        public TelegramBotClient Client { get; }
        public event GameEndedEvent Ended;
        public delegate void GameEndedEvent(object sender, HGame game);
        private int StartPlayersCount;
        #region Constants
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

        public GameThread(HGame game, TelegramBotClient client)
        {
            Game = game;
            Client = client;
        }

        public void Start()
        {
            StartPlayersCount = Game.Players.Count;
            foreach (Player p in Game.Players)
            {
                p.Hand.AddCard(Game.Stack.Draw(10 - Game.Players.Count));
                p.Hand.OnRazupaltuff += (sender, card) =>
                    Client.SendTextMessageAsync(Game.GroupId, $"*{p.TUser.FirstName}:* Razupaltuff!",
                        parseMode: ParseMode.Markdown);
            }
            Card c;
            do
            {
                c = Game.Stack.Draw();
            } while (c.Color != Color.Razupaltuff);
            Game.Pile.AddCard(c);
            Client.SendStickerAsync(Game.GroupId, GetStickerId(c));
            int turn = 0;
            while (Game.Players.Count == StartPlayersCount)
            {
                turn++;
                int playerturn = turn % Game.Players.Count;
                Player player = Game.Players[playerturn];
                HaveTurn(player);
            }
        }

        private void HaveTurn(Player player)
        {
            Client.SendTextMessageAsync(Game.GroupId, $"{player.TUser.FirstName} ist dran. Er hat 33 Sekunden Zeit.");
            SelectCard(player, player.Hand.Cards.FindAll(x => Game.Pile.Fits(x)), canDraw: true, seconds: 33);
        }

        private void SelectCard(Player player, List<Card> cards, bool canDraw = false, int seconds = 30)
        {
            List<InlineKeyboardButton[]> rows = new List<InlineKeyboardButton[]>();
            foreach (Card c in cards)
            {
                InlineKeyboardButton b = new InlineKeyboardButton(c.ToString(), $"card_{c.ToString()}@{Game.GroupId}");
                rows.Add(new InlineKeyboardButton[] { b });
            }
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup(rows.ToArray());
            Task<Message> task = Client.SendTextMessageAsync(player.TUser.Id, "Lege eine Karte.", replyMarkup: markup);
            task.Wait();
            Message sent = task.Result;
            ManualResetEvent e = new ManualResetEvent(false);
            bool gotResponse = false;
            string response = "";
            Timer t = new Timer(x =>
            { 
                if (!gotResponse) Client.EditMessageTextAsync(sent.Chat.Id, sent.MessageId, "Leider zu spät...");
                e.Set();
            }, null, 1000 * seconds, Timeout.Infinite);
            e.WaitOne();
            gotResponse = true;
            Client.OnCallbackQuery += (sender, ev) =>
            {
                string data = ev.CallbackQuery.Data;
                Regex regex = new Regex(@"^card_.*@\d*$");
                if (!regex.IsMatch(data)) return;
                int underscoreIndex = data.IndexOf('_');
                int atIndex = data.IndexOf('@');
                long id = Convert.ToInt64(data.Substring(atIndex + 1));
                if (id == Game.GroupId)
                {
                    response = data.Substring(underscoreIndex + 1).Remove(atIndex);
                    e.Set();
                }
            };
        }

        #region Get Sticker Id
        private string GetStickerId(Card c)
        {
            switch (c.Symbol)
            {
                case Symbol.All:
                    switch (c.Type)
                    {
                        case Type.Kommunismus:
                            return kommunismusFileId;
                    }
                    return "fail";
                case Symbol.Kleinkünstler:
                    switch (c.Type)
                    {
                        case Type.AchMeinDein:
                            return achMeinDeinKleinkünstlerFileId;
                        case Type.GruppenSchnickSchnackSchnuck:
                            return gruppenSchnickSchnackSchnuckKleinkünstlerFileId;
                        case Type.HaltMalKurz:
                            return haltMalKurzKleinkünstlerFileId;
                        case Type.Kapitalismus:
                            return kapitalismusKleinkünstlerFileId;
                        case Type.Nazi:
                            return naziKleinkünstlerFileId;
                        case Type.NotToDoListe:
                            return notToDoListeKleinkünstlerFileId;
                        case Type.Polizei:
                            return polizeiKleinkünstlerFileId;
                        case Type.SchnickSchnackSchnuck:
                            return schnickSchnackSchnuckKleinkünstlerFileId;
                        case Type.Vollversammlung:
                            return vollversammlungKleinkünstlerFileId;
                    }
                    return "fail";
                case Symbol.Känguru:
                    switch (c.Type)
                    {
                        case Type.AchMeinDein:
                            return achMeinDeinKänguruFileId;
                        case Type.GruppenSchnickSchnackSchnuck:
                            return gruppenSchnickSchnackSchnuckKänguruFileId;
                        case Type.HaltMalKurz:
                            return haltMalKurzKänguruFileId;
                        case Type.Kapitalismus:
                            return kapitalismusKänguruFileId;
                        case Type.Nazi:
                            return naziKänguruFileId;
                        case Type.NotToDoListe:
                            return notToDoListeKänguruFileId;
                        case Type.Polizei:
                            return polizeiKänguruFileId;
                        case Type.SchnickSchnackSchnuck:
                            return schnickSchnackSchnuckKänguruFileId;
                        case Type.Vollversammlung:
                            return vollversammlungKänguruFileId;
                    }
                    return "fail";
                case Symbol.Pinguin:
                    switch (c.Type)
                    {
                        case Type.AchMeinDein:
                            return achMeinDeinPinguinFileId;
                        case Type.GruppenSchnickSchnackSchnuck:
                            return gruppenSchnickSchnackSchnuckPinguinFileId;
                        case Type.HaltMalKurz:
                            return haltMalKurzPinguinFileId;
                        case Type.Kapitalismus:
                            return kapitalismusPinguinFileId;
                        case Type.Nazi:
                            return naziPinguinFileId;
                        case Type.NotToDoListe:
                            return notToDoListePinguinFileId;
                        case Type.Polizei:
                            return polizeiPinguinFileId;
                        case Type.SchnickSchnackSchnuck:
                            return schnickSchnackSchnuckPinguinFileId;
                        case Type.Vollversammlung:
                            return vollversammlungPinguinFileId;
                    }
                    return "fail";
                case Symbol.None:
                    switch (c.Type)
                    {
                        case Type.Razupaltuff:
                            return razupaltuffFileId;
                    }
                    return "fail";
            }
            return "fail";
        }
        #endregion
    }
}
