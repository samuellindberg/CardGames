using System;
using System.Text;
using CardGames.Core;
using CardGames.Core.Cards;

namespace CardGames.ConsoleVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            MainMenu();

        }

        public static void MainMenu()
        {
            bool isRunning = true;

            do
            {
                Console.Clear();
                Console.WriteLine("C A R D   G A M E S");
                Console.WriteLine();
                Console.WriteLine("[1] High Or Low");
                Console.WriteLine("[Q]uit");
                Console.WriteLine();
                Console.Write(">");
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        var game = new HighOrLowConsole();
                        game.Play();
                        break;
                    case ConsoleKey.Q:
                        isRunning = false;
                        break;
                    default:
                        break;
                }

            } while (isRunning);
        }
    }
}
