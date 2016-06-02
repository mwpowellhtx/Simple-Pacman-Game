using System;

namespace PacManGame
{
    // ReSharper disable once ArrangeTypeModifiers
    class Program
    {
        // ReSharper disable once ArrangeTypeMemberModifiers
        static void Main()
        {
            var game = new Game(new Board());
            do
            {
                // Report on the game and advance while not game over.
                Console.WriteLine(game.Report());
                game.Next();
            } while (!game.GameOver);
            // Write the final game report.
            Console.WriteLine(game.Report());
            Console.Write("Press any key to continue");
            Console.ReadKey(true);
        }
    }
}
