using CardGames.Core.Cards;
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

            do
            {
                Console.Clear();
                Header();
                //Console.WriteLine($"{core.Player.Name}'s score: {core.Player.HighOrLowScore}");
                Console.WriteLine();
                Console.ReadKey();


            } while (true);
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
