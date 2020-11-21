using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerJoiningAndLeavingAnnouncer.Handlers
{
    static class CustomStatement
    {
        public static bool IsWorkingColor(this string PlyColor)
        {
            if (PlyColor == "pink" || PlyColor == "light_green" || PlyColor == "crimson" || PlyColor == "deep_pink" || PlyColor == "tomato" || PlyColor == "blue_green" || PlyColor == "emerald" || PlyColor == "carmine" || PlyColor == "nickel" || PlyColor == "mint" || PlyColor == "army_green" || PlyColor == "pumpkin" || PlyColor == "gold" || PlyColor == "light_red" || PlyColor == "silver_blue" || PlyColor == "police_blue")
            {
                return false;
            }
            else
            {
                return true;
            }            
        }
       
    }
}
