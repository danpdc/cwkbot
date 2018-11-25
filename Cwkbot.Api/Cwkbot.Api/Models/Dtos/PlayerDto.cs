using Cwkbot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwkbot.Api.Models.Dtos
{
    public class PlayerDto
    {
        public string Username { get; set; }
        public int Chips { get; set; }
        public bool IsAllIn { get; set; }
        public bool HasFolded { get; set; }
        public int Pot { get; set; }

        public Player GetPlayerModel(PlayerDto player)
        {
            Player playerModel = new Player();
            playerModel.Chips = player.Chips;
            playerModel.HasFolded = player.HasFolded;
            playerModel.IsAllIn = player.IsAllIn;
            playerModel.Username = player.Username;
            playerModel.PotContribution = player.Pot;
            return playerModel;
        }
    }
}
