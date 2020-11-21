using Exiled.API.Interfaces;
using System.ComponentModel;

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
    }
}
