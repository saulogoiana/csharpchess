using board;
namespace chess
{
    class ChessPosition
    {
        public char column { get; set; }
        public int line { get; set; }

        public ChessPosition(char col, int lin)
        {
            this.column = col;
            this.line = lin;
        }

        public Position toPosition()
        {
            return new Position(8 - line, column - 'a');
        }

        public override string ToString()
        {
            return "" + column + line;
        }
    }
}
