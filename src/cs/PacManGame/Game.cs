using System;
using System.Text;

namespace PacManGame
{
    public class Game
    {
        private readonly Board _board;

        public PacmanActor Pacman { get; private set; }

        public MonsterActor Monster { get; private set; }

        public Game(Board board)
        {
            _board = board;

            Pacman = new PacmanActor(new PacmanInput(), new Coordinate(1, 1));

            Pacman.Moved += Pacman_Moved;

            Monster = new MonsterActor(new MonsterInput(),
                // Assumes that the coordinate on the board is valid.
                new Coordinate(board.Height - 2, board.Width - 2));
        }

        /// <summary>
        /// Next as in the next potential state of the Game.
        /// </summary>
        /// <returns></returns>
        public Game Next()
        {
            Pacman.Move(_board);
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

            sb.Append(_board.Report(this));

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
                if (_board.CanWin)
                {
                    _won = true;
                }
                return _won ?? false;
            }
            set { _won = value; }
        }

        private bool TryTestPacmanPosition()
        {
            return GameOver = Pacman.Position.Equals(Monster.Position);
        }

        private bool TryTestWon()
        {
            return GameOver = Won;
        }

        private bool TryTestMonsterMoves()
        {
            // Defer to the Monster(s) move(s).
            Monster.Move(_board);
            var gameOver = GameOver = Pacman.Position.Equals(Monster.Position);
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
