using board;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(color, board)
        {
        }

        public bool validMove(Position position)
        {
            Piece p = board.piece(position);
            if(p == null || p.color != color)
                return true;
            else
                return false;
        }



        public override string ToString()
        {
            return "T";
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];
            Position pos = new Position(0,0);

            pos.defValues(position.line-1, position.column);//N
            while(board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if(board.piece(pos)!=null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line--;
            }
            pos.defValues(position.line+1, position.column);//S
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.line++;
            }
            pos.defValues(position.line, position.column-1);//O
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.column--;
            }
            pos.defValues(position.line, position.column+1);//E
            while (board.positionIsValid(pos) && validMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.column++;
            }

            return mat;
        }
    }
}