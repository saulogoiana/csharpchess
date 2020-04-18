using System;
using System.Collections.Generic;
using System.Text;
using board;
using chess;

namespace XadrezConsole
{
    class Screen
    {
        public static void printMatch(ChessMatch chessMatch)
        {
            printBoard(chessMatch.board);
            printCapturedPieces(chessMatch);
            Console.WriteLine("\nRound: "+chessMatch.rounds);
            if (!chessMatch.over)
            {
                Console.Write("\nActual Player: " + chessMatch.actPlayer);
                if (chessMatch.check)
                {
                    Console.WriteLine("\nCHECK!!");
                }
            }
            else
            {
                Console.WriteLine("Check Mate!!");
                Console.WriteLine("Winner: "+chessMatch.actPlayer);
            }
        }

        public static void printCapturedPieces(ChessMatch chessMatch)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("White: ");
            printHashset(chessMatch.capturedPieces(Color.White));
            Console.Write("\nBlack: ");
            printHashset(chessMatch.capturedPieces(Color.Black));
        }

        public static void printHashset(HashSet<Piece> pieces)
        {
            Console.Write("[ ");
            foreach(Piece p in pieces)
            {
                Console.Write(p+"; ");
            }
            Console.Write(" ]");
        }

        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    printPiece(board.piece(i,j));
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  a b c d e f g h ");
        }

        public static void printBoard(Board board, bool[,] validPos)
        {
            ConsoleColor originalBack = Console.BackgroundColor;
            ConsoleColor alteredBack = ConsoleColor.Blue;

            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (validPos[i, j])
                    {
                        Console.BackgroundColor = alteredBack;
                    }
                    else
                        Console.BackgroundColor = originalBack;
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBack;
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  a b c d e f g h ");
            Console.BackgroundColor = originalBack;
        }

        public static void printPiece(Piece p)
        {
            if (p == null)
                Console.Write("- ");
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                if (p.color == Color.White)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(p);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(p);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
            
        }

        public static ChessPosition readChessPos()
        {
            string s = Console.ReadLine();
            char col = s[0];
            int lin = int.Parse(s[1]+"");

            return new ChessPosition(col, lin);
        }

    }
}
