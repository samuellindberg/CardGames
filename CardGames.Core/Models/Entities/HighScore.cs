﻿using System;
using System.Collections.Generic;

namespace CardGames.Core.Models.Entities
{
    public partial class HighScore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HighOrLowScore { get; set; }
        public int? ChicagoScore { get; set; }
    }
}
