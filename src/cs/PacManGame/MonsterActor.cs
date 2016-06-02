using System;
using System.Linq;

namespace PacManGame
{
    public class MonsterActor : Actor
    {
        public MonsterActor(Coordinate position)
            : base(position)
        {
        }

        public override Actor Move(Board board, MoveChoice? choice = null)
        {
            //TODO: TBD: also assumes that we have done some QA on the board cells, valid paths, etc...

            // Help restrict the values we want. Otherwise the monster logic will be indecisive.
            var exceptValues = EnumHelper<MoveChoice>.GetValues()
                .Where(c =>
                {
                    var state = board.GetState(Position.Apply(board, c));
                    return state == BoardStates.Wall;
                }).ToArray();

            // We can inject a choice in for the monster or select one at random.
            choice = choice ?? EnumHelper<MoveChoice>.ElementAtRandom(exceptValues);

            // In which case, we are guaranteed for the monster choices not to run into any walls.
            Position = Position.Apply(board, choice);

            OnMoved(EventArgs.Empty);

            return this;
        }
    }
}
