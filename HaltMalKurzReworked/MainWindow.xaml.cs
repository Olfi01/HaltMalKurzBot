using Project_Daidalos;
using Project_Daidalos.Types.Actions;
using Project_Daidalos.Types.Enums;
using Project_Daidalos.Types.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using HaltMalKurzReworked.Game;
using System.Threading;

namespace HaltMalKurzReworked
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants
        private const string Token = "278139117:AAFl13vIj7dVwbep1wQs1_Mw4mQZDa3yR3Y";
        #endregion
        #region Variables
        private DBot Bot;
        private List<HGame> games = new List<HGame>();
        #endregion
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            InitializeBot();
        }
        #endregion

        private void InitializeBot()
        {
            Bot = new DBot(Token);
            Bot.OnUserStarted += (x, y) => {
                Bot.Client.SendTextMessageAsync(y.Message.Chat.Id, "Hallo! Dies ist ein kleines Hobbyprogrammierprojekt "
                    + "eines Elftklässlers (@Olfi01), also erwarte nicht zu viel!"); };
            Bot.AddCommand(new CommandAction.RunMethod(StartGame_Command), "/startgame", CommandType.CommandOnly);
            Bot.AddCommand(new CommandAction.RunMethod(Anleitung_Command), "/anleitung", CommandType.CommandOnly);
            Bot.AddCallback(Join_Callback, "join");
            Bot.AddCallback(Start_Callback, "start");
        }

        #region Commands
        #region /startgame
        private void StartGame_Command(Message message, CommandType type, List<string> args)
        {
            if (message.Chat.Type != ChatType.Group && message.Chat.Type != ChatType.Supergroup)
            {
                Bot.SendTextMessage(message.Chat.Id, "Du musst diesen Befehl in einer Gruppe verwenden, in der ich vorhanden bin!");
                return;
            }
            if (games.Exists(x => x.GroupId == message.Chat.Id))
            {
                Bot.SendTextMessage(message.Chat.Id, "In dieser Gruppe läuft bereits ein Spiel.");
                return;
            }
            InlineKeyboardButton buttonJoin = new InlineKeyboardButton()
            {
                Text = "Join",
                CallbackData = $"join_{message.Chat.Id}"
            };
            InlineKeyboardButton buttonStart = new InlineKeyboardButton()
            {
                Text = "Start",
                CallbackData = $"start_{message.Chat.Id}"
            };
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup(new InlineKeyboardButton[] { buttonJoin, buttonStart });
            Bot.Client.SendTextMessageAsync(message.Chat.Id, "Das Spiel ist eröffnet! Benutzt die Knöpfe, um den Spaß zu beginnen!",
                replyMarkup: markup);
            HGame game = new HGame(message.Chat.Id, Bot.Client);
            games.Add(game);
            Bot.Client.SendTextMessageAsync(message.Chat.Id, game.AddPlayer(new Player(message.From)));
        }
        #endregion
        #region /anleitung
        private void Anleitung_Command(Message message, CommandType type, List<string> args)
        {
            InlineKeyboardButton b = new InlineKeyboardButton("Anleitung")
            {
                Url = "www.halt-mal-kurz.de"
            };
            InlineKeyboardButton[] bs = { b };
            InlineKeyboardMarkup im = new InlineKeyboardMarkup(bs);
            Bot.Client.SendTextMessageAsync(message.Chat.Id, "Hier ist der Link:", replyMarkup: im);
        }
        #endregion
        #endregion

        #region Callbacks
        private void Join_Callback(CallbackQuery query)
        {
            long groupid = Convert.ToInt64(query.Data.Substring(query.Data.IndexOf('_') + 1));
            if (games.Exists(x => x.GroupId == groupid))
            {
                HGame game = games.Find(x => x.GroupId == groupid);
                Bot.SendTextMessage(groupid, game.AddPlayer(new Player(query.From)));
            }
            else
            {
                Bot.SendTextMessage(groupid, "Scheinbar läuft in dieser Gruppe gerade kein Spiel. Starte eines mit /startgame!");
            }
            Bot.Client.AnswerCallbackQueryAsync(query.Id);
        }

        private void Start_Callback(CallbackQuery query)
        {
            long groupid = Convert.ToInt64(query.Data.Substring(query.Data.IndexOf('_') + 1));
            if (games.Exists(x => x.GroupId == groupid))
            {
                HGame game = games.Find(x => x.GroupId == groupid);
                string res = game.Start();
                if (res == "Das Spiel beginnt!")
                {
                    Bot.Client.EditMessageTextAsync(groupid, query.Message.MessageId,
                        "Dieses Spiel hat begonnen!", replyMarkup: null);
                    Bot.SendTextMessage(groupid, res);
                    GameThread gameThread = new GameThread(game, Bot.Client);
                    Thread t = new Thread(gameThread.Start);
                    t.Start();
                    gameThread.Ended += (sender, g) => games.Remove(g);
                }
                else
                {
                    Bot.SendTextMessage(groupid, res);
                }
            }
            else
            {
                Bot.SendTextMessage(groupid, "Scheinbar läuft in dieser Gruppe gerade kein Spiel. Starte eines mit /startgame!");
            }
            Bot.Client.AnswerCallbackQueryAsync(query.Id);
        }
        #endregion

        #region XAML Stuff
        #region Start Button
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Bot.State == State.Active)
            {
                Bot.Stop();
                startButton.Content = "Starten";
                startButton.Background = Brushes.Red;
            }
            else
            {
                Bot.Start();
                startButton.Content = "Stoppen";
                startButton.Background = Brushes.Green;
            }
        }
        #endregion
        #endregion
    }
}
