using CardGames.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core
{
    public interface ICardGameCore
    {
        HighScoreServices Service { get; }

        void GetPlayer(string name);
    }
}
