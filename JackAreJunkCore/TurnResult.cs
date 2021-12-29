using System;
namespace JackAreJunk
{
    public class TurnResult
    {
        public Player Player { get; set; }
        public bool IsWinner { get; set; }
        public ICard ReturnCard { get; set; }
        public TurnResult()
        {
        }

    }
}
