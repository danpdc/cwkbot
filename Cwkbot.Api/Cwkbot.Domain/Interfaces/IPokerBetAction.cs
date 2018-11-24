using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Interfaces
{
    public interface IPokerBetAction : IPokerAction
    {
        int Chips { get; set; }
    }
}
