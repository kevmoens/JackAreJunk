using System;
namespace JackAreJunk
{
    public class CardPlayable : CardDecorator, ICardPlayable
    {
        public CardPlayable(ICard card) : base(card)
        {
        }

        public bool IsShown { get; set; }
    }
}
