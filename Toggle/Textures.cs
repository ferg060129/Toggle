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
                                               "BoxDark","BoxLight","lasBoxHori","lasBoxVert","cursor", "animals","battery","goo","berry","berryRot", "start","exit","buttonUp","buttonDown","OpenGate","ClosedGate",
                                               "LitLantern","UnlitLantern","BustedLantern", "shiftCooldown","hourglass","hourglass2","shiftlocked","buttonHUp","buttonHDown",
                                               "titleScreen3","lostScreen","blackScreen","controls1","controls2","rays","darkHaze","pause","shadowScreen", "whiteblock", "grayblock",
                                                "shiftCooldownBar", "house", "housedark", "housedarkNoEyes","GoopFrame1","GoopFrame2","GoopFrame3", "school","schooldark","laser",
                                              "water","water2","water3","water4", "boat", "whiteScreen","ghost","unghost"};
        public static string[] tileNames = { "grass", "grass4", "grass2", "grass3","grass5","grass6","grass7","grass8", "darkgrass", "darkgrass4", "darkgrass2", "darkgrass3","darkgrass5","darkgrass6","darkgrass7","darkgrass8",
                                           "woodenwallbottomleftcorner","woodenwallbottomrightcorner","woodenwalltopleftcorner","woodenwalltoprightcorner",
                                           "woodenwallvertical","woodenwallhorizontal1","HappyButton","HappyButtonPressed","AngryButton","AngryButtonPressed","HoleTile", "HoleTilePressed",
                                           "KeyButton","KeyButtonPressed","stone","blackBlock","woodfloor","darkwoodfloor"};

        public static Dictionary<char, string> charToFileName = new Dictionary<char, string>()
            {
                {'.', "grass5,darkgrass5,"},
                {'`', "grass6,darkgrass6,"},
                {',', "grass7,darkgrass7,"},
                {'*', "grass8,darkgrass8,"},
                {'!', "woodenwallbottomleftcorner,stone,w"},
                {'#', "woodenwallbottomrightcorner,stone,w"},
                {'$', "woodenwalltopleftcorner,stone,w"},
                {'%', "woodenwalltoprightcorner,stone,w"},
                {'|', "woodenwallvertical,stone,w"},
                {'-', "woodenwallhorizontal1,stone,w"},
                {'f', "AngryButtonPressed,AngryButton,"},
                {'s', "HappyButtonPressed,HappyButton,"},
                {'l', "HoleTilePressed,HoleTile,"},
                {'a', "blackBlock,blackBlock,"},    //takes you to glevel
                {'b', "blackBlock,blackBlock,"},
                {'c', "blackBlock,blackBlock,"},
                {'u', "KeyButtonPressed,KeyButton,"},
                {'+', "woodfloor,darkwoodfloor,"}

            };

    }
}
