using Exiled.API.Features;
using Exiled.Events.EventArgs;
using PlayerJoiningAndLeavingAnnouncer.Handlers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PlayerJoiningAnnouncer.Handlers
{
    class Player
    {
        bool IsColoredRanksEnabled = Plugin.Instance.Config.IsRankColoringEnabled;
        int JoinedMessageDuration = Plugin.Instance.Config.JoinMessageDuration;
        string JoinedMessage = Plugin.Instance.Config.JoinMessage;
        string NoRankColor = Plugin.Instance.Config.NoRankColor;
        public void OnJoin(JoinedEventArgs ev)
        {
            if (IsColoredRanksEnabled == true)
            {
                Map.ClearBroadcasts();                
                Log.Info("Some other thing");
                string ColoredPlayerName;
                var NotWorkingColors = new Dictionary<string, string>();
                NotWorkingColors.Add("pink", "#FF96DE");
                NotWorkingColors.Add("light_green", "#32CD32");
                NotWorkingColors.Add("crimson", "#DC143C");
                NotWorkingColors.Add("deep_pink", "#FF1493");
                NotWorkingColors.Add("tomato", "#FF6448");
                NotWorkingColors.Add("blue_green", "#4DFFB8");
                NotWorkingColors.Add("lime", "#BFFF00");
                NotWorkingColors.Add("emerald", "#50C878");
                NotWorkingColors.Add("carmine", "#960018");
                NotWorkingColors.Add("nickel", "#727472");
                NotWorkingColors.Add("mint", "#98FB9");
                NotWorkingColors.Add("army_green", "#4B5320");
                NotWorkingColors.Add("pumpkin", "#EE7600");
                NotWorkingColors.Add("gold", "#EFC01A");
                NotWorkingColors.Add("light_red", "#FD8272");
                NotWorkingColors.Add("silver_blue", "#666699");
                NotWorkingColors.Add("police_blue", "#002DB3");

                if (ev.Player.RankColor.IsWorkingColor() && ev.Player.RankColor != "default")
                {
                    ColoredPlayerName = $"<color={ev.Player.RankColor}>{ev.Player.Nickname}</color>";
                }

                else if (ev.Player.RankColor == "default")
                {                    
                    if (NoRankColor.IsWorkingColor())
                    {
                        ColoredPlayerName = $"<color={NoRankColor}>{ev.Player.Nickname}</color>";
                    }
                    else
                    {
                        ColoredPlayerName = $"<color={NotWorkingColors[NoRankColor]}>{ev.Player.Nickname}</color>";
                    }
                }

                else
                {
                    ColoredPlayerName = $"<color={NotWorkingColors[ev.Player.RankColor]}>{ev.Player.Nickname}</color>";
                }

                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), JoinedMessage.Replace("{playername}", ColoredPlayerName));
            }
            else
            {
                Map.ClearBroadcasts();
                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), JoinedMessage.Replace("{playername}", ev.Player.Nickname));
            }
        }

        int LeftMessageDuration = Plugin.Instance.Config.LeftMessageDuration;
        string LeftMessage = Plugin.Instance.Config.LeftMessage;
        public void OnLeft(LeftEventArgs ev)
        {
            if (IsColoredRanksEnabled == true)
            {
                Map.ClearBroadcasts();
                Log.Info(ev.Player.RankColor);
                string ColoredPlayerName = $"<color={ev.Player.RankColor}>{ev.Player.Nickname}</color>";
                Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), LeftMessage.Replace("{playername}", ColoredPlayerName));
            }
            else
            {
                Map.ClearBroadcasts();
                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), JoinedMessage.Replace("{playername}", ev.Player.Nickname));
            }
        }
    }
}
