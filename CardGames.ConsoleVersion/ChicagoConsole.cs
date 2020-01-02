﻿using CardGames.Core.Cards;
using CardGames.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamUtils;
using CardGames.Core.Models;

namespace CardGames.ConsoleVersion
{
    public class ChicagoConsole
    {
        bool isRunning = true;

        private readonly ChicagoCore core;
        CardGamesUtils utils;

        public ChicagoConsole(ChicagoCore core)
        {
            this.core = core;
            utils = new CardGamesUtils(core);

        }

        public int NumOfPlayers { get; set; }

        public void Run()
        {
            do
            {
                Console.Clear();
                Header();
                Menu();

            } while (isRunning);
        }

        private static void Header()
        {
            Console.WriteLine($"{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[2]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}{DeckUtils.suitSymbols[3]}");
            Console.WriteLine("C H I C A G O");
            Console.WriteLine($"{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[0]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}{DeckUtils.suitSymbols[1]}");
            Console.WriteLine();
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

        private void GetPlayersNames(int numOfPlayers)
        {
            core.Players = new Player[numOfPlayers];

            for (int i = 0; i < core.Players.Length; i++)
            {
                Console.Write($"Enter name for player {i + 1}: ");
                core.Players[i] = utils.GetPlayerName();
            }
        }

        private void RunGame()
        {
            NumOfPlayers = GetNumberOfPlayers();
            GetPlayersNames(NumOfPlayers);
            core.InitChicago(NumOfPlayers);
            core.TurnCounter = 0;

            do
            {
                var currentPlayer = core.Players[core.TurnCounter % core.Players.Length];
                Console.Clear();
                Header();
                Console.WriteLine();
                PrintTable(core.Table);
                Console.Write($"{currentPlayer.Name}'s turn. ");
                if (core.Table.Count == 0)
                    Console.Write("Plays first card.");
                Console.WriteLine();
                Console.WriteLine();
                PrintCards(currentPlayer.CardsOnHand);
                GetPlayerAction(currentPlayer);

            } while (isRunning);
        }



        private void GetPlayerAction(Player player)
        {
            int cardIndex;
            bool correctCard = false;

            do
            {

                Console.Write("Choose card to play. Enter number and press enter: ");
                try
                {
                    cardIndex = Console.ReadLine().IsInt(1, player.CardsOnHand.Count) - 1;
                    core.PlayCard(player, cardIndex);
                    correctCard = true;

                }
                catch (FormatException)
                {
                    Console.WriteLine("Input is not a number. Try again.");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Card doesn't exist. Try again.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Card must be {core.Table[0].Suit.ToString()}");
                }
            } while (!correctCard);

        }

        private int GetNumberOfPlayers()
        {
            bool correctInput = false;
            int input = 0;

            do
            {
                Console.WriteLine();
                Console.Write("Enter hor many players (2-6): ");
                try
                {
                    input = Console.ReadLine().IsInt(2, 6);
                    correctInput = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    correctInput = false;
                }

            } while (!correctInput);

            return input;

        }
        private void PrintTable(List<PlayingCard> table)
        {
            var u = '\u203E';

            for (int i = 0; i < table.Count; i++) { Console.Write("___________    "); }
            Console.WriteLine();
            for (int i = 0; i < table.Count; i++) { Console.Write($"|{table[i].ToString().PadRight(9)}|    "); }
            Console.WriteLine();
            for (int i = 0; i < table.Count; i++) { Console.Write("|         |    "); }
            Console.WriteLine();
            for (int i = 0; i < table.Count; i++) { Console.Write("|         |    "); }
            Console.WriteLine();
            for (int i = 0; i < table.Count; i++) { Console.Write("|         |    "); }
            Console.WriteLine();
            for (int i = 0; i < table.Count; i++) { Console.Write($"|{table[i].ToString().PadLeft(9)}|    "); }
            Console.WriteLine();
            for (int i = 0; i < table.Count; i++) { Console.Write($"{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}    "); }
            Console.WriteLine();
            for (int i = 0; i < table.Count; i++) { Console.Write($"[{table[i].PlayedBy.Name.PadRight(9)}]    "); }
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine();

        }
        private void PrintCards(List<PlayingCard> cardsOnHand)
        {
            var u = '\u203E';
            for (int i = 0; i < cardsOnHand.Count; i++) { Console.Write($"    -{i + 1}-        "); }
            Console.WriteLine();
            for (int i = 0; i < cardsOnHand.Count; i++) { Console.Write("___________    "); }
            Console.WriteLine();
            for (int i = 0; i < cardsOnHand.Count; i++) { Console.Write($"|{cardsOnHand[i].ToString().PadRight(9)}|    "); }
            Console.WriteLine();
            for (int i = 0; i < cardsOnHand.Count; i++) { Console.Write("|         |    "); }
            Console.WriteLine();
            for (int i = 0; i < cardsOnHand.Count; i++) { Console.Write("|         |    "); }
            Console.WriteLine();
            for (int i = 0; i < cardsOnHand.Count; i++) { Console.Write("|         |    "); }
            Console.WriteLine();
            for (int i = 0; i < cardsOnHand.Count; i++) { Console.Write($"|{cardsOnHand[i].ToString().PadLeft(9)}|    "); }
            Console.WriteLine();
            for (int i = 0; i < cardsOnHand.Count; i++) { Console.Write($"{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}{u}    "); }
            Console.WriteLine();
        }

        private void PrintOutHighScore()
        {
            throw new NotImplementedException();
        }

        private void GetPlayerName()
        {
            throw new NotImplementedException();
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
    }
}
