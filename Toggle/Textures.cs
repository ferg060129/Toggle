using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Toggle
{
    static class Textures
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static string[] graphicNames = { "kitten", "zombie" , "player", "greenblock","badgreenblock", "inventory"};
        public static string[] tileNames = { "grass", "grass4", "grass2", "grass3", "woodenwallbottomleftcorner", "woodenwallbottomrightcorner", 
                                               "woodenwalltopleftcorner", "woodenwalltoprightcorner", "woodenwallvertical", "woodenwallhorizontal1",
                                           "darkgrass","darkgrass2","darkgrass3","darkgrass4"};

    }
}
