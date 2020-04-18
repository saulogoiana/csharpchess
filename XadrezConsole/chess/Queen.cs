using board;
using System;

namespace chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(color, board)
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

            pos.defValues(position.line - 1, position.column);//N
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line--;
            }
            pos.defValues(position.line + 1, position.column);//S
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line++;
            }
            pos.defValues(position.line, position.column - 1);//O
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.column--;
            }
            pos.defValues(position.line, position.column + 1);//E
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.column++;
            }
            pos.defValues(position.line - 1, position.column + 1);//NE
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line--;
                pos.column++;
            }
            pos.defValues(position.line - 1, position.column - 1);//NO
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line--;
                pos.column--;
            }
            pos.defValues(position.line + 1, position.column + 1);//SE
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line++;
                pos.column++;
            }
            pos.defValues(position.line + 1, position.column - 1);//SO
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line++;
                pos.column--;
            }

            return mat;
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}