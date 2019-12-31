using CardGames.Core.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.ConsoleVersion
{
    public class ChicagoConsole
    {
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
                    GetPlayerName();
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

        private void PrintOutHighScore()
        {
            throw new NotImplementedException();
        }

        private void RunGame()
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
