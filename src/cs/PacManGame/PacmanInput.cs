using System;

namespace PacManGame
{
    public class PacmanInput : Input
    {
        public override MoveChoice? Next(Board board, Actor actor)
        {
            var line = Console.ReadKey().KeyChar;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (line.ToString().ToLower())
            {
                case "w":
                    return MoveChoice.Up;
                case "a":
                    return MoveChoice.Left;
                case "s":
                    return MoveChoice.Down;
                case "d":
                    return MoveChoice.Right;
            }
            return null;
        }
    }
}
