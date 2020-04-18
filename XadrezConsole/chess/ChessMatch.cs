using board;
using System.Collections.Generic;

namespace chess
{
    class ChessMatch
    {
        public Board board { get; set; }
        public int rounds { get; private set; }
        public Color actPlayer { get; private set; }
        public bool over { get; set; }
        public bool check { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capPieces;
        public Piece vunerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            rounds = 1;
            actPlayer = Color.White;
            over = false;
            check = false;
            pieces = new HashSet<Piece>();
            capPieces = new HashSet<Piece>();
            insertPieces();
        }

        public Piece execMove(Position orig, Position dest)
        {
            Piece p = board.rmvPiece(orig);
            p.incrementMoves();
            Piece rmP = board.rmvPiece(dest);
            board.addPiece(p, dest);
            if (rmP != null)
                capPieces.Add(rmP);

            //Low Rock
            if(p is King && dest.column == orig.column + 2)
            {
                Position origT = new Position(orig.line,orig.column+3);
                Position destT = new Position(orig.line, orig.column + 1);
                Piece T = board.rmvPiece(origT);
                T.incrementMoves();
                board.addPiece(T, destT);
            }

            //High Rock
            if (p is King && dest.column == orig.column - 2)
            {
                Position origT = new Position(orig.line, orig.column - 4);
                Position destT = new Position(orig.line, orig.column - 1);
                Piece T = board.rmvPiece(origT);
                T.incrementMoves();
                board.addPiece(T, destT);
            }

            //En Passant
            if(p is Pawn)
            {
                if(orig.column != dest.column && rmP == null)
                {
                    Position posP;
                    if(p.color == Color.White)
                    {
                        posP = new Position(dest.line + 1, dest.column);
                    }
                    else
                    {
                        posP = new Position(dest.line - 1, dest.column);
                    }
                    rmP = board.rmvPiece(posP);
                    capPieces.Add(rmP);
                }
            }

            return rmP;
        }

        public void unMove(Position orig, Position dest, Piece piece)
        {
            Piece p = board.rmvPiece(dest);
            p.decrementMoves();
            if (piece != null)
            {
                board.addPiece(piece, dest);
                capPieces.Remove(piece);
            }
            board.addPiece(p, orig);

            //Low Rock
            if (p is King && dest.column == orig.column + 2)
            {
                Position origT = new Position(orig.line, orig.column + 3);
                Position destT = new Position(orig.line, orig.column + 1);
                Piece T = board.rmvPiece(origT);
                T.incrementMoves();
                board.addPiece(T, destT);
            }

            //High Rock
            if (p is King && dest.column == orig.column - 2)
            {
                Position origT = new Position(orig.line, orig.column - 4);
                Position destT = new Position(orig.line, orig.column - 1);
                Piece T = board.rmvPiece(origT);
                T.incrementMoves();
                board.addPiece(T, destT);
            }

            //En Passant
            if (p is Pawn)
            {
                if (orig.column != dest.column && piece == null)
                {
                    Piece pawn = board.rmvPiece(dest);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, dest.column);
                    }
                    else
                    {
                        posP = new Position(4, dest.column);
                    }
                    board.addPiece(pawn, posP);
                }
            }
        }

        public bool isInCheckMate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }

            foreach(Piece x in ingamePieces(color))
            {
                bool[,] mat = x.possibleMoves();
                for(int i = 0; i < board.lines; i++)
                {
                    for(int j=0; j<board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position dest = new Position(i, j);
                            Position origin = x.position;
                            Piece p = execMove(x.position, dest);
                            bool testcheck = isInCheck(color);
                            unMove(origin, dest, p);
                            if (!testcheck)
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            return true;
        }

        public void doPlay(Position orig, Position dest)
        {
            Piece rmPiece = execMove(orig, dest);

            if (isInCheck(actPlayer))
            {
                unMove(orig, dest, rmPiece);
                throw new BoardException("Your King is in check!! Move him!");
            }

            Piece p = board.piece(dest);
            //Promotion
            if(p is Pawn)
            {
                if((p.color == Color.White && dest.line ==0) || (p.color == Color.Black && dest.line == 7))
                {
                    p = board.rmvPiece(dest);
                    pieces.Remove(p);
                    Piece queen = new Queen(board, p.color);
                    board.addPiece(queen,dest);
                    pieces.Add(queen);
                }
            }

            if (isInCheck(adversary(actPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (isInCheckMate(adversary(actPlayer)))
            {
                over = true;
            }
            else
            {
                rounds++;
                changePlayer();
            }
        }

        

        private Color adversary(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece findKing(Color color)
        {
            foreach (Piece x in ingamePieces(color))
            {
                if (x is King)
                    return x;
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece K = findKing(color);
            if (K == null)
            {
                throw new BoardException("King not finded in game!!");
            }

            foreach (Piece x in ingamePieces(adversary(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[K.position.line, K.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public void validatePlay(Position position)
        {
            if (board.piece(position) == null)
                throw new BoardException("Doesnt exist a piece in position!");
            if (actPlayer != board.piece(position).color)
                throw new BoardException("The selected piece isnt yours!");
            if (!board.piece(position).existPossMove())
                throw new BoardException("The selected piece dont have possible moves!");
        }

        public void validatePlay(Position position, Position destiny)
        {
            if (!board.piece(position).possibleMove(destiny))
                throw new BoardException("Invalid Move!");
        }

        public void changePlayer()
        {
            if (actPlayer == Color.White)
                actPlayer = Color.Black;
            else
                actPlayer = Color.White;
        }

        public void agroupPiece(Piece p)
        {
            pieces.Add(p);
        }

        public HashSet<Piece> capturedPieces(Color c)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in capPieces)
            {
                if (x.color == c)
                    aux.Add(x);
            }
            return aux;
        }
        public HashSet<Piece> ingamePieces(Color c)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == c)
                    aux.Add(x);
            }
            aux.ExceptWith(capturedPieces(c));
            return aux;
        }
        public void insertNewPiece(int lin, int col, Piece p)
        {
            board.addPiece(p, new Position(lin,col));
            pieces.Add(p);
        }

        public void insertPieces()
        {
            //Towers
            insertNewPiece(0, 7, new Tower(board, Color.Black));
            insertNewPiece(0, 0, new Tower(board,Color.Black));
            insertNewPiece(7, 7, new Tower(board, Color.White));
            insertNewPiece(7, 0, new Tower(board, Color.White));
            //Horses
            insertNewPiece(0, 6, new Horse(board, Color.Black));
            insertNewPiece(0, 1, new Horse(board, Color.Black));
            insertNewPiece(7, 6, new Horse(board, Color.White));
            insertNewPiece(7, 1, new Horse(board, Color.White));
            //Bishops
            insertNewPiece(0, 5, new Bishop(board, Color.Black));
            insertNewPiece(0, 2, new Bishop(board, Color.Black));
            insertNewPiece(7, 5, new Bishop(board, Color.White));
            insertNewPiece(7, 2, new Bishop(board, Color.White));
            //Kings
            insertNewPiece(0, 3, new King(board, Color.Black,this));
            insertNewPiece(7, 3, new King(board, Color.White,this));
            //Queens
            insertNewPiece(0, 4, new Queen(board, Color.Black));
            insertNewPiece(7, 4, new Queen(board, Color.White));
            for(int i = 0; i < 8; i++)
            {
                insertNewPiece(1, i, new Pawn(board,Color.Black,this));
                insertNewPiece(6, i, new Pawn(board, Color.White, this));
            }
        }

    }
}
