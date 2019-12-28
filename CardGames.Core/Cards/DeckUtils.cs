using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core.Cards
{
    public static class DeckUtils
    {
        public static char[] suitSymbols = new char[] { '\u2660', '\u2665', '\u2666', '\u2663' };

        public static List<PlayingCard> ShuffleDeck(List<PlayingCard> deck)
        {
            var randomizer = new Random();
            return deck.OrderBy(c => randomizer.Next(0, 52)).ToList();
        }

        public static PlayingCard PickFirst(this List<PlayingCard> deck)
        {
            var card = deck[0];
            deck.RemoveAt(0);
            return card;
        }

        public static void InsertAtEnd(this List<PlayingCard> deck, PlayingCard card) => deck.Add(card);

    }
}
