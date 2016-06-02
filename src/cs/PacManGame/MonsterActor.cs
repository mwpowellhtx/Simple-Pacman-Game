using System;

namespace PacManGame
{
    public class MonsterActor : Actor
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public MonsterActor(MonsterInput input, Coordinate position)
            : base(input, position)
        {
        }

        public override Actor Move(Board board)
        {
            //TODO: TBD: also assumes that we have done some QA on the board cells, valid paths, etc...
            var choice = Input.Next(board, this);

            // In which case, we are guaranteed for the monster choices not to run into any walls.
            Position = Position.Apply(board, choice);

            OnMoved(EventArgs.Empty);

            return this;
        }
    }
}
