using board;
using System;
using System.Security.Cryptography;

namespace chess
{
    class Pawn : Piece
    {
        private ChessMatch cm;
        public Pawn(Board board, Color color,ChessMatch chessMatch) : base(color, board)
        {
            this.cm = chessMatch;
        }

        public bool validMove(Position position)
        {
            Piece p = board.piece(position);
            if (p == null)
                return true;
            else
                return false;
        }

        public bool existEnemy(Position position)
        {
            Piece p = board.piece(position);
            if (p != null && p.color != color)
            {
                return true;
            }
            else
                return false;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];
            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.defValues(position.line - 1, position.column);//N
                if (board.positionIsValid(pos) && validMove(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defValues(position.line - 2, position.column);//N
                if (board.positionIsValid(pos) && validMove(pos) && qttmoves == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defValues(position.line - 1, position.column +1);//NE
                if (board.positionIsValid(pos) && existEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defValues(position.line - 1, position.column -1);//NO
                if (board.positionIsValid(pos) && existEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

            }
            else
            {
                pos.defValues(position.line + 1, position.column);//S
                if (board.positionIsValid(pos) && validMove(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defValues(position.line + 2, position.column);//S
                if (board.positionIsValid(pos) && validMove(pos) && qttmoves == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defValues(position.line + 1, position.column + 1);//SE
                if (board.positionIsValid(pos) && existEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.defValues(position.line + 1, position.column - 1);//SO
                if (board.positionIsValid(pos) && existEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
            }
            
            return mat;
        }

        public override string ToString()
        {
            return "p";
        }
    }
}