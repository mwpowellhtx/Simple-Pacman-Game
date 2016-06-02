using System;

namespace PacManGame
{
    public class Coordinate
        : IEquatable<Coordinate>
            , ICloneable
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal Coordinate Apply(Board board, MoveChoice? choice = null)
        {
            var result = (Coordinate) Clone();

            Func<Coordinate> findTorsoidalDest = () => board.FindTorsoidalDestination(result);

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (choice)
            {
                case MoveChoice.Left:
                    if (result.X == 0)
                    {
                        return findTorsoidalDest();
                    }
                     result.X--;
                    break;
                case MoveChoice.Right:
                    if (result.X == board.Width - 1)
                    {
                        return findTorsoidalDest();
                    }
                    result.X++;
                    break;
                case MoveChoice.Up:
                    if (result.Y == 0)
                    {
                        return findTorsoidalDest();
                    }
                    result.Y--;
                    break;
                case MoveChoice.Down:
                    if (result.Y == board.Width - 1)
                    {
                        return findTorsoidalDest();
                    }
                    result.Y++;
                    break;
                // ReSharper disable once RedundantEmptyDefaultSwitchBranch
                default: // do nothing in this event
                    break;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        private static bool Equals(Coordinate a, Coordinate b)
        {
            return !(a == null || b == null)
                   && (ReferenceEquals(a, b)
                       || (a.X == b.X && a.Y == b.Y));
        }

        public bool Equals(Coordinate other)
        {
            return Equals(this, other);
        }

        public object Clone()
        {
            return new Coordinate(X, Y);
        }

        public override string ToString()
        {
            return string.Format("{{{0}, {1}}}", X, Y);
        }
    }
}
