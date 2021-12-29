namespace JackAreJunk
{
    public enum Suit
    {
        Diamonds
        ,Heart
        ,Spades
        ,Clubs
    }
    public enum Rank
    {
        None
        ,C01
        ,C02
        ,C03
        ,C04
        ,C05
        ,C06
        ,C07
        ,C08
        ,C09
        ,C10
        ,Jack
        ,Queen
        ,King
    }
    public class Card : ICard
    {
        public Suit Suit { get;}
        public Rank Rank { get; }
        public bool IsJoker { get; }
        public bool IsRed { get; }
        public Card(bool isJoker, bool isRed)
        {
            IsJoker = isJoker;
            IsRed = isRed;
        }
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            IsRed = Suit == Suit.Diamonds || Suit == Suit.Heart;
        }
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
