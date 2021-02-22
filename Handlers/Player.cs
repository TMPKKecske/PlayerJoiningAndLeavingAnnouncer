using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using MEC;
using System.Security.Policy;

namespace PlayerJoiningAnnouncer.Handlers
{
    public class Player
    {

        Dictionary<string, string> RestrictedColors = new Dictionary<string, string>()
        {
            ["pink"] = "#FF96DE",
            ["light_green"] = "#32CD32",
            ["crimson"] = "#DC143C",
            ["deep_pink"] = "#FF1493",
            ["tomato"] = "#FF6448",
            ["blue_green"] = "#4DFFB8",
            ["lime"] = "#BFFF00",
            ["emerald"] = "#50C878",
            ["carmine"] = "#960018",
            ["nickel"] = "#727472",
            ["mint"] = "#98FB9",
            ["army_green"] = "#4B5320",
            ["pumpkin"] = "#EE7600",
            ["gold"] = "#EFC01A",
            ["light_red"] = "#FD8272",
            ["silver_blue"] = "#666699",
            ["police_blue"] = "#002DB3",
            ["default"] = NoRankColor,
        };

        private string ColorFix(string color)
        {
            if (!RestrictedColors.ContainsKey(color) && color != "default")
            {                
                return color;
            }
            else
            {                
                return RestrictedColors[color];
            }
        }

        bool OnlyRankedPlayersAreDisplayed = Plugin.Singleton.Config.OnlyRankedAreDisplayed;
        bool IsColoredRanksEnabled = Plugin.Singleton.Config.IsRankColoringEnabled;
        UInt16 JoinedMessageDuration = Plugin.Singleton.Config.JoinMessageDuration;
        string JoinedMessage = Plugin.Singleton.Config.JoinMessage;
        static string NoRankColor = Plugin.Singleton.Config.NoRankColor;
        int OnlySpecificPlayersBeDisplayedMode = Plugin.Singleton.Config.HowSpecificPlayersAreDisplayed;

        public void OnVertified(VerifiedEventArgs ev)
        {
            Timing.CallDelayed(0.3f, () => 
            {
                Map.ClearBroadcasts();               
                switch (OnlySpecificPlayersBeDisplayedMode)
                {
                    case 0:
                        OnlySpecificPlayersBeDisplayedMode0(true, ev.Player);
                    break;

                    case 1:
                        OnlySpecificPlayersBeDisplayedMode1(true, ev.Player);
                    break;

                    case 2:
                        OnlySpecificPlayersBeDisplayedMode2(true, ev.Player);
                    break;

                    default:
                        Log.Warn("The OnlySpecificPlayersBeDisplayedMode config variable can only be 0 - off, 1 - the players given should be displayed with a costume mesage and so do the others with the default, 2 - only the players given should be displayed only with the message given.");
                    break;
                }
            });
        }

        UInt16 LeftMessageDuration = Plugin.Singleton.Config.LeftMessageDuration;
        string LeftMessage = Plugin.Singleton.Config.LeftMessage;        
        public void OnDerstroy(DestroyingEventArgs ev)
        {
                Map.ClearBroadcasts();                
                switch (OnlySpecificPlayersBeDisplayedMode)
                {
                    case 0:
                        OnlySpecificPlayersBeDisplayedMode0(false, ev.Player);
                    break;
                    
                    case 1:
                        OnlySpecificPlayersBeDisplayedMode1(false, ev.Player);
                    break;

                    case 2:
                        OnlySpecificPlayersBeDisplayedMode2(false, ev.Player);
                    break;

                    default:
                        Log.Warn("The OnlySpecificPlayersBeDisplayedMode config variable can only be 0 - The specific players in the list are not displayed differently, 1 - the players given should be displayed with a costume mesage and so do the others with the default, 2 - only the players given should be displayed only with the message given.");
                    break;
                }
        }

