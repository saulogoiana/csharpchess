using board;
using System;

namespace chess
{
    class Horse : Piece
    {
        public Horse(Board board, Color color) : base(color, board)
        {
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

            pos.defValues(position.line - 1, position.column -2);
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line - 2, position.column - 1); //NO
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line - 2, position.column + 1); //NE
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line -1, position.column +2); //O
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line +1, position.column + 2); //E
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line + 2, position.column+1); //S
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line + 2, position.column - 1); //SO
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.defValues(position.line + 1, position.column-2); //SE
            if (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "H";
        }
    }
}