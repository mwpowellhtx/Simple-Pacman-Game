using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacManGame
{
    class Game
    {
        private Board mBoard;
        private Coordinate mPacMansPosition;
        private Coordinate mMonsterPosition;
        private bool mGameOver;
        private bool mWon;

        public Game(Board pBoard) {
            mBoard = pBoard;
            mPacMansPosition = new Coordinate(1, 1);
            mMonsterPosition = new Coordinate(8,8);
        }

        public enum move {
            LEFT,
            RIGHT,
            UP,
            DOWN
        }

        public void Move (move pMove) {
            //legal move
            Coordinate newPosition;
            switch (pMove)
            {
                case move.LEFT:
                    newPosition = new Coordinate(mPacMansPosition.x - 1, mPacMansPosition.y);
                    break;
                case move.RIGHT:
                    newPosition = new Coordinate(mPacMansPosition.x + 1, mPacMansPosition.y);
                    break;
                case move.UP:
                    newPosition = new Coordinate(mPacMansPosition.x, mPacMansPosition.y - 1);
                    break;
                default:
                    newPosition = new Coordinate(mPacMansPosition.x, mPacMansPosition.y + 1);
                    break;
            }
            if (mBoard.GetState(newPosition) == BoardStates.WALL)
            {
                // dont move him
            }
            else
            {
                mPacMansPosition = newPosition;
                mBoard.Eat(newPosition);
            }

            if (mPacMansPosition.toString().Equals(mMonsterPosition.toString()))
            {
                mGameOver = true;
                mWon = false;
            }
            MoveMonster();
            if (mPacMansPosition.toString().Equals(mMonsterPosition.toString())) {
                mGameOver = true;
                mWon = false;
            }

            //win/loose
        }

        private void MoveMonster() {
            Coordinate newMonsterPosition;
            Array values = Enum.GetValues(typeof (move));
            Random random = new Random();
            move monsterMove = (move) values.GetValue(random.Next(values.Length));
            switch (monsterMove) {
                case move.LEFT:
                    newMonsterPosition = new Coordinate(mMonsterPosition.x - 1, mMonsterPosition.y);
                    break;
                case move.RIGHT:
                    newMonsterPosition = new Coordinate(mMonsterPosition.x + 1, mMonsterPosition.y);
                    break;
                case move.UP:
                    newMonsterPosition = new Coordinate(mMonsterPosition.x, mMonsterPosition.y - 1);
                    break;
                default:
                    newMonsterPosition = new Coordinate(mMonsterPosition.x, mMonsterPosition.y + 1);
                    break;
            }
            if (mBoard.GetState(newMonsterPosition) == BoardStates.WALL) {
                MoveMonster();
            }
            else {
                mMonsterPosition = newMonsterPosition;
            }
        }

        public string present() {
            if (mGameOver) {
                if (mWon) {
                return mBoard.ToString(mPacMansPosition, mMonsterPosition)+"GAME OVER YOU WON";
                }
                return mBoard.ToString(mPacMansPosition, mMonsterPosition,false) + "GAME OVER YOU LOST!!!!1!";
            }
            return mBoard.ToString(mPacMansPosition, mMonsterPosition);
        }

        internal bool GameOver
        {
            get { return mGameOver; }
        }
    }
}
