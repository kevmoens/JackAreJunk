using System;
namespace JackAreJunk
{
    public interface ICardPlayable : ICard
    {
        bool IsShown { get; set; }
    }
}
