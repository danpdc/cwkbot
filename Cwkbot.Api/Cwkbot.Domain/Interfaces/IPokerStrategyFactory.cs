using Cwkbot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Interfaces
{
    public interface IPokerStrategyFactory
    {
        IPokerStrategy GetStrategy();
    }
}
