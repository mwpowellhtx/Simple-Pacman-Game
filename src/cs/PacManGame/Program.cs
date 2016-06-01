using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PacManGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Game game = new Game(board);
            Console.WriteLine(game.present());
            while (!game.GameOver) {
                string line = Console.ReadKey().KeyChar.ToString();
                if (line.ToLower().Equals("w")) {
                    game.Move(Game.move.UP);
                }
                else if (line.ToLower().Equals("a")) {
                    game.Move(Game.move.LEFT);
                }
                else if (line.ToLower().Equals("s")) {
                    game.Move(Game.move.DOWN);
                }
                else if (line.ToLower().Equals("d")) {
                    game.Move(Game.move.RIGHT);
                }
                Console.WriteLine();
                Console.WriteLine(game.present());
                Thread.Sleep(4000);
            }
        }
    }

}
