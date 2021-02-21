using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using MEC;

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
                string ColoredName;
                switch (OnlySpecificPlayersBeDisplayedMode)
                {
                    case 0:
                        if (IsColoredRanksEnabled == true)
                        {
                            if (OnlyRankedPlayersAreDisplayed == false)
                            {
                                ColoredName = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                Map.Broadcast(JoinedMessageDuration, JoinedMessage.Replace("{playername}", ColoredName));
                            }
                            else
                            {
                                if (ev.Player.RankColor != "default")
                                {
                                    ColoredName = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                    Map.Broadcast(JoinedMessageDuration, JoinedMessage.Replace("{playername}", ColoredName));
                                }
                            }
                        }
                        else
                        {
                            if (OnlyRankedPlayersAreDisplayed == false)
                            {
                                Map.Broadcast(JoinedMessageDuration, JoinedMessage.Replace("{playername}", ev.Player.Nickname));
                            }
                            else
                            {
                                if (ev.Player.RankColor != "default")
                                {
                                    Map.Broadcast(JoinedMessageDuration, JoinedMessage.Replace("{playername}", ev.Player.Nickname));
                                }
                            }
                        }
                        break;

                    case 1:
                        int iteration = 0;
                        foreach (KeyValuePair<string, List<string>> entry in Plugin.Singleton.Config.SpecificPlayers)
                        {
                            iteration++;
                            if (ev.Player.UserId == entry.Key)
                            {
                                if (IsColoredRanksEnabled == true)
                                {
                                    string playername = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                    Map.Broadcast(JoinedMessageDuration, entry.Value[0].Remove(0, 13).Replace("{playername}", playername));
                                    break;
                                }
                                else
                                {
                                    Map.Broadcast(JoinedMessageDuration, entry.Value[0].Remove(1, 13).Replace("{playername}", ev.Player.Nickname));
                                    break;
                                }
                            }
                            else
                            {
                                if (iteration == Plugin.Singleton.Config.SpecificPlayers.Count)
                                {
                                    iteration = 0;
                                    goto case 0;
                                }
                            }
                        }
                        break;

                    case 2:
                        foreach (KeyValuePair<string, List<string>> entry in Plugin.Singleton.Config.SpecificPlayers)
                        {
                            if (ev.Player.UserId == entry.Key)
                            {
                                if (IsColoredRanksEnabled == true)
                                {
                                    string playername = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                    Map.Broadcast(JoinedMessageDuration, entry.Value[0].Remove(0, 13).Replace("{playername}", playername));
                                }
                                else
                                {
                                    Map.Broadcast(JoinedMessageDuration, entry.Value[0].Remove(1, 13).Replace("{playername}", ev.Player.Nickname));
                                }
                            }
                        }
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
                string ColoredName;
                switch (OnlySpecificPlayersBeDisplayedMode)
                {
                    case 0:
                        if (IsColoredRanksEnabled == true)
                        {
                            if (OnlyRankedPlayersAreDisplayed == false)
                            {
                                ColoredName = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                Map.Broadcast(LeftMessageDuration, LeftMessage.Replace("{playername}", ColoredName));
                            }
                            else
                            {
                                if (ev.Player.RankColor != "default")
                                {
                                    ColoredName = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                    Map.Broadcast(LeftMessageDuration, LeftMessage.Replace("{playername}", ColoredName));
                                }
                            }
                        }
                        else
                        {
                            if (OnlyRankedPlayersAreDisplayed == false)
                            {
                                Map.Broadcast(LeftMessageDuration, LeftMessage.Replace("{playername}", ev.Player.Nickname));
                            }
                            else
                            {
                                if (ev.Player.RankColor != "default")
                                {
                                    Map.Broadcast(LeftMessageDuration, LeftMessage.Replace("{playername}", ev.Player.Nickname));
                                }
                            }
                        }
                        break;

                    case 1:
                        int iteration = 0;
                        foreach (KeyValuePair<string, List<string>> entry in Plugin.Singleton.Config.SpecificPlayers)
                        {
                            iteration++;
                            if (ev.Player.UserId == entry.Key)
                            {
                                if (IsColoredRanksEnabled == true)
                                {
                                    string playername = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                    Map.Broadcast(LeftMessageDuration, entry.Value[1].Remove(0, 13).Replace("{playername}", playername));
                                    break;
                                }
                                else
                                {
                                    Map.Broadcast(LeftMessageDuration, entry.Value[1].Remove(1, 13).Replace("{playername}", ev.Player.Nickname));
                                    break;
                                }
                            }
                            else
                            {
                                if (iteration == Plugin.Singleton.Config.SpecificPlayers.Count)
                                {
                                    iteration = 0;
                                    goto case 0;
                                }
                            }
                        }
                        break;

                    case 2:
                        foreach (KeyValuePair<string, List<string>> entry in Plugin.Singleton.Config.SpecificPlayers)
                        {
                            if (ev.Player.UserId == entry.Key)
                            {
                                if (IsColoredRanksEnabled == true)
                                {
                                    string playername = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                    Map.Broadcast(LeftMessageDuration, entry.Value[1].Remove(0, 13).Replace("{playername}", playername));
                                }
                                else
                                {
                                    Map.Broadcast(LeftMessageDuration, entry.Value[1].Remove(1, 13).Replace("{playername}", ev.Player.Nickname));
                                }
                            }
                        }
                        break;

                    default:
                        Log.Warn("The OnlySpecificPlayersBeDisplayedMode config variable can only be 0 - The specific players in the list are not displayed differently, 1 - the players given should be displayed with a costume mesage and so do the others with the default, 2 - only the players given should be displayed only with the message given.");
                        break;
                }
        }            
    }
}