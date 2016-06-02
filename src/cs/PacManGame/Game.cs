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
            Pacman = new PacmanActor(new Coordinate(1, 1));
            Pacman.Moved += Pacman_Moved;
            Monster = new MonsterActor(new Coordinate(8, 8));
        }

        public Game Move(MoveChoice choice)
        {
            Pacman.Move(_board, choice);
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

        public void Pacman_Moved(object sender, EventArgs e)
        {
            if (Pacman.Position.Equals(Monster.Position))
            {
                GameOver = true;
                Won = false;
            }

            if (GameOver) return;

            Monster.Move(_board);

            // ReSharper disable once InvertIf
            if (Pacman.Position.Equals(Monster.Position))
            {
                GameOver = true;
                Won = false;
            }
        }
    }
}
