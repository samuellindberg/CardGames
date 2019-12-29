﻿using CardGames.Core.Cards;
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
        public PlayingCard PlayersCard { get; set; }
        public PlayingCard OpenCard { get; set; }
        public bool high = false;
        public string Result { get; set; }
        public string Guess { get; set; }
        public bool tryAgain;
        public Player Player { get; set; }
        

        public HighOrLowCore(HighScoreServices service)
        {
            this.service = service;
            Deck = new PlayingCardDeck();
            Deck.Cards = DeckUtils.ShuffleDeck(Deck.Cards);
        }

        public void CheckHighOrLow()
        {
            high = false;
            if ((int)PlayersCard.Rank > (int)OpenCard.Rank)
                high = true;
            else if ((int)PlayersCard.Rank == (int)OpenCard.Rank)
            {
                if ((int)PlayersCard.Suit < (int)OpenCard.Suit)
                    high = true;
            }
        }

        public void GetOpenCard()
        {
            OpenCard = shuffledDeck.PickFirst();
        }

        public void GetPlayersCard()
        {
            PlayersCard = shuffledDeck.PickFirst();
        }

        public void CreateNewPlayer(string name)
        {
            Player = new Player
            {
                Id = null,
                Name = name,
                Score = 0,
            };
        }

        public void GetPlayer(string name)
        {
            Player = service.GetPlayerByName(name);
        }

        public void PutBackCards()
        {
            shuffledDeck.InsertAtEnd(PlayersCard);
            shuffledDeck.InsertAtEnd(OpenCard);
        }

    }
}
