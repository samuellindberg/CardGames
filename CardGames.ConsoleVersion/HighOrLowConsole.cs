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

        public void Play()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            Header();
            Menu();
        }

        private void Menu()
        {
            Console.WriteLine("MENU");
            Console.WriteLine("[N]ew game");
            Console.WriteLine("[B]ack to main");
            Console.Write(">");
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.N:
                    Console.Clear();
                    GetPlayerName();
                    RunGame();
                    break;
                case ConsoleKey.B:
                    Console.Clear();
                    Program.MainMenu();
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

        private static void Header()
        {
            Console.WriteLine($"---------------------");
            Console.WriteLine("H I G H   O R   L O W");
            Console.WriteLine($"{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}");
            Console.WriteLine();
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
                Console.WriteLine($"{core.player.Name}'s score: {core.player.Score}");
                Console.WriteLine();
                GetHiddenCardAndPrintOut();
                GetPlayersCardAndPrintOut();
                core.CheckHighOrLow();
                GetPlayerGuess();
                core.PutBackCards();
                PrintResult();
                core.tryAgain = TryAgainOrMenu();

            } while (core.tryAgain);
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
                    case ConsoleKey.L:
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
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(core.result);
            Console.Write("Hidden card: ");
            Console.WriteLine(core.hiddenCard);
            Console.Write("Your card: ");
            Console.WriteLine(core.playersCard);
        }

        private void GetPlayerGuess()
        {
            Console.Write("Is your card [H]igher or [L]ower? >");
            var key = Console.ReadKey(true);

            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.H:
                    core.result = core.high ? "RIGHT" : "WRONG";
                    break;
                case ConsoleKey.L:
                    core.result = core.high ? "WRONG" : "RIGHT";
                    break;
            }
        }



        private void GetPlayersCardAndPrintOut()
        {
            core.GetPlayersCard();
            Console.Write("Your card: ");
            Console.WriteLine(core.playersCard);
        }

        private void GetHiddenCardAndPrintOut()
        {
            core.GetHiddenCard();
            Console.Write("Hidden card: ");
            Console.WriteLine("*****");
        }

        public void PrintOutHighScore()
        {
            var highScoreList = core.service.GetHighScores();

            Console.WriteLine("-------------------");
            Console.WriteLine("H I G H   S C O R E");
            Console.WriteLine("-------------------");

            foreach (var h in highScoreList)
            {
                Console.WriteLine($"{h.Name} - {h.Score}");
            }
        }


    }
}
