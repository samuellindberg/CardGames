using System;
using CardGames.Core;
using CardGames.Core.Cards;

namespace CardGames.ConsoleVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
            
        }

        public static void MainMenu()
        {
            Console.WriteLine("C A R D   G A M E S");
            Console.WriteLine();
            Console.WriteLine("[1] High Or Low");
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
                default:
                    break;
            }
        }
    }
}
