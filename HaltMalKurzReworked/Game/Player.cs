using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HaltMalKurzReworked.Game
{
    public class Player
    {
        public Hand Hand { get; } = new Hand();
        public User TUser { get; }
        public Player(User user)
        {
            TUser = user;
        }
    }
}
