using System;
using System.Collections.Generic;
using System.Linq;

namespace PacManGame
{
    /* TODO: TBD: there you go: potential future directions for the package might
     * involve some sort of over-arching flow field Monster movement government. */
    /// <summary>
    /// Best path Monster input is a single Monster algorithm, meaning that no coordination
    /// occurs between Monsters maximizing an efficient flow field search pattern for Pacman.
    /// </summary>
    public class BestPathMonsterInput : MonsterInput
    {
        private readonly IEnumerable<MoveChoice> _availableChoices;

        public BestPathMonsterInput()
        {
            _availableChoices = EnumHelper<MoveChoice>.GetPossibleValues().ToArray();
        }

        private class InputPath : ICloneable
        {
            public Coordinate Current { get; private set; }

            internal Coordinate Destination { get; private set; }

            public IList<MoveChoice> History { get; private set; }

            public IDictionary<Coordinate, MoveChoice?> Visitations { get; private set; }

            internal InputPath(Coordinate current, Coordinate destination)
                : this(current, destination, new List<MoveChoice>(),
                    new Dictionary<Coordinate, MoveChoice?>())
            {
                // Does not matter starting out, apart from recording that we have been there.
                Visitations[current] = null;
            }

            internal InputPath(Coordinate current, Coordinate destination,
                IEnumerable<MoveChoice> history, IDictionary<Coordinate, MoveChoice?> visitations)
            {
                Current = current;
                Destination = destination;
                History = history.ToList();
                Visitations = visitations.ToDictionary(x => x.Key, x => x.Value);
            }

            public object Clone()
            {
                return new InputPath((Coordinate) Current.Clone(), Destination, History, Visitations);
            }

            public bool IsFound
            {
                get { return Current.Equals(Destination); }
            }
        }

        /// <summary>
        /// Recursive algorithm calculating potentially the best Monster input paths. We make the
        /// huge assumption that there are no corners on the map in which a path cannot be formed.
        /// If we should discover that, this is more of a quality assurance step testing for map
        /// validation.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        /// <remarks>Recursion might not be the best choice for performance reasons, but it will
        /// most certainly get the job done.</remarks>
        private IEnumerable<InputPath> GetBestInputPaths(Game game, IList<InputPath> paths)
        {
            /* The recursive terminal use case. Careful, though, we could end up with
             * tens of thousands of paths, hundreds of which are potential candidates. */
            if (paths.Any(p => p.IsFound))
            {
                return paths.Where(p => p.IsFound)
                    .OrderBy(p => p.History.Count)
                    .GroupBy(p => p.History.Count).First();
            }

            var board = game.Board;
            // Basically, allowing for any Empty or Pill cell.
            var invalidStates = new[] {BoardStates.Wall, BoardStates.One, BoardStates.Two};

            var eligible = new List<InputPath>();

            foreach (var path in paths)
            {
                var p = path;

                var staged = _availableChoices.Select(c =>
                {
                    var applied = p.Current.Apply(board, c);

                    // Allow valid choices and where we have not yet been.
                    if (invalidStates.Contains(board.GetState(applied))
                        || p.Visitations.ContainsKey(applied))
                    {
                        return null;
                    }

                    // Not a clone but rather the next, using Applied for Current.
                    var next = new InputPath(applied, p.Destination, p.History, p.Visitations);

                    next.History.Add(c);
                    next.Visitations[applied] = c;

                    return next;
                }).Where(x => x != null).ToArray();

                if (staged.Any())
                {
                    eligible.AddRange(staged);
                }
            }

            // TODO: TBD: recursion is likely not the smartest approach here...
            // ReSharper disable once TailRecursiveCall
            return GetBestInputPaths(game, eligible);
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        /// <summary>
        /// Helps the Monster to &quot;find&quot; (so-called) Pacman.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="pacman"></param>
        /// <returns></returns>
        private static Coordinate GetNearestPosition(Board board, PacmanActor pacman)
        {
            switch (board.GetState(pacman.Position))
            {
                case BoardStates.One:
                case BoardStates.Two:
                    var position = (Coordinate) pacman.Position.Clone();
                    if (position.X == 0)
                    {
                        position.X++;
                    }
                    else if (position.Y == 0)
                    {
                        position.Y++;
                    }
                    else if (position.X == board.Width - 1)
                    {
                        position.X--;
                    }
                    else if (position.Y == board.Height - 1)
                    {
                        position.Y--;
                    }
                    return position;

                default:
                    return pacman.Position;
            }
        }

        public override MoveChoice? Next(Game game, Actor monster)
        {
            var board = game.Board;

            var paths = new List<InputPath>
            {
                new InputPath(monster.Position, GetNearestPosition(board, game.Pacman))
            };

            var bestPaths = GetBestInputPaths(game, paths).ToArray();

            // TODO: potentially infinite recursion? If so, that's more of a case of invalid map than not finding pacman?
            if (!bestPaths.Any())
            {
                return base.Next(game, monster);
            }

            var selected = bestPaths.ElementAtRandom();

            return selected.History.First();
        }
    }
}
