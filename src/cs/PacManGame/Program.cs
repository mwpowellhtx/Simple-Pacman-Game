using System;

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
            while (!game.GameOver)
            {
                game.Next();
                Console.WriteLine();
                Console.WriteLine(game.Report());
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
    }
}
