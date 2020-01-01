using CardGames.Core.Enums;
using CardGames.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core.Cards
{
    public class PlayingCard
    {
        public PlayingCard(CardRank rank, CardSuit suit, char suitSymbol, string rankChar)
        {
            Rank = rank;
            Suit = suit;
            SuitSymbol = suitSymbol;
            RankChar = rankChar;
        }

        public CardRank Rank { get; }
        public CardSuit Suit { get; }
        public char SuitSymbol { get; set; }
        public string RankChar { get; set; }
        public Player PlayedBy { get; set; }

        public override string ToString()
        {
            return $"{SuitSymbol}{RankChar}";
        }
    }
}
