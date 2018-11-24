using Cwkbot.Domain.Enums;
using Cwkbot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models
{
    public class HandEvaluation
    {
        public Position Position { get; set; }
        public int PotSize { get; set; }
        public double Equity { get; set; }
        public IPokerAction SuggestedAction { get; set; }
    }
}
