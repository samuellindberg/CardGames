using CardGames.Core;
using CardGames.Core.Cards;
using CardGames.Core.Enums;
using CardGames.Core.Models;
using CardGames.Core.Models.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardGames.Test
{
    [TestClass]
    public class HighOrLowTests
    {
        HighOrLowCore c = new HighOrLowCore(new HighScoreServices(new CardGamesContext()));

        [TestMethod]
        public void HeartsFourHigherThanDiamondsFour()
        {
            c.OpenCard = new PlayingCard(CardRank.Four, CardSuit.Diamonds, 'x', "x");
            c.PlayersCard = new PlayingCard(CardRank.Four, CardSuit.Hearts, 'x', "x");
            c.CheckHighOrLow();

            Assert.IsTrue(c.high);
        }

        [TestMethod]
        public void ClubsTenLowerThanSpadesTen()
        {
            //Testing suit ranking
            c.OpenCard = new PlayingCard(CardRank.Ten, CardSuit.Spades, 'x', "x");
            c.PlayersCard = new PlayingCard(CardRank.Ten, CardSuit.Clubs, 'x', "x");
            c.CheckHighOrLow();

            Assert.IsFalse(c.high);
        }

        [TestMethod]
        public void ClubsQueenLowerThanHeartsAce()
        {
            c.OpenCard = new PlayingCard(CardRank.Ace, CardSuit.Hearts, 'x', "x");
            c.PlayersCard = new PlayingCard(CardRank.Queen, CardSuit.Clubs, 'x', "x");
            c.CheckHighOrLow();

            Assert.IsFalse(c.high);
        }

        [TestMethod]
        public void ClubsAceHigherThanSpadesKing()
        {
            c.OpenCard = new PlayingCard(CardRank.King, CardSuit.Spades, 'x', "x");
            c.PlayersCard = new PlayingCard(CardRank.Ace, CardSuit.Clubs, 'x', "x");
            c.CheckHighOrLow();

            Assert.IsTrue(c.high);
        }

        [TestMethod]
        public void ClubsQueenHigherThanSpadesThree()
        {
            c.OpenCard = new PlayingCard(CardRank.Three, CardSuit.Spades, 'x', "x");
            c.PlayersCard = new PlayingCard(CardRank.Queen, CardSuit.Clubs, 'x', "x");
            c.CheckHighOrLow();

            Assert.IsTrue(c.high);
        }

        [TestMethod]
        public void Diamond2LowerThanHearts8()
        {
            c.OpenCard = new PlayingCard(CardRank.Eight, CardSuit.Hearts, 'x', "x");
            c.PlayersCard = new PlayingCard(CardRank.Two, CardSuit.Diamonds, 'x', "x");
            c.CheckHighOrLow();

            Assert.IsFalse(c.high);
        }
    }
}
