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
    public class HighOrLowCore : ICardGameCore
    {
        public HighScoreServices Service { get; }

        public PlayingCardDeck Deck { get; set; }
        public List<PlayingCard> ShuffledDeck { get; set; }
        public PlayingCard PlayersCard { get; set; }
        public PlayingCard OpenCard { get; set; }
        public bool high = false;
        public string Result { get; set; }
        public string Guess { get; set; }
        public bool tryAgain;
        public Player Player { get; set; }


        public HighOrLowCore(HighScoreServices service)
        {
            Service = service;
            Deck = new PlayingCardDeck();
            Deck.Cards = DeckUtils.ShuffleDeck(Deck.Cards);
        }

        public void CheckHighOrLow()
        {
            high = false;

            if (((int)PlayersCard.Rank == 1 && (int)OpenCard.Rank != 1) 
                || 
                ((int)PlayersCard.Rank > (int)OpenCard.Rank) && (int)OpenCard.Rank != 1)
                high = true;
            else if ((int)PlayersCard.Rank == (int)OpenCard.Rank)
            {
                if ((int)PlayersCard.Suit < (int)OpenCard.Suit)
                    high = true;
            }
        }

        public void GuessedHigh()
        {
            Result = high ? "RIGHT" : "WRONG";
            Guess = "higher";
            if (Player.HighOrLowScore == null)
                Player.HighOrLowScore = high ? 1 : -1;
            else
                Player.HighOrLowScore += high ? 1 : -1;
        }

        public void GuessedLow()
        {
            Result = high ? "WRONG" : "RIGHT";
            Guess = "lower";
            if (Player.HighOrLowScore == null)
                Player.HighOrLowScore = high ? -1 : 1;
            else
                Player.HighOrLowScore += high ? -1 : 1;
        }

        public void GetOpenCard()
        {
            OpenCard = ShuffledDeck.PickFirst();
        }

        public void GetPlayersCard()
        {
            PlayersCard = ShuffledDeck.PickFirst();
        }

        public void CreateNewPlayer(string name)
        {
            Player = new Player
            {
                Id = null,
                Name = name,
                HighOrLowScore = 0,
            };
        }

        public void GetPlayer(string name)
        {
            Player = Service.GetPlayerByName(name);
        }

        public void PutBackCards()
        {
            ShuffledDeck.InsertAtEnd(PlayersCard);
            ShuffledDeck.InsertAtEnd(OpenCard);
        }

    }
}
