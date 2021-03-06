﻿using CardGames.Core;
using CardGames.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using CardGames.Core.Cards;
using CardGames.Core.Models;
using CardGames.Core.Enums;

namespace CardGames.ConsoleVersion
{
    class HighOrLowConsole
    {
        HighOrLowCore core;
        CardGamesUtils utils;

        public HighOrLowConsole(HighOrLowCore core)
        {
            this.core = core;
            utils = new CardGamesUtils(core);
        }

        bool isRunning = true;

        public void Run()
        {
            do
            {
                Console.Clear();
                Header();
                Menu();

            } while (isRunning);
        }

        private void Menu()
        {
            Console.WriteLine("[N]ew game");
            Console.WriteLine("[R]ules");
            Console.WriteLine("[H]ighscore");
            Console.WriteLine("[B]ack to main menu");
            Console.WriteLine();
            Console.Write(">");
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.N:
                    Console.Clear();
                    Header();
                    Console.Write("Enter your name: ");
                    core.Player = utils.GetPlayerName();
                    RunGame();
                    break;
                case ConsoleKey.R:
                    PrintOutRules();
                    break;
                case ConsoleKey.H:
                    PrintOutHighScore();
                    break;
                case ConsoleKey.B:
                    isRunning = false;
                    break;
            }
        }
        private void PrintOutRules()
        {
            Console.Clear();
            Header();
            Console.WriteLine("Guess if hidden card is higher or lower than the open card. Right answer gives +1 point, wrong answer gives -1 point.");
            Console.WriteLine();
            Console.Write("Press any key to go back.");
            Console.ReadKey(true);
        }
        private void RunGame()
        {
            var deck = new PlayingCardDeck();
            core.ShuffledDeck = DeckUtils.ShuffleDeck(deck.Cards);
            do
            {
                Console.Clear();
                Header();
                PrintNameAndScore();
                core.GetOpenCard();
                core.GetPlayersCard();
                PrintCards(core.OpenCard, core.PlayersCard, true);
                core.CheckHighOrLow();
                GetPlayerGuess();
                core.PutBackCards();
                PrintResult();
                core.tryAgain = TryAgainOrMenu();

            } while (core.tryAgain);
        }

        private void PrintNameAndScore()
        {
            Console.WriteLine($"{core.Player.Name}'s score: {(core.Player.HighOrLowScore == null ? 0 : core.Player.HighOrLowScore)}");
            Console.WriteLine();
        }
        private static void Header()
        {
            Console.WriteLine($"{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}");
            Console.WriteLine("H I G H   O R   L O W");
            Console.WriteLine($"{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}");
            Console.WriteLine();
        }
        private bool TryAgainOrMenu()
        {
            bool correctInput = false;
            do
            {
                Console.WriteLine();
                Console.Write("[T]ry again or back to [M]enu: ");
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.T:
                        return true;
                    case ConsoleKey.M:
                        core.Service.UpdateHighOrLowHighscore(core.Player);
                        return false;
                    default:
                        Console.WriteLine("Please enter [T] or [M].");
                        break;
                }

            } while (!correctInput);

            return false;
        }
        private void PrintResult()
        {
            Console.Clear();
            Header();
            Console.WriteLine("*********");
            Console.WriteLine(core.Result);
            Console.WriteLine("*********");
            Console.WriteLine();
            Console.WriteLine($"You guessed {core.Guess}");
            Console.WriteLine($"New score: {core.Player.HighOrLowScore} ({(core.Result == "RIGHT" ? "+1" : "-1")})");
            Console.WriteLine();
            PrintCards(core.OpenCard, core.PlayersCard, false);

        }
        private void GetPlayerGuess()
        {
            bool correctInput = false;

            Console.WriteLine();
            Console.Write("Is your card [H]igher or [L]ower? >");
            do
            {
                var key = Console.ReadKey(true);

                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.H:
                        core.GuessedHigh();
                        correctInput = true;
                        break;
                    case ConsoleKey.L:
                        core.GuessedLow();
                        correctInput = true;
                        break;
                    default:
                        Console.WriteLine("Press [H] or [L].");
                        break;
                }
            } while (!correctInput);
        }

        private void PrintCards(PlayingCard open, PlayingCard players, bool hidden)
        {
            var u = '\u203E';

            if (hidden)
            {
                Console.WriteLine($"___________    ___________\n" +
                    $"|    _    |    |{open.ToString().PadRight(9)}|\n" +
                    $"|   /  \\  |    |         |\n" +
                    $"|      /  |    |         |\n" +
                    $"|     |   |    |         |\n" +
                    $"|     *   |    |{open.ToString().PadLeft(9)}|\n" +
                    $"{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}    {u}{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}");
            }
            else
            {
                Console.WriteLine($"___________    ___________\n" +
                    $"|{players.ToString().PadRight(9)}|    |{open.ToString().PadRight(9)}|\n" +
                    $"|         |    |         |\n" +
                    $"|         |    |         |\n" +
                    $"|         |    |         |\n" +
                    $"|{players.ToString().PadLeft(9)}|    |{open.ToString().PadLeft(9)}|\n" +
                    $"{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}    {u}{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}");
            }
        }

        public void PrintOutHighScore()
        {
            var highScoreList = core.Service.GetHighScores(CardGamesEnum.HighOrLow);
            Console.Clear();
            Header();
            Console.WriteLine("-------------------");
            Console.WriteLine("H I G H   S C O R E");
            Console.WriteLine("-------------------");

            foreach (var h in highScoreList)
            {
                if (h.HighOrLowScore != null)
                    Console.WriteLine($"{h.Name} - {h.HighOrLowScore}");
            }

            Console.WriteLine();
            Console.Write("Press any key to go back.");
            Console.ReadKey(true);
        }


    }
}
