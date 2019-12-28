using CardGames.Core.Cards;
using CardGames.Core.Models;
using CardGames.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core
{
    public class HighOrLowCore
    {
        public readonly HighScoreServices service;

        public PlayingCardDeck Deck { get; set; }
        public List<PlayingCard> shuffledDeck;
        public PlayingCard hiddenCard;
        public PlayingCard playersCard;
        public bool high = false;
        public string result;
        public bool tryAgain;
        public Player player;

        public HighOrLowCore(HighScoreServices service)
        {
            this.service = service;
            Deck = new PlayingCardDeck();
            Deck.Cards = DeckUtils.ShuffleDeck(Deck.Cards);
        }

        public void CheckHighOrLow()
        {
            if ((int)hiddenCard.Rank < (int)playersCard.Rank)
                high = true;
            else if ((int)hiddenCard.Rank == (int)playersCard.Rank)
            {
                if ((int)hiddenCard.Suit > (int)playersCard.Suit)
                    high = true;
            }
        }

        public void GetPlayersCard()
        {
            playersCard = shuffledDeck.PickFirst();
        }

        public void GetHiddenCard()
        {
            hiddenCard = shuffledDeck.PickFirst();
        }

        public void CreateNewPlayer(string name)
        {
            player = new Player
            {
                Name = name,
            };
        }

        public void GetPlayer(string name)
        {
            player = service.GetPlayerByName(name);
        }

        public void PutBackCards()
        {
            shuffledDeck.InsertAtEnd(hiddenCard);
            shuffledDeck.InsertAtEnd(playersCard);
        }

    }
}
