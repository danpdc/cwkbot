using Cwkbot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models.Actions
{
    public class Bet : IPokerBetAction
    {
        public string Action { get; set; }
        public int Chips { get; set; }
    }
}
