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

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (choice)
            {
                case MoveChoice.Left:
                    result.X--;
                    break;
                case MoveChoice.Right:
                    result.X++;
                    break;
                case MoveChoice.Up:
                    result.Y--;
                    break;
                case MoveChoice.Down:
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
