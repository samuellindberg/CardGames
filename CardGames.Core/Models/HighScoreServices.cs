﻿using CardGames.Core.Cards;
using CardGames.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Core.Models
{
    public class HighScoreServices
    {
        private readonly CardGamesContext ctx;

        public HighScoreServices(CardGamesContext ctx)
        {
            this.ctx = ctx;
        }
        public bool CheckIfRegistered(string input)
        {
            return ctx.HighScore
                .Any(h => h.Name == input);
        }

        public List<HighScore> GetHighScores()
        {
            return ctx.HighScore.Select(h => h).OrderByDescending(h => h.HighOrLowScore).ToList();
        }

        public Player GetPlayerByName(string name)
        {
            var hs = ctx.HighScore.Single(h => h.Name == name);
            return new Player
            {
                Id = hs.Id,
                Name = hs.Name,
                HighOrLowScore = hs.HighOrLowScore,
                CardsOnHand = new List<PlayingCard>(),
            };
        }

        public void UpdateHighscore(Player p)
        {
            if (p.Id == null)
                ctx.HighScore.Add(new HighScore
                {
                    Name = p.Name,
                    HighOrLowScore = p.HighOrLowScore,
                });
            else
            {
                var hs = ctx.HighScore.Single(h => h.Id == p.Id);
                hs.HighOrLowScore = p.HighOrLowScore;
            }

            ctx.SaveChanges();
        }

        public Player CreateNewPlayer(string name)
        {
            return new Player
            {
                Id = null,
                Name = name,
                ChicagoScore = 0,
                CardsOnHand = new List<PlayingCard>(),
            };
        }
    }
}
