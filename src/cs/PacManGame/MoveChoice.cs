using System.Collections.Generic;

namespace PacManGame
{
    public enum MoveChoice
    {
        Left,
        Right,
        Up,
        Down
    }

    public static class MoveChoiceExtensionMethods
    {
        private static readonly IDictionary<MoveChoice, MoveChoice> InverseChoices
            = new Dictionary<MoveChoice, MoveChoice>
            {
                {MoveChoice.Left, MoveChoice.Right},
                {MoveChoice.Right, MoveChoice.Left},
                {MoveChoice.Up, MoveChoice.Down},
                {MoveChoice.Down, MoveChoice.Up}
            };

        public static MoveChoice Invert(this MoveChoice choice)
        {
            return InverseChoices[choice];
        }
    }
}
