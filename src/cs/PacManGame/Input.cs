namespace PacManGame
{
    public interface IInput
    {
        MoveChoice? Next(Game game, Actor actor);
    }

    public abstract class Input : IInput
    {
        public abstract MoveChoice? Next(Game game, Actor actor);
    }
}
