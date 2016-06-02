using System;

namespace PacManGame
{
    public abstract class Actor
    {
        public bool Alive { get; set; }

        /// <summary>
        /// The Moved event helps us to communicate between the Actors
        /// and the Game throughout the movement sequence.
        /// </summary>
        public event EventHandler<EventArgs> Moved;

        protected void OnMoved(EventArgs e)
        {
            if (Moved == null) return;
            Moved(this, e);
        }

        protected IInput Input { get; private set; }

        public Coordinate Position { get; protected set; }

        public abstract Actor Move(Board board);

        protected Actor(IInput input, Coordinate position)
        {
            Alive = true;
            Input = input;
            Position = position;
        }
    }
}
