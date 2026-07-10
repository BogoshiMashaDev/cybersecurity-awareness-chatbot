using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBotPart2
{
    public class UserMemory
    {
        public string UserName { get; set; } = "";
        public string FavouriteTopic { get; set; } = "";
        public string LastTopic { get; set; } = "";
        public string LastSentiment { get; set; } = "";
    }
}