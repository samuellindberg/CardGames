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
    }
}
