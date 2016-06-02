using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacManGame
{
    public class Game
    {
        public Board Board { get; private set; }

        public PacmanActor Pacman { get; private set; }

        public IEnumerable<MonsterActor> Monsters { get; private set; }

        private readonly Lazy<Coordinate> _startingPacmanCoord
            = new Lazy<Coordinate>(() => new Coordinate(1, 1));

        private static Coordinate GetStartingMonsterCoord(Board board)
        {
            return new Coordinate(board.Height - 2, board.Width - 2);
        }

        public Game(Board board)
        {
            Board = board;

            var startingMonsterCoord = new Lazy<Coordinate>(() => GetStartingMonsterCoord(board));

            Pacman = new PacmanActor(new PacmanInput(),
                (Coordinate) _startingPacmanCoord.Value.Clone());

            Pacman.Moved += Pacman_Moved;

            // Assumes that the coordinate on the board is valid.
            Monsters = new[]
            {
                new MonsterActor(new MonsterInput(), (Coordinate) startingMonsterCoord.Value.Clone()),
                new MonsterActor(new MonsterInput(), (Coordinate) startingMonsterCoord.Value.Clone()),
                new MonsterActor(new MonsterInput(), (Coordinate) startingMonsterCoord.Value.Clone())
            };
        }

        /// <summary>
        /// Next as in the next potential state of the Game.
        /// </summary>
        /// <returns></returns>
        public Game Next()
        {
            Pacman.Move(this);
            return this;
        }

        public string Report()
        {
            var sb = new StringBuilder();

            var message = string.Empty;

            if (GameOver)
            {
                message = Won ? "GAME OVER YOU WON" : "GAME OVER YOU LOST!";
            }

            sb.Append(Board.Report(this));

            if (!string.IsNullOrEmpty(message))
            {
                sb.AppendLine(message);
            }

            return sb.ToString();
        }

        internal bool GameOver { get; private set; }

        private bool? _won;

        private bool Won
        {
            get
            {
                if (Board.CanWin)
                {
                    _won = true;
                }
                return _won ?? false;
            }
            set { _won = value; }
        }

        private bool TryTestPacmanPosition()
        {
            return GameOver = Monsters.Any(m => Pacman.Position.Equals(m.Position));
        }

        private bool TryTestWon()
        {
            return GameOver = Won;
        }

        private bool TryTestMonsterMoves()
        {
            // Defer to the Monster(s) move(s).
            Monsters.ToList().ForEach(m => m.Move(this));
            var gameOver = GameOver = Monsters.Any(m => Pacman.Position.Equals(m.Position));
            Pacman.Alive = !gameOver;
            return gameOver;
        }

        public void Pacman_Moved(object sender, EventArgs e)
        {
            // This is a little more concise and easier to follow state machine.
            if (TryTestPacmanPosition() || TryTestWon() || !TryTestMonsterMoves())
            {
                return;
            }

            Won = false;
        }
    }
}
