using System;

namespace PacManGame
{
    public class PacmanActor : Actor
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public PacmanActor(PacmanInput input, Coordinate position)
            : base(input, position)
        {
        }

        public override Actor Move(Game game)
        {
            var board = game.Board;

            var choice = Input.Next(game, this);

            // God help you if you ran into a wall. Do nothing in that instance.
            var position = Position.Apply(board, choice);

            if (board.GetState(position) != BoardStates.Wall)
            {
                Position = position;

                board.Eat(position);
            }

            OnMoved(EventArgs.Empty);

            return this;
        }
    }
}
