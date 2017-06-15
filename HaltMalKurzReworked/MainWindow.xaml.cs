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
using Telegram.Bot.Types.ReplyMarkups;

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
        }

        #region Commands
        #region /startgame
        private void StartGame_Command(Message message, CommandType type, List<string> args)
        {
            //throw new NotImplementedException();
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
