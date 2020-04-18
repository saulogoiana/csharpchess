using board;
using System;

namespace chess
{
    class King : Piece
    {
        private ChessMatch cm;
        public King(Board board, Color color, ChessMatch chessMatch):base(color,board)
        {
            this.cm = chessMatch;
        }

        public bool validMove(Position position)
        {
            Piece p = board.piece(position);
            if (p == null || p.color != color)
                return true;
            else
                return false;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            pos.defValues(position.line-1,position.column);
            if (board.positionIsValid(pos)&&validMove(pos))//N
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line - 1, position.column - 1); //NO
            if(board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line - 1, position.column + 1); //NE
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line, position.column - 1); //O
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line, position.column + 1); //E
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line + 1, position.column); //S
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line + 1, position.column - 1); //SO
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line + 1, position.column + 1); //SE
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
