using System;
using System.Collections.Generic;

namespace JackAreJunk
{
    public class Deck
    {
        public List<Card> Cards { get; set; } = new List<Card>();
        public Deck()
        {
            Shuffle();
        }
        public void Shuffle()
        {
            List<Card> AllCards = LoadAllCards();
            var rand = new Random();
            for (int i = AllCards.Count - 1; i >= 0; i--)
            {
                int cardPos = rand.Next(0, i + 1);
                Cards.Add(AllCards[cardPos]);
                AllCards.RemoveAt(cardPos);
            }
        }

        private static List<Card> LoadAllCards()
        {
            var AllCards = new List<Card>();
            var suits = (Suit[])(Enum.GetValues(typeof(Suit)));
            var ranks = (Rank[])(Enum.GetValues(typeof(Rank)));
            for (int r = 1; r < 14; r++)
            {
                for (int s = 0; s < 4; s++)
                {
                    var suit = suits[s];
                    var rank = ranks[r];
                    AllCards.Add(new Card(suit, rank));
                }
            }
            AllCards.Add(new Card(true, false));
            AllCards.Add(new Card(true, true));
            return AllCards;
        }
    }

}