        void OnlySpecificPlayersBeDisplayedMode0(bool IsJoined, Exiled.API.Features.Player ply)
        {
            string ColoredName;
            if (IsColoredRanksEnabled == true)
            {
                if (OnlyRankedPlayersAreDisplayed == false)
                {
                    ColoredName = $"<color={ ColorFix(ply.RankColor) }>{ply.Nickname}</color>";
                    if (IsJoined)
                    {
                        Map.Broadcast(JoinedMessageDuration, JoinedMessage.Replace("{playername}", ColoredName));
                    }
                    else
                    {
                        Map.Broadcast(LeftMessageDuration, LeftMessage.Replace("{playername}", ColoredName));
                    }

                }
                else
                {
                    if (ply.RankColor != "default")
                    {
                        ColoredName = $"<color={ ColorFix(ply.RankColor) }>{ply.Nickname}</color>";
                        if (IsJoined)
                        {
                            Map.Broadcast(JoinedMessageDuration, JoinedMessage.Replace("{playername}", ColoredName));
                        }
                        else
                        {
                            Map.Broadcast(LeftMessageDuration, LeftMessage.Replace("{playername}", ColoredName));
                        }
                    }
                }
            }
            else
            {
                if (OnlyRankedPlayersAreDisplayed == false)
                {
                    if (IsJoined)
                    {
                        Map.Broadcast(JoinedMessageDuration, JoinedMessage.Replace("{playername}", ply.Nickname));
                    }
                    else
                    {
                        Map.Broadcast(LeftMessageDuration, LeftMessage.Replace("{playername}", ply.Nickname));
                    }
                }
                else
                {
                    if (ply.RankColor != "default")
                    {
                        if (IsJoined)
                        {
                            Map.Broadcast(JoinedMessageDuration, JoinedMessage.Replace("{playername}", ply.Nickname));
                        }
                        else 
                        {
                            Map.Broadcast(LeftMessageDuration, LeftMessage.Replace("{playername}", ply.Nickname));
                        }
                    }
                }
            }
        }

        void OnlySpecificPlayersBeDisplayedMode1(bool IsJoined, Exiled.API.Features.Player ply)
        {
            if (Plugin.Singleton.Config.SpecificPlayers.ContainsKey(ply.UserId))
            {
                List<String> Messages = Plugin.Singleton.Config.SpecificPlayers[ply.UserId];

                if (IsColoredRanksEnabled == true)
                {
                    string playername = $"<color={ ColorFix(ply.RankColor) }>{ply.Nickname}</color>";
                    if (IsJoined)
                    {
                        Map.Broadcast(LeftMessageDuration, Messages[0].Remove(0, 13).Replace("{playername}", playername));
                    }
                    else
                    {
                        Map.Broadcast(LeftMessageDuration, Messages[1].Remove(0, 13).Replace("{playername}", playername));
                    }
                }
            }
            else 
            {
                OnlySpecificPlayersBeDisplayedMode0(IsJoined, ply);
            }
        }

        void OnlySpecificPlayersBeDisplayedMode2(bool IsJoined, Exiled.API.Features.Player ply)
        {
            if (Plugin.Singleton.Config.SpecificPlayers.ContainsKey(ply.UserId)) 
            {
                List<String> Messages = Plugin.Singleton.Config.SpecificPlayers[ply.UserId];

                if (IsColoredRanksEnabled == true)
                {
                    string playername = $"<color={ ColorFix(ply.RankColor) }>{ply.Nickname}</color>";
                    if (IsJoined)
                    {
                        Map.Broadcast(LeftMessageDuration, Messages[0].Remove(0, 13).Replace("{playername}", playername));
                    }
                    else 
                    {
                        Map.Broadcast(LeftMessageDuration, Messages[1].Remove(0, 13).Replace("{playername}", playername));
                    }                   
                }
                else
                {
                    if (IsJoined)
                    {
                        Map.Broadcast(LeftMessageDuration, Messages[0].Remove(1, 13).Replace("{playername}", ply.Nickname));
                    }
                    else 
                    {
                        Map.Broadcast(LeftMessageDuration, Messages[1].Remove(1, 13).Replace("{playername}", ply.Nickname));
                    }
                }
            }
        }
    }
}