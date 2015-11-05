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
        public static string[] graphicNames = { "kitten", "zombie", "player", "protagsheet", "greenblock", "badgreenblock", "itemblock", "baditemblock", "inventory2", "BigPicture", "player_right", "sprites",
                                               "BoxDark","BoxLight","lasBoxHori","lasBoxVert","cursor", "animals","battery","goo","berry","berryRot", "start","exit","buttonUp","buttonDown","gateOpen","gateClose",
                                               "lampoff","lampon","shiftCooldown","hourglass","hourglass2","shiftlocked","buttonHUp","buttonHDown",
                                               "titleScreen3","lostScreen","blackScreen","rays","darkHaze","pause","shadowScreen", "whiteblock", "grayblock",
                                                "shiftCooldownBar"};
        public static string[] tileNames = { "grass", "grass4", "grass2", "grass3","grass5","grass6","grass7","grass8", "darkgrass", "darkgrass4", "darkgrass2", "darkgrass3",
                                           "woodenwallbottomleftcorner","woodenwallbottomrightcorner","woodenwalltopleftcorner","woodenwalltoprightcorner",
                                           "woodenwallvertical","woodenwallhorizontal1","HappyButton","HappyButtonPressed","AngryButton","AngryButtonPressed","locktile",
                                           "KeyButton","KeyButtonPressed","stone","blackBlock","woodfloor","darkwoodfloor"};

        public static Dictionary<char, string> charToFileName = new Dictionary<char, string>()
            {
                {'.', "grass5,darkgrass,"},
                {'`', "grass6,darkgrass2,"},
                {',', "grass7,darkgrass3,"},
                {'*', "grass8,darkgrass4,"},
                {'!', "woodenwallbottomleftcorner,stone,w"},
                {'#', "woodenwallbottomrightcorner,stone,w"},
                {'$', "woodenwalltopleftcorner,stone,w"},
                {'%', "woodenwalltoprightcorner,stone,w"},
                {'|', "woodenwallvertical,stone,w"},
                {'-', "woodenwallhorizontal1,stone,w"},
                {'f', "AngryButtonPressed,AngryButton,"},
                {'s', "HappyButtonPressed,HappyButton,"},
                {'l', "locktile,locktile,"},
                {'a', "blackBlock,blackBlock,"},    //takes you to glevel
                {'b', "blackBlock,blackBlock,"},
                {'c', "blackBlock,blackBlock,"},
                {'u', "KeyButtonPressed,KeyButton,"},
                {'+', "woodfloor,darkwoodfloor,"}

            };

    }
}
