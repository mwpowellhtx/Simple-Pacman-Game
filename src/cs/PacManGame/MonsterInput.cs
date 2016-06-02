using System.Linq;

namespace PacManGame
{
    public class MonsterInput : Input
    {
        public override MoveChoice? Next(Board board, Actor actor)
        {
            // Help restrict the values we want. Otherwise the monster logic will be indecisive.
            var exceptValues = EnumHelper<MoveChoice>.GetValues()
                .Where(c =>
                {
                    var state = board.GetState(actor.Position.Apply(board, c));
                    return state == BoardStates.Wall;
                }).ToArray();

            // We can inject a choice in for the monster or select one at random.
            var choice = EnumHelper<MoveChoice>.ElementAtRandom(exceptValues);

            return choice;
        }
    }
}
