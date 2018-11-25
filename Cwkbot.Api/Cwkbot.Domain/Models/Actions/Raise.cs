using Cwkbot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models.Actions
{
    public class Raise : IPokerAction
    {
        private const string RAISE = "raise";
        public Raise()
        {
            Action = RAISE;
        }
        public string Action { get; }
        public int Chips { get; set; }
    }
}
