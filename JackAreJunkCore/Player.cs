using System;
using System.Collections.Generic;

namespace JackAreJunk
{
    public class Player
    {
        public string Name { get; set; }
        public int CardsPerDeal { get; set; }
        public List<ICard> Cards { get; set; } = new List<ICard>();

        public Player()
        {
        }
        
        public bool IsAllCardsSolved()
        {
            return false; //todo 
        }
    }
}
