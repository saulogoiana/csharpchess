
using System;

namespace board
{
    class BoardException : ApplicationException
    {
        public BoardException(string e) : base(e)
        {
        }
    }
}
