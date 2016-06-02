using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacManGame
{
    public class Board
    {
        private readonly IDictionary<Coordinate, BoardStates> _map;

        public Board(Func<IEnumerable<string>> getStrings)
        {
            _map = new Dictionary<Coordinate, BoardStates>();
            ConvertToMap(getStrings().ToArray());
        }

        public int Height { get; private set; }

        public int Width { get; private set; }

        private void ConvertToMap(params string[] mapStrings)
        {
            Height = mapStrings.Length;
            Width = mapStrings.Max(s => s.Length);

            // Instead of dealing with one line at a time, just accept all of them in sequence.
            for (var j = 0; j < mapStrings.Length; j++)
            {
                for (var i = 0; i < mapStrings[j].Length; i++)
                {
                    var coordinate = new Coordinate(i, j);

                    var state = BoardStates.Empty;

                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (mapStrings[j][i])
                    {
                        case '1':
                            state = BoardStates.One;
                            break;
                        case '2':
                            state = BoardStates.Two;
                            break;
                        case '#':
                            state = BoardStates.Wall;
                            break;
                        case '*':
                            state = BoardStates.Pill;
                            break;
                    }

                    _map[coordinate] = state;
                }
            }
        }

        public Board Eat(Coordinate position)
        {
            var state = _map[position];
            // We only want to eat pill positions. Leave torsoidal positions alone.
            if (state == BoardStates.Pill)
                _map[position] = BoardStates.Empty;
            return this;
        }

        public BoardStates GetState(Coordinate position)
        {
            return _map[position];
        }

        public Coordinate FindTorsoidalDestination(Coordinate position)
        {
            var state = GetState(position);

            if (!state.IsTorsoidal())
            {
                return null;
            }

            // Single or default will not work here because we're talking about structs "instances" (value), not class instances (reference).
            var actual = _map.Where(m => m.Value == state && !m.Key.Equals(position)).ToArray();

            if (actual.Any())
            {
                return (Coordinate) actual.Single().Key.Clone();
            }

            return null;
        }

        /// <summary>
        /// Gets whether Pacman CanWin, but leaves the final decision up to the Game engine.
        /// Winning simply means there are no <see cref="BoardStates.Pill"/>s remaining.
        /// </summary>
        public bool CanWin
        {
            get { return _map.Values.Count(v => v == BoardStates.Pill) == 0; }
        }

        public string Report(Game game)
        {
            var pacman = game.Pacman;
            var monster = game.Monster;

            var sb = new StringBuilder();

            // Order by rows then columns, and group by the rows.
            var groupedKeys = _map.Keys
                .OrderBy(k => k.Y)
                .ThenBy(k => k.X)
                .GroupBy(k => k.Y);

            foreach (var group in groupedKeys)
            {
                var line = group.Aggregate(string.Empty,
                    (g, k) =>
                    {
                        //P = Pac-Man
                        //M = Monster
                        //# = Wall
                        //* = Pill
                        if (k.Equals(pacman.Position))
                        {
                            g += pacman.Alive ? "P" : "X";
                        }
                        else if (k.Equals(monster.Position))
                        {
                            g += "M";
                        }
                        else
                        {
                            // ReSharper disable once SwitchStatementMissingSomeCases
                            switch (_map[k])
                            {
                                case BoardStates.One:
                                    g += "1";
                                    break;
                                case BoardStates.Two:
                                    g += "2";
                                    break;
                                case BoardStates.Pill:
                                    g += "*";
                                    break;
                                case BoardStates.Wall:
                                    g += "#";
                                    break;
                                case BoardStates.Empty:
                                    g += " ";
                                    break;
                            }
                        }

                        return g;
                    });

                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
