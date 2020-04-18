using board;
using chess;
using System;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch cm = new ChessMatch();

                while (!cm.over)
                {
                    Console.Clear();
                    Screen.printMatch(cm);
                    Console.Write("\nOrigin:");
                    Position origin = Screen.readChessPos().toPosition();
                    try
                    {
                        cm.validatePlay(origin);

                        bool[,] vp = cm.board.piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printBoard(cm.board, vp);
                        Console.Write("\nDestiny:");
                        Position dest = Screen.readChessPos().toPosition();
                        cm.validatePlay(origin, dest);

                        cm.doPlay(origin, dest);
                    }
                    catch(BoardException e)
                    {
                        Console.WriteLine("ERROR! "+e.Message);
                        Console.ReadLine();
                    }
                }

            }
            catch (BoardException e)
            {
                Console.WriteLine("Error! "+e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! " + e.Message);
            }
        }
    }
}
