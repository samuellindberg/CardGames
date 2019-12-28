using CardGames.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core.Cards
{
    public class PlayingCardDeck
    {
        public List<PlayingCard> Cards;

        Random randomizer = new Random(1);


        public PlayingCardDeck()
        {
            Cards = new List<PlayingCard>();
            PopulateCardDeck();
        }

        private void PopulateCardDeck()
        {
            for (int s = 0; s < 4; s++)
            {
                for (int r = 1; r <= 13; r++)
                {
                    Cards.Add(new PlayingCard((CardRank)r , (CardSuit)s, DeckUtils.suitSymbols[s]));
                }
            }
        }


    }
}
