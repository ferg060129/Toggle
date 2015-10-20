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
        public static string[] graphicNames = { "kitten", "zombie", "player", "greenblock", "badgreenblock", "itemblock", "baditemblock", "inventory2", "BigPicture", "player_right", "sprites",
                                              "moveableVineBlock","vineBlock","cursor", "animals"};
        public static string[] tileNames = { "grass", "grass4", "grass2", "grass3", "darkgrass", "darkgrass4", "darkgrass2", "darkgrass3",
                                           "woodenwallbottomleftcorner","woodenwallbottomrightcorner","woodenwalltopleftcorner","woodenwalltoprightcorner",
                                           "woodenwallvertical","woodenwallhorizontal1","frown","smile","locktile","unlocktile", "stone","blackBlock","woodfloor","darkwoodfloor"};

        public static Dictionary<char, string> charToFileName = new Dictionary<char, string>()
            {
                {'.', "grass,darkgrass,"},
                {'`', "grass2,darkgrass2,"},
                {',', "grass3,darkgrass3,"},
                {'*', "grass4,darkgrass4,"},
                {'!', "woodenwallbottomleftcorner,stone,w"},
                {'#', "woodenwallbottomrightcorner,stone,w"},
                {'$', "woodenwalltopleftcorner,stone,w"},
                {'%', "woodenwalltoprightcorner,stone,w"},
                {'|', "woodenwallvertical,stone,w"},
                {'-', "woodenwallhorizontal1,stone,w"},
                {'f', "frown,frown,"},
                {'s',"smile,smile,"},
                {'l',"locktile,locktile,"},
                {'a', "blackBlock,blackBlock,"},    //takes you to homelevel
                {'b', "blackBlock,blackBlock,"},
                {'u',"unlocktile,unlocktile,"},
                {'+',"woodfloor,darkwoodfloor"}

            };

    }
}
