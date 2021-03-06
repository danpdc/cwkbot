﻿using Cwkbot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models.Actions
{
    public class Bet : IPokerBetAction
    {
        private const string BET = "bet";
        public Bet()
        {
            Action = BET;
        }
        public string Action { get; private set; }
        public int Chips { get; set; }
    }
}
