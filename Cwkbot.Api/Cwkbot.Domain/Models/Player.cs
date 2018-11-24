using System;
using System.Collections.Generic;
using System.Text;

namespace Cwkbot.Domain.Models
{
    public class Player
    {
        public string Username { get; set; }
        public int Chips { get; set; }
        public int PotContribution { get; set; }
        public bool IsAllIn { get; set; }
        public bool HasFolded { get; set; }
    }
}
