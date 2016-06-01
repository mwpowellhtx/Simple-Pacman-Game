using System;
using System.Collections.Generic;
using System.Text;

namespace PacManGame
{
    class Board {

        private Dictionary<string, BoardStates> map;

        public Board() {
            map = new Dictionary<string, BoardStates>();
            convertToMap("##########");
            convertToMap("# *******#");
            convertToMap("#*##**##*#");
            convertToMap("#********#");
            convertToMap("#*#*##*#*#");
            convertToMap("#*#*##*#*#");
            convertToMap("#********#");
            convertToMap("#*##**##*#");
            convertToMap("#********#");
            convertToMap("##########");
        }

        private int lineCount = 0;
        private void convertToMap(string pMapString) {
            for (int i = 0; i < pMapString.Length; i++) {
                Coordinate coordinate = new Coordinate(i, lineCount);
                BoardStates state;
                if(pMapString[i].Equals('#')) {
                    state = BoardStates.WALL;
                }
                else if(pMapString[i].Equals('*')) {
                    state = BoardStates.PILL;
                }
                else {
                    state = BoardStates.EMPTY;
                }
                map.Add(coordinate.toString(),state);
            }

            lineCount++;
        }

        public string ToString(Coordinate pPacMan, Coordinate pMonster, bool pPacLives = true) {
            string[] board = new string[10];
            for (int i = 0; i < 10; i++) {
                board[i] = "";
            }

            //P = Pac-Man
            //M = Monster
            //# = Wall
            //* = Pill
            foreach (KeyValuePair<string, BoardStates> boardStatese in map) {
                string character = "";
                if (!pPacLives) {
                    character = "X";
                }
                else if(boardStatese.Key.Equals(pPacMan.toString())){
                    character = "P";
                }
                else if (boardStatese.Key.Equals(pMonster.toString())) {
                    character = "M";
                } else if (boardStatese.Value == BoardStates.WALL) {
                    character = "#";
                } else if (boardStatese.Value == BoardStates.PILL) {
                    character = "*";
                } else {
                    character = " ";
                }


               board[Convert.ToInt32(boardStatese.Key.Substring(2, 2))] = board[Convert.ToInt32(boardStatese.Key.Substring(2,2))].Insert(Convert.ToInt32(boardStatese.Key.Substring(0,2)), character);
            }
            StringBuilder test = new StringBuilder();
            foreach (string s in board) {
                test.AppendLine(s);
            }
            return test.ToString();
        }

        public BoardStates GetState(Coordinate pNewPosition) {
            return map[pNewPosition.toString()];
        }
        
        public void Eat(Coordinate pNewPosition) {
            map[pNewPosition.toString()] = BoardStates.EMPTY;
        }
    }

    public enum BoardStates {
        WALL,
        PILL,
        EMPTY
    }


    public class Coordinate  {
        public int x;
        public int y;

        public Coordinate(int pX, int pY) {
            x = pX;
            y = pY;
        }

        public new string toString() {
            return string.Format("{0}{1}", x.ToString("D2"), y.ToString("D2"));
        }

    }
}
