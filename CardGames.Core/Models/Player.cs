using CardGames.Core.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core.Models
{
    public class Player
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? HighOrLowScore { get; set; }
        public int? ChicagoScore { get; set; }
        public List<PlayingCard> CardsOnHand { get; set; }
        public List<List<PlayingCard>> Tricks { get; set; }
    }
}
