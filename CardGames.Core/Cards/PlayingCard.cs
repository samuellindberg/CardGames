using CardGames.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core.Cards
{
    public class PlayingCard
    {
        public PlayingCard(CardRank rank, CardSuit suit, char suitSymbol)
        {
            Rank = rank;
            Suit = suit;
            SuitSymbol = suitSymbol;
        }

        public CardRank Rank { get; }
        public CardSuit Suit { get; }
        public char SuitSymbol { get; set; }

        public override string ToString()
        {
            return $"{SuitSymbol}{Rank}";
        }
    }
}
