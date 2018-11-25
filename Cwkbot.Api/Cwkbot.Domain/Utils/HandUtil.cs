using Cwkbot.Domain.Interfaces;
using Cwkbot.Domain.Models;
using Cwkbot.Domain.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cwkbot.Domain.Utils
{
    public static class HandUtil
    {
        public static Tuple<bool, int> IsPair(List<Card> cards)
        {

            var hasPair = cards.GroupBy(card => card.Rank)
                               .Count(group => group.Count() == 2) == 1;
            int sum = 0;
            if (hasPair)
            {
                var paired = cards.GroupBy(card => card.Rank);
                foreach (var key in paired)
                {
                    var keyArray = key.ToArray();
                    if (keyArray.Length == 2)
                        sum = (int)keyArray[0].Rank + (int)keyArray[1].Rank;
                }
            }
            return new Tuple<bool, int>(hasPair, sum);
        }

        public static Tuple<bool, int> IsTwoPairs(List<Card> cards)
        {
            var hasTwoPairs = cards.GroupBy(card => card.Rank)
                                   .Count(group => group.Count() >= 2) == 2;
            var pairs = cards.GroupBy(card => card.Rank);
            int sum = 0;
            foreach (var card in cards)
            {
                sum += (int)card.Rank;
            }
            return new Tuple<bool, int>(hasTwoPairs, sum);
        }
        public static List<IPokerAction> GetPokerActions(HandInfo hand)
        {
            List<IPokerAction> actions = new List<IPokerAction>();
            actions.Add(new Fold());
            var isCheckPossible = IsCheckPossible(hand.Players);
            if (isCheckPossible)
            {
                actions.Add(new Check());
                actions.Add(new Bet());
            }
            else
            {
                actions.Add(new Call());
                actions.Add(new Raise());
            }


            return actions;
        }

        public static int CalculateBetMargin(HandInfo hand, int percent)
        {
            var myPlayer = hand.Players.Find(p => p.Username == "cwkbot");
            var playingPlayers = hand.Players.Where(p => p.HasFolded == false);
            int neededForCall = 0;
            foreach (var player in playingPlayers)
            {
                if (player.PotContribution > myPlayer.PotContribution)
                    neededForCall = player.PotContribution - myPlayer.PotContribution;
            }
            if (neededForCall == 0)
                return (myPlayer.Chips * percent) / 100;
            else
                return neededForCall + (myPlayer.Chips * 5 / 100);
        }

        private static bool IsCheckPossible(List<Player> players)
        {
            int pot = players[0].PotContribution;
            var playingPlayers = players.Where(p => p.HasFolded == false);
            foreach (var player in playingPlayers)
            {
                if (pot != player.PotContribution)
                    return false;
            }
            return true;
        }
    }
}
