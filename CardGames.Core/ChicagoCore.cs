﻿using CardGames.Core.Cards;
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
        public Player RoundWinner { get; set; }
        public Player GameWinner { get; set; }

        public void GetPlayer(string name)
        {
            //Player = Service.GetPlayerByName(name);
        }

        public void InitChicago()
        {
            var deck = new PlayingCardDeck();
            ShuffledDeck = DeckUtils.ShuffleDeck(deck.Cards);
            DeckUtils.DealCards(Players, ShuffledDeck, 5);
            Table = new List<PlayingCard>();
            TurnCounter = 0;

        }

        public void PlayCard(Player player, int cardIndex)
        {
            if (Table.Count > 0
                && CheckIfHavingSuit(player, Table)
                && player.CardsOnHand[cardIndex].Suit != Table[0].Suit)
            {
                throw new ArgumentException("Unallowed card.");
            }
            else
            {
                player.CardsOnHand[cardIndex].PlayedBy = player;
                Table.Add(player.CardsOnHand[cardIndex]);
                player.CardsOnHand.RemoveAt(cardIndex);
            }

            CheckIfEveryonePlayedAndTrickWinner();


        }

        private void CheckIfEveryonePlayedAndTrickWinner()
        {
            if (Table.Count == Players.Length)
            {
                RoundWinner = Table[0].PlayedBy;
                var highest = (int)Table[0].Rank;

                for (int i = 1; i < Table.Count; i++)
                {
                    if (Table[0].Suit == Table[i].Suit && highest != 1)
                    {
                        if (highest < (int)Table[i].Rank || (int)Table[i].Rank == 1)
                        {
                            highest = (int)Table[i].Rank;
                            RoundWinner = Table[i].PlayedBy;
                        }

                    }
                }

                RoundWinner.Tricks.Add(Table);
                Table.Clear();
                TurnCounter = Array.IndexOf(Players, RoundWinner);
            }
            else
                TurnCounter++;
        }

        public bool CheckIfGameIsOverAndAddScore()
        {
            bool gameOver = false;

            if (Players.Where(p => p.CardsOnHand.Count > 0).Count() == 0)
            {
                GameWinner = RoundWinner;
                if (GameWinner.ChicagoScore == null)
                    GameWinner.ChicagoScore = 2;
                else
                    GameWinner.ChicagoScore += 2;
                gameOver = true;
            }

            return gameOver;
        }

        private bool CheckIfHavingSuit(Player player, List<PlayingCard> table)
        {
            return player.CardsOnHand.Exists(c => c.Suit == table[0].Suit);
        }
    }
}
