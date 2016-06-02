using System;

namespace PacManGame
{
    public class PacmanActor : Actor
    {
        public PacmanActor(Coordinate position)
            : base(position)
        {
        }

        public override Actor Move(Board board, MoveChoice? choice = null)
        {
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
