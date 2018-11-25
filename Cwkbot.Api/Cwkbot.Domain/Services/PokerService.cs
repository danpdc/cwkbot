using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Cwkbot.Domain.Models.Actions;
using Cwkbot.Domain.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Services
{
    public class PokerService : IPokerService
    {
        public HandEvaluation GetHandEvaluation(HandInfo currentHand)
        {
            HandEvaluation he = new HandEvaluation();
            var strategyFactory = new PokerStrategyFactory(currentHand);
            var strategy = strategyFactory.GetStrategy();
            var suggestedAction = strategy.Evaluate();
            if (suggestedAction == null)
                he.SuggestedAction = new Fold();
            else
                he.SuggestedAction = suggestedAction;
            return he;
        }
    }
}
