using Cwkbot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models
{
    public class HandInfo
    {

        public HandInfo(int smallBlind, int bigBlind, List<Card> playerCards,
            List<Card> tableCards, List<Player> players)
        {
            SmallBlind = smallBlind;
            BigBlind = bigBlind;
            PlayerCards = playerCards;
            TableCards = tableCards;
            Players = players;
        }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public List<Card> PlayerCards { get; set; }
        public List<Card> TableCards { get; set; }
        public List<Player> Players { get; set; }
        public HandStage Stage => GetHandStage();
        public int PotSize => GetPotSize();

        public HandStage GetHandStage()
        {
            if (TableCards.Count == 0)
                return HandStage.Preflop;
            else if (TableCards.Count == 3)
                return HandStage.Flop;
            else if (TableCards.Count == 4)
                return HandStage.Turn;
            else
                return HandStage.River;
        }

        public int GetPotSize()
        {
            int pot = 0;
            foreach (var player in Players)
            {
                pot += player.PotContribution;
            }
            return pot;
        }
    }
}
