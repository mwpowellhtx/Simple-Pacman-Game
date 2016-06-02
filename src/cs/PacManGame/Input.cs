namespace PacManGame
{
    public interface IInput
    {
        MoveChoice? Next(Board board, Actor actor);
    }

    public abstract class Input : IInput
    {
        public abstract MoveChoice? Next(Board board, Actor actor);
    }
}
