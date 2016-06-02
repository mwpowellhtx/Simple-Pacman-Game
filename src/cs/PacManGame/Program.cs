using System;
using System.Threading;

namespace PacManGame
{
    // ReSharper disable once ArrangeTypeModifiers
    class Program
    {
        // ReSharper disable once ArrangeTypeMemberModifiers
        static void Main(string[] args)
        {
            var game = new Game(new Board());
            Console.WriteLine(game.Report());
            // TODO: TBD: could defer this to a first class dependency injectable service
            while (!game.GameOver)
            {
                var line = Console.ReadKey().KeyChar;
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (line.ToString().ToLower())
                {
                    case "w":
                        game.Move(MoveChoice.Up);
                        break;
                    case "a":
                        game.Move(MoveChoice.Left);
                        break;
                    case "s":
                        game.Move(MoveChoice.Down);
                        break;
                    case "d":
                        game.Move(MoveChoice.Right);
                        break;
                }
                Console.WriteLine();
                Console.WriteLine(game.Report());
                //Thread.Sleep(4000);
            }
        }
    }
}
