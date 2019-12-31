using CardGames.Core;
using CardGames.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using CardGames.Core.Cards;
using CardGames.Core.Models;

namespace CardGames.ConsoleVersion
{
    class HighOrLowConsole
    {
        HighOrLowCore core = new HighOrLowCore(new HighScoreServices(new CardGamesContext()));
        bool isRunning = true;

        public void Play()
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
            Console.WriteLine("[H]ighscore");
            Console.WriteLine("[B]ack to main menu");
            Console.WriteLine();
            Console.Write(">");
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.N:
                    Console.Clear();
                    GetPlayerName();
                    RunGame();
                    break;
                case ConsoleKey.H:
                    PrintOutHighScore();
                    break;
                case ConsoleKey.B:
                    isRunning = false;
                    break;
            }
        }

        private void GetPlayerName()
        {
            bool isRegistered;
            bool isPlayer = false;
            bool correctInput = false;

            do
            {
                Console.Write("Enter your name: ");
                string input = Console.ReadLine();
                isRegistered = core.service.CheckIfRegistered(input);
                if (isRegistered)
                {
                    Console.WriteLine("Name is taken!");
                    Console.WriteLine($"Are you {input}? y/n ");
                    do
                    {
                        var key = Console.ReadKey(true);

                        switch (key.Key)
                        {
                            case ConsoleKey.Y:
                                isPlayer = true;
                                correctInput = true;
                                break;
                            case ConsoleKey.N:
                                isPlayer = false;
                                correctInput = true;
                                break;
                            default:
                                Console.WriteLine("Please enter [Y] or [N].");
                                correctInput = false;
                                break;
                        }
                    } while (!correctInput);

                    core.GetPlayer(input);
                }
                else
                {
                    core.CreateNewPlayer(input);
                    isPlayer = true;
                }
            } while (!isPlayer);


        }

        private void RunGame()
        {
            var deck = new PlayingCardDeck();
            core.shuffledDeck = DeckUtils.ShuffleDeck(deck.Cards);
            do
            {
                Console.Clear();
                Header();
                Console.WriteLine("Guess if your card is higher or lower than the open card.");
                Console.WriteLine();
                Console.WriteLine($"{core.Player.Name}'s score: {core.Player.Score}");
                Console.WriteLine();
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
                Console.Write("[T]ry again or back to main [M]enu: ");
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.T:
                        return true;
                    case ConsoleKey.M:
                        core.service.UpdateHighscore(core.Player);
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
            Console.WriteLine($"New score: {core.Player.Score} ({(core.Result == "RIGHT" ? "+1" : "-1")})");
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
                        core.Result = core.high ? "RIGHT" : "WRONG";
                        core.Guess = "higher";
                        core.Player.Score += core.high ? 1 : -1;
                        correctInput = true;
                        break;
                    case ConsoleKey.L:
                        core.Result = core.high ? "WRONG" : "RIGHT";
                        core.Guess = "lower";
                        core.Player.Score += core.high ? -1 : 1;
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
            var highScoreList = core.service.GetHighScores();
            Console.Clear();
            Header();
            Console.WriteLine();
            Console.WriteLine("-------------------");
            Console.WriteLine("H I G H   S C O R E");
            Console.WriteLine("-------------------");

            foreach (var h in highScoreList)
            {
                Console.WriteLine($"{h.Name} - {h.Score}");
            }

            Console.Write("Press any key to go back.");
            Console.ReadKey(true);
        }


    }
}
