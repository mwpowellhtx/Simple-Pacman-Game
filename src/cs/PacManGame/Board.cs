using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacManGame
{
    public class Board
    {
        private readonly IDictionary<Coordinate, BoardStates> _map;

        public Board()
        {
            _map = new Dictionary<Coordinate, BoardStates>();
            ConvertToMap(
                "##########",
                "# *******#",
                "#*##**##*#",
                "#********#",
                "#*#*##*#*#",
                "#*#*##*#*#",
                "#********#",
                "#*##**##*#",
                "#********#",
                "##########");
        }

        private void ConvertToMap(params string[] mapStrings)
        {
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
            _map[position] = BoardStates.Empty;
            return this;
        }

        public BoardStates GetState(Coordinate position)
        {
            return _map[position];
        }

        /// <summary>
        /// Gets whether Pacman CanWin, but leaves the final decision up to the Game engine.
        /// </summary>
        public bool CanWin
        {
            get { return !_map.Values.Select(v => v == BoardStates.Pill).Any(); }
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
