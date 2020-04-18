using System.Globalization;

namespace board
{
    class Board
    {
        public int lines { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Board(int lin, int col)
        {
            this.lines = lin;
            this.columns = col;
            pieces = new Piece[lin, col];
        }

        public Piece piece(int lin, int col)
        {
            return pieces[lin, col];
        }

        public Piece piece(Position pos)
        {
            return pieces[pos.line, pos.column];
        }

        public void addPiece(Piece p, Position pos)
        {
            if (existPiece(pos))
            {
                throw new BoardException("Alread exist a piece in this position!");
            }
            pieces[pos.line, pos.column] = p;
            p.position = pos;
        }

        public Piece rmvPiece(Position pos)
        {
            if (piece(pos) == null)
            {
                return null;
            }
            else
            {
                Piece aux = piece(pos);
                aux.position = null;
                pieces[pos.line, pos.column] = null;
                return aux;
            }
        }

        public bool positionIsValid(Position pos)
        {
            if (pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= columns)
            {
                return false;
            }
            else
                return true;
        }


        public void validatePosition(Position pos)
        {
            if (!positionIsValid(pos))
                throw new BoardException("Invalid Position!");
        }

        public bool existPiece(Position pos)
        {
            validatePosition(pos);
            return piece(pos) != null;
        }

    }
}
