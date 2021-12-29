using System;
namespace JackAreJunk
{
    public interface ICard
    {
        Suit Suit { get; }
        Rank Rank { get;  }
        bool IsJoker { get; }
        bool IsRed { get; }
        string ToString();
    }
}