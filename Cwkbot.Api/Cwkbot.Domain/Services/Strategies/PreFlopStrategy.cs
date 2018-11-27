using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Cwkbot.Domain.Models.Actions;
using Cwkbot.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cwkbot.Domain.Services.Strategies
{
    public class PreFlopStrategy : IPokerStrategy
    {
        private HandInfo _handInfo;
        public PreFlopStrategy(HandInfo handInfo)
        {
            _handInfo = handInfo;
        }
        public IPokerAction Evaluate()
        {
            var actions = HandUtil.GetPokerActions(_handInfo);
            var myPlayer = _handInfo.Players.Find(p => p.Username == "cwkbot");
            var sum = (int)_handInfo.YourCards[0].Rank + (int)_handInfo.YourCards[1].Rank;
            var isPair = _handInfo.YourCards[0].Rank == _handInfo.YourCards[1].Rank;
            var isSameSuit = _handInfo.YourCards[0].Suit == _handInfo.YourCards[1].Suit;
            if (isSameSuit && sum > 21 )
            {
                var betAction = actions.Find( a => a.Action == "bet");
                if (betAction != null)
                {
                    Bet bet = new Bet();
                    var betValue = HandUtil.CalculateBetMargin(_handInfo, 10);
                    if (betValue > myPlayer.Chips)
                        bet.Chips = myPlayer.Chips;
                    return bet;
                }
                var raiseAction = actions.Find(a => a.Action == "raise");
                if (raiseAction != null)
                {
                    Raise raise = new Raise();
                    var betValue = HandUtil.CalculateBetMargin(_handInfo, 10);
                }     
            }

            else if(isPair && sum > 23)
            {
                var betAction = actions.Find(a => a.Action == "bet");
                if (betAction != null)
                {
                    Bet bet = new Bet();
                    var betValue = HandUtil.CalculateBetMargin(_handInfo, 10);
                    if (betValue > myPlayer.Chips)
                        bet.Chips = myPlayer.Chips;
                    return bet;
                }
                var raiseAction = actions.Find(a => a.Action == "raise");
                if (raiseAction != null)
                {
                    Raise raise = new Raise();
                    var betValue = HandUtil.CalculateBetMargin(_handInfo, 10);
                }
            }
            else
            {
                if (sum > 17)
                {
                    var callAction = actions.Find(a => a.Action == "call");
                    return new Call();
                }
                else
                    return new Fold();
            }
            return new Fold();
        } 
    }
}
