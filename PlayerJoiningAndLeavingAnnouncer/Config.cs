using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using Telepathy;
using Utils.Networking;
using static PlayerJoiningAndLeavingAnnouncer.Handlers.CustomStatement;

namespace PlayerJoiningAnnouncer
{    
    public class Config : IConfig
    {        
        [Description("Is the plugin Enabled?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Should the players name appear in the color of their rank badge?")]
        public bool IsRankColoringEnabled { get; set; } = true;
        [Description("What color should the names of players with no ran be?")]
        public string NoRankColor { get; set; } = "yellow";
        [Description("What should the plugin broadcast when a player joins the server?")]
        public string JoinMessage { get; set; } = "{playername} joined the server!";
        [Description("How long should the plugin broadcast the message when a player joins the server? (in seconds)")]
        public int JoinMessageDuration { get; set; } = 3;
        [Description("What should the plugin broadcast when a player leaves the server?")]
        public string LeftMessage { get; set; } = "{playername} left the server!";
        [Description("How long should the plugin broadcast the message when a player leaves the server? (in seconds)")]
        public int LeftMessageDuration { get; set; } = 3;
        [Description("Should only ranked players be diplayed?")]
        public bool OnlyRankedAreDisplayed { get; set; } = false;
        [Description("Should only players with a specific steam id be displayed and how? Available modes: 0 - off , 1 - the players given should be displayed with a costume message and so do the others with the default, 2 - only the players given should be displayed only with the message given")]        
        public int HowSpecificPlayersAreDisplayed { get; set; } = 0;
        [Description("Give the steam id of the players. And the costume messages which will be displayed when they join/leave.")]        
        public Dictionary<string, List<string>> SpecificPlayers { get; set; } = new Dictionary<string, List<string>>() { ["SomeSteamID@steam"] = new List<string> { "JoinMessage: {playername} joined the server!", "LeftMessage: {playername} left the server!" } };
    }
}