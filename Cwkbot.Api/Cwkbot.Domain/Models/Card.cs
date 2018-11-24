using Cwkbot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models
{
    public class Card
    {
        public CardRank Rank { get; set; }
        public CardSuit Suit { get; set; }

        public override string ToString()
        {
            return $"Rank: {Rank.ToString()} Suit: {Suit.ToString()}";
        }

        public bool IsSameRank(Card card)
        {
            return Rank == card.Rank;
        }

        public bool IsSameSuit(Card card)
        {
            return Suit == card.Suit;
        }
    }
}
