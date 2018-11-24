using Cwkbot.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models.Actions
{
    public class Fold : IPokerAction
    {
        private const string FOLD = "fold";
        public Fold()
        {
            Action = FOLD;
        }
        public string Action { get; set; }
    }
}
