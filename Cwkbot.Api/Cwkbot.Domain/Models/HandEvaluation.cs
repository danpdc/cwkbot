using Cwkbot.Domain.Enums;
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
    }
}
