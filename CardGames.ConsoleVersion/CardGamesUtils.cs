using CardGames.Core;
using CardGames.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.ConsoleVersion
{
    public class CardGamesUtils
    {
        private readonly ICardGameCore core;

        public CardGamesUtils(ICardGameCore core)
        {
            this.core = core;
        }
        public Player GetPlayerName()
        {
            bool isRegistered;
            bool isPlayer = false;
            bool correctInput = false;

            do
            {
                string input = Console.ReadLine();
                isRegistered = core.Service.CheckIfRegistered(input);
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

                    return core.Service.GetPlayerByName(input);
                }
                else
                {
                    return core.Service.CreateNewPlayer(input);
                }
            } while (!isPlayer);


        }

    }
}
