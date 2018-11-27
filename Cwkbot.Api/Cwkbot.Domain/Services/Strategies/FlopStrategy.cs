using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Cwkbot.Domain.Models.Actions;
using Cwkbot.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Services.Strategies
{
    public class FlopStrategy : IPokerStrategy
    {
        private HandInfo _handInfo;
        public FlopStrategy(HandInfo handInfo)
        {
            _handInfo = handInfo;
        }
        public IPokerAction Evaluate()
        {
            var actions = HandUtil.GetPokerActions(_handInfo);
            var myPlayer = _handInfo.Players.Find(p => p.Username == "cwkbot");
            var raiseAvailable= actions.Find(a => a.Action == "raise");
            var betAvailable = actions.Find(a => a.Action == "bet");
            var callAvailable = actions.Find(a => a.Action == "call");
            var checkAvailable = actions.Find(a => a.Action == "check");
            Raise raise = new Raise();
            Bet bet = new Bet();
            Call call = new Call();
            Check check = new Check();
            List<Card> allCards = new List<Card>();
            foreach (var card in _handInfo.YourCards)
                allCards.Add(card);
            foreach (var card in _handInfo.TableCards)
                allCards.Add(card);
            var isTwoPairs = HandUtil.IsTwoPairs(allCards);
            if (isTwoPairs.Item1)
            {
                if (raiseAvailable != null)
                {
                    raise.Chips = HandUtil.CalculateBetMargin(_handInfo, 15);
                    if (raise.Chips > myPlayer.Chips)
                        raise.Chips = myPlayer.Chips / 6;
                }
                else if (betAvailable != null)
                {
                    bet.Chips = HandUtil.CalculateBetMargin(_handInfo, 15);
                    if (bet.Chips > myPlayer.Chips)
                        bet.Chips = myPlayer.Chips / 6;
                }               
            }
            var isPair = HandUtil.IsPair(allCards);
            var isThreeOfAKind = HandUtil.IsThreeOfAKind(allCards);
            if (isThreeOfAKind.Item1)
            {
                if (raiseAvailable != null)
                {
                    if (isThreeOfAKind.Item2 >= 36)
                        raise.Chips = myPlayer.Chips / 8;
                    else
                    {
                        raise.Chips = HandUtil.CalculateBetMargin(_handInfo, 40);
                        if (raise.Chips > myPlayer.Chips)
                            raise.Chips = myPlayer.Chips / 8;
                    }
                }

                if (betAvailable != null)
                {
                    if (isThreeOfAKind.Item2 >= 36)
                        bet.Chips = myPlayer.Chips / 6;
                    else
                    {
                        bet.Chips = HandUtil.CalculateBetMargin(_handInfo, 40);
                        if (bet.Chips > myPlayer.Chips)
                            bet.Chips = myPlayer.Chips / 6;
                    }
                }
            }

            var isStraight = HandUtil.IsStraight(allCards);
            if (isStraight)
            {
                if (raiseAvailable != null)
                {
                    raise.Chips = myPlayer.Chips / 6;
                }
                if (betAvailable != null)
                {
                    bet.Chips = myPlayer.Chips / 6;
                }
            }

            var isFlush = HandUtil.IsFlush(allCards);
            if (isFlush.Item1)
            {
                if (isFlush.Item2 > 10)
                {
                    if (raiseAvailable != null)
                        raise.Chips = myPlayer.Chips / 6;
                    if (betAvailable != null)
                        bet.Chips = myPlayer.Chips / 6;
                }
                else if (isFlush.Item2 <= 10)
                {
                    if (raiseAvailable != null)
                        raise.Chips = HandUtil.CalculateBetMargin(_handInfo, 40);
                    if (betAvailable != null)
                        bet.Chips = HandUtil.CalculateBetMargin(_handInfo, 40);
                }
                    
            }

            var isFourOfAKind = HandUtil.IsFourOfAKind(allCards);
            if (isFourOfAKind)
            {
                if (raiseAvailable != null)
                    raise.Chips = myPlayer.Chips;
                if (betAvailable != null)
                    bet.Chips = myPlayer.Chips;
            }
            var isFullHouse = HandUtil.IsFullHouse(allCards);
            if (isFullHouse)
            {
                if (raiseAvailable != null)
                    raise.Chips = myPlayer.Chips / 6;
                if (betAvailable != null)
                    bet.Chips = myPlayer.Chips / 6;
            }

            if (raise.Chips > 0)
                return raise;
            else if (bet.Chips > 0)
                return bet;
            else if (callAvailable != null && isPair.Item2 > 17)
                return call;
            else if (checkAvailable != null)
                return check;
            else
                return new Fold();
        }
    }
}
