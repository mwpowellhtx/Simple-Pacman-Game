using System.Linq;

namespace PacManGame
{
    /// <summary>
    /// Represents a basic randomly generated movement algorithm. The monsters will not be very
    /// smart, in fact they will be quite dumb, as a result.
    /// </summary>
    public class MonsterInput : Input
    {
        public override MoveChoice? Next(Game game, Actor monster)
        {
            var board = game.Board;

            // Help restrict the values we want. Otherwise the monster logic will be indecisive.
            var exceptValues = EnumHelper<MoveChoice>.GetValues()
                .Where(c =>
                {
                    var state = board.GetState(monster.Position.Apply(board, c));
                    // This part should be consistent regardless whether talking about a torsoidal or normal movement.
                    return state == BoardStates.Wall;
                }).ToArray();

            // We can inject a choice in for the monster or select one at random.
            var choice = EnumHelper<MoveChoice>.GetPossibleValues(exceptValues).ElementAtRandom();

            return choice;
        }
    }
}
