using System;
namespace JackAreJunk
{
    public class CardDecorator : ICard
    {
        private ICard _card;
        public CardDecorator(ICard card)
        {
            _card = card;
        }

        public Suit Suit { get { return _card.Suit; } }
        public Rank Rank { get { return _card.Rank; } }
        public bool IsJoker { get { return _card.IsJoker; } }
        public bool IsRed { get { return _card.IsRed; }  }

        public override string ToString()
        {
            if (IsJoker)
            {
                return (IsRed ? "Red" : "Black") + "Joker";
            }
            return $"{Rank} {Suit}";
        }
    }
}
