namespace PacManGame
{
    public enum BoardStates
    {
        One,
        Two,
        Wall,
        Pill,
        Empty
    }

    public static class BoardStateExtensionMethods
    {
        public static bool IsTorsoidal(this BoardStates state)
        {
            return state == BoardStates.One || state == BoardStates.Two;
        }
    }
}
