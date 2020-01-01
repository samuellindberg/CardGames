using CardGames.Core.Cards;
using CardGames.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core
{
    public class ChicagoCore : ICardGameCore
    {
        bool cardOnTable = false;

        public ChicagoCore(HighScoreServices service)
        {
            Service = service;
            Deck = new PlayingCardDeck();
            Deck.Cards = DeckUtils.ShuffleDeck(Deck.Cards);
        }

        public HighScoreServices Service { get; }
        public PlayingCardDeck Deck { get; set; }
        public List<PlayingCard> ShuffledDeck { get; set; }
        public Player[] Players { get; set; }
        public int TurnCounter { get; set; }
        public List<PlayingCard> Table { get; set; }

        public void GetPlayer(string name)
        {
            //Player = Service.GetPlayerByName(name);
        }

        public void InitChicago(int numOfPlayers)
        {
            var deck = new PlayingCardDeck();
            ShuffledDeck = DeckUtils.ShuffleDeck(deck.Cards);
            DeckUtils.DealCards(Players, ShuffledDeck, 5);
            Table = new List<PlayingCard>();
        }

        public void PlayCard(Player player, int cardIndex)
        {
            bool havingSuit = CheckIfHavingSuit(player, Table);

            player.CardsOnHand[cardIndex].PlayedBy = player;
            Table.Add(player.CardsOnHand[cardIndex]);
            player.CardsOnHand.RemoveAt(cardIndex);
        }

        private bool CheckIfHavingSuit(Player player, List<PlayingCard> table)
        {
            return player.CardsOnHand.Exists(c => c.Suit == table[0].Suit);
        }
    }
}
