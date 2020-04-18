using System.Text.RegularExpressions;

namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; set; }
        public int qttmoves { get; set; }
        public Board board { get; set; }

        public Piece(Color color, Board board)
        {
            this.position = null;
            this.color = color;
            this.board = board;
            this.qttmoves = 0;
        }

        public bool existPossMove()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < board.lines; i++)
            {
                for(int j =0;j<board.columns;j++)
                {
                    if (mat[i, j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool possibleMove(Position pos)
        {
            return possibleMoves()[pos.line, pos.column];
        }

        public void incrementMoves()
        {
            this.qttmoves++;
        }
        public void decrementMoves()
        {
            this.qttmoves--;
        }

        public abstract bool[,] possibleMoves();
    }
}
