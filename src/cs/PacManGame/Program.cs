using System;
using System.Collections.Generic;

namespace PacManGame
{
    // ReSharper disable once ArrangeTypeModifiers
    class Program
    {
        private static IEnumerable<string> GetTenByTenMapStrings()
        {
            yield return "######1###";
            yield return "# *******#";
            yield return "2*##**##*#";
            yield return "#********#";
            yield return "#*#*##*#*#";
            yield return "#*#*##*#*#";
            yield return "#********#";
            yield return "#*##**##*2";
            yield return "#********#";
            yield return "###1######";
        }

        public static IEnumerable<string> GetTwentyByTwentyMapStrings()
        {
            /* TODO: TBD: could probably implement a couple of constraint solvers in order to
             * generate maps based on a couple of described constraints: i.e. map sides should be
             * walls, there should be two torsoidal pairs, etc, etc. */

            //            01234567890123456789
            yield return "############1#######"; // 0
            yield return "#******************#"; // 1
            yield return "#*###*##**##*#####*#"; // 2
            yield return "#*#************#***#"; // 3
            yield return "2***##*###*###*###*#"; // 4
            yield return "#*###***##**##***#*#"; // 5
            yield return "#*#**********###***#"; // 6
            yield return "#*#*##**###***###**#"; // 7
            yield return "#*#****************#"; // 8
            yield return "#***####*##*####*#*2"; // 9
            yield return "#*#****####**#***#*#"; // 0
            yield return "#*####******##*###*#"; // 1
            yield return "#******###*********#"; // 2
            yield return "#*####*##**######**#"; // 3
            yield return "#****#*###***#*###*#"; // 4
            yield return "#*####**####*******#"; // 5
            yield return "#*#********#**#**#*#"; // 6
            yield return "#*#####*####**####*#"; // 7
            yield return "#******************#"; // 8
            yield return "####1###############"; // 9
        }

        // ReSharper disable once ArrangeTypeMemberModifiers
        static void Main()
        {
            //var game = new Game(new Board(GetTenByTenMapStrings));
            var game = new Game(new Board(GetTwentyByTwentyMapStrings));
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
