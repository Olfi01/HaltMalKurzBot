using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;

namespace HaltMalKurzReworked.Game
{
    public class HGame
    {
        public List<Player> Players = new List<Player>();
        private TelegramBotClient Client { get; }
        public long GroupId { get; }
        public GameState State { get; set; }
        public Pile Pile { get; }
        public Stack Stack { get; }
        public HGame(long groupId, TelegramBotClient client)
        {
            GroupId = groupId;
            Client = client;
            State = GameState.JoinPhase;
            Pile = new Pile();
            Stack = new Stack(Pile);
        }

        public string Start()
        {
            if (Players.Count > 2 && Players.Count < 6)
            {
                State = GameState.Running;
                return "Das Spiel beginnt!";
            }
            else
            {
                return Players.Count < 3 ? "Nicht genügend Spieler." : "Zu viele Spieler. Wie habt ihr das geschafft?";
            }
        }

        public string AddPlayer(Player p)
        {
            if (State != GameState.JoinPhase)
            {
                return "Dem Spiel kann momentan leider nicht beigetreten werden!";
            }
            if (Players.Count > 4)
            {
                return "Leider ist kein Platz mehr für weitere Spieler.";
            }
            if (Players.Exists(x => x.TUser.Id == p.TUser.Id))
            {
                return $"Der Spieler {p.TUser.FirstName} spielt bereits mit.";
            }
            var t = Client.SendTextMessageAsync(p.TUser.Id, "Du bist einem Spiel beigetreten!");
            try
            {
                t.Wait();
            }
            catch (AggregateException)
            {
                string pname = p.TUser.Username != null
                    ? "@" + p.TUser.Username
                    : p.TUser.FirstName;
                return $"Ich konnte {pname} nicht zum Spiel hinzufügen, "
                    + "da er/sie mich noch im Privatchat starten muss.";
            }
            Players.Add(p);
            return $"Der Spieler {p.TUser.FirstName} ist dem Spiel beigetreten.\nIm Spiel: {Players.Count} Spieler\n"
                + $"Erlaubt: 3 - 5 Spieler.";
        }
    }

    public enum GameState
    {
        JoinPhase,
        Running
    }
}
