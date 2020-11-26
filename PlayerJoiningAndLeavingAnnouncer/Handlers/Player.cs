using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using PlayerJoiningAndLeavingAnnouncer.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using Telepathy;
using static PlayerJoiningAnnouncer.Config;

namespace PlayerJoiningAnnouncer.Handlers
{
    class Player
    {
        Dictionary<string, string> NotWorkingColors = new Dictionary<string, string>()
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
            if (color.IsWorkingColor() && color != "default")
            {
                return color;
            }
            else
            {
                return NotWorkingColors[color];
            }
        }

        bool OnlyRankedPlayersAreDisplayed = Plugin.Instance.Config.OnlyRankedAreDisplayed;
        bool IsColoredRanksEnabled = Plugin.Instance.Config.IsRankColoringEnabled;
        int JoinedMessageDuration = Plugin.Instance.Config.JoinMessageDuration;
        string JoinedMessage = Plugin.Instance.Config.JoinMessage;
        static string NoRankColor = Plugin.Instance.Config.NoRankColor;
        int OnlySpecificPlayersBeDisplayedMode = Plugin.Instance.Config.HowSpecificPlayersAreDisplayed;

        public void OnJoin(JoinedEventArgs ev)
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
                            Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), JoinedMessage.Replace("{playername}", ColoredName));
                        }
                        else
                        {
                            if (ev.Player.RankColor != "default")
                            {
                                ColoredName = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), JoinedMessage.Replace("{playername}", ColoredName));
                            }
                        }
                    }
                    else
                    {
                        if (OnlyRankedPlayersAreDisplayed == false)
                        {
                            Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), JoinedMessage.Replace("{playername}", ev.Player.Nickname));
                        }
                        else
                        {                            
                            if (ev.Player.RankColor != "default")
                            {
                                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), JoinedMessage.Replace("{playername}", ev.Player.Nickname));
                            }
                        }
                    }
                    break;

                case 1:
                    int iteration = 0;
                    foreach (KeyValuePair<string, List<string>> entry in Plugin.Instance.Config.SpecificPlayers)
                    {
                        iteration++;
                        if (ev.Player.UserId == entry.Key)
                        {
                            if (IsColoredRanksEnabled == true)
                            {
                                string playername = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), entry.Value[0].Remove(0, 13).Replace("{playername}", playername));                                
                            }
                            else
                            {
                                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), entry.Value[0].Remove(1, 13).Replace("{playername}", ev.Player.Nickname));
                            }
                        }
                        else
                        {
                            if (iteration == Plugin.Instance.Config.SpecificPlayers.Count)
                            {
                                iteration = 0;
                                Log.Info(ev.Player.UserId + " and " + entry.Key);
                                Log.Info(entry.Value[1]);
                                goto case 0;
                            }
                        }
                    }
                    break;

                case 2:
                    foreach (KeyValuePair<string, List<string>> entry in Plugin.Instance.Config.SpecificPlayers)
                    {
                        if (ev.Player.UserId == entry.Key)
                        {
                            if (IsColoredRanksEnabled == true)
                            {
                                string playername = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), entry.Value[0].Remove(0, 13).Replace("{playername}", playername));                                
                            }
                            else
                            {
                                Map.Broadcast(Convert.ToUInt16(JoinedMessageDuration), entry.Value[0].Remove(1, 13).Replace("{playername}", ev.Player.Nickname));
                            }
                        }                        
                    }
                    break;

                default:
                    Log.Info("The OnlySpecificPlayersBeDisplayedMode config variable can only be 0 - off, 1 - the players given should be displayed with a costume mesage and so do the others with the default, 2 - only the players given should be displayed only with the message given. ");
                break;
            }
        }   
        int LeftMessageDuration = Plugin.Instance.Config.LeftMessageDuration;
        string LeftMessage = Plugin.Instance.Config.LeftMessage;
        public void OnLeft(JoinedEventArgs ev)
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
                            Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), LeftMessage.Replace("{playername}", ColoredName));
                        }
                        else
                        {
                            if (ev.Player.RankColor != "default")
                            {
                                ColoredName = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), LeftMessage.Replace("{playername}", ColoredName));
                            }
                        }
                    }
                    else
                    {
                        if (OnlyRankedPlayersAreDisplayed == false)
                        {
                            Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), LeftMessage.Replace("{playername}", ev.Player.Nickname));
                        }
                        else
                        {
                            if (ev.Player.RankColor != "default")
                            {
                                Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), LeftMessage.Replace("{playername}", ev.Player.Nickname));
                            }
                        }
                    }
                break;

                case 1:
                    int iteration = 0;
                    foreach (KeyValuePair<string, List<string>> entry in Plugin.Instance.Config.SpecificPlayers)
                    {
                        iteration++;
                        if (ev.Player.UserId == entry.Key)
                        {
                            if (IsColoredRanksEnabled == true)
                            {
                                string playername = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), entry.Value[1].Remove(0, 13).Replace("{playername}", playername));                                
                            }
                            else
                            {
                                Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), entry.Value[1].Remove(1, 13).Replace("{playername}", ev.Player.Nickname));
                            }
                        }
                        else
                        {
                            if (iteration == Plugin.Instance.Config.SpecificPlayers.Count)
                            {
                                iteration = 0;
                                Log.Info(ev.Player.UserId + " and " + entry.Key);
                                Log.Info(entry.Value[1]);
                                goto case 0;
                            }
                        }
                    }
                break;

                case 2:
                    foreach (KeyValuePair<string, List<string>> entry in Plugin.Instance.Config.SpecificPlayers)
                    {
                        if (ev.Player.UserId == entry.Key)
                        {
                            if (IsColoredRanksEnabled == true)
                            {
                                string playername = $"<color={ ColorFix(ev.Player.RankColor) }>{ev.Player.Nickname}</color>";
                                Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), entry.Value[1].Remove(0, 13).Replace("{playername}", playername));
                            }
                            else
                            {
                                Map.Broadcast(Convert.ToUInt16(LeftMessageDuration), entry.Value[1].Remove(1, 13).Replace("{playername}", ev.Player.Nickname));
                            }
                        }
                    }
                    break;

                default:
                    Log.Info("The OnlySpecificPlayersBeDisplayedMode config variable can only be 0 - The specific players in the list are not displayed differently, 1 - the players given should be displayed with a costume mesage and so do the others with the default, 2 - only the players given should be displayed only with the message given. ");
                break;
            }
        }
    }
}