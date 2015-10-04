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
        public static string[] graphicNames = { "kitten", "zombie", "player", "greenblock", "badgreenblock", "itemblock", "baditemblock", "inventory", "BigPicture", "player_right", "sprites" };
        public static string[] tileNames = { "grass", "grass4", "grass2", "grass3", "darkgrass", "darkgrass4", "darkgrass2", "darkgrass3",
                                           "woodenwallbottomleftcorner","woodenwallbottomrightcorner","woodenwalltopleftcorner","woodenwalltoprightcorner",
                                           "woodenwallvertical","woodenwallhorizontal1","frown","smile","locktile","unlocktile"};

        public static Dictionary<char, string> charToFileName = new Dictionary<char, string>()
            {
                {'1', "grass,darkgrass,"},
                {'2', "grass2,darkgrass2,"},
                {'3', "grass3,darkgrass3,"},
                {'4', "grass4,darkgrass4,"},
                {'!', "woodenwallbottomleftcorner,woodenwallbottomleftcorner,w"},
                {'#', "woodenwallbottomrightcorner,woodenwallbottomrightcorner,w"},
                {'$', "woodenwalltopleftcorner,woodenwalltopleftcorner,w"},
                {'%', "woodenwalltoprightcorner,woodenwalltoprightcorner,w"},
                {'5', "woodenwallvertical,woodenwallvertical,w"},
                {'6', "woodenwallhorizontal1,woodenwallhorizontal1,w"},
                {'f', "frown,frown,"},
                {'s',"smile,smile,"},
                {'l',"locktile,locktile,"},
                {'u',"unlocktile,unlocktile,"}

            };

    }
}
