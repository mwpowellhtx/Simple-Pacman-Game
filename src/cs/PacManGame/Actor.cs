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

        public Coordinate Position { get; protected set; }

        public abstract Actor Move(Board board, MoveChoice? choice = null);

        protected Actor(Coordinate position)
        {
            Alive = true;
            Position = position;
        }
    }
}
