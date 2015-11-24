using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
namespace Toggle
{
    static class Textures
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        public static Dictionary<string, SoundEffectInstance> sounds = new Dictionary<string, SoundEffectInstance>();
        public static string[] graphicNames = { "protagsheet", "itemblock", "baditemblock", "inventory2", "sprites",
                                               "BoxDark","BoxLight","lasBoxHori","lasBoxVert","cursor","battery","berry","berryRot", "start","startHover","continue","continueHover","exit","exitHover",
                                               "buttonUp","buttonDown","OpenGate","ClosedGate",
                                               "LitLantern","UnlitLantern","BustedLantern", "shiftCooldown","hourglass","hourglass2","shiftlocked","buttonHUp","buttonHDown",
                                               "titleScreen3","lostScreen","blackScreen","controls1","controls2","rays","darkHaze","pause","shadowScreen", "whiteblock", "grayblock",
                                                "shiftCooldownBar", "house", "housedark", "housedarkNoEyes","GoopFrame1","GoopFrame2","GoopFrame3", "school","schooldark","laser","laserB",
                                              "water","water2","water3","water4", "boat", "whiteScreen","ghost","unghost","chalkboard2","chalkboard3", "chalkboardtop", "ItemBox", "boxInside",
                                              "desk","deskBad","door","doorDark","knife","knifePlatform","lanternPlatform","knifePlatformComplete","lanternPlatformComplete", "rope","chain",
                                              "aboutScreen2", "questionBox","fence", "barbedBottomLeft","barbedHor","barbedVertical1","blocks","goodBlob","badBlob","title","help",
                                              "diary","diaryBad","diaryPlatformB","diaryPlatformG","diaryPlatformCompleteB","diaryPlatformCompleteG","glow32","hiddenArrow","nothing","ghostGate","buttonSUp",
                                              "torchGood","torchBad","rose","rosePlatform","rosePlatformComplete","playerGhost","inventorytutorial", "textBox","hourglassinstruction","particleShadow", "leftArrow", "rightArrow",
                                              "leftArrowHovered", "rightArrowHovered", "xButton","xButtonHovered"};


        public static string[] tileNames = {"grass5","grass6","grass7","grass8","darkgrass5","darkgrass6","darkgrass7","darkgrass8",
                                           "woodenwallbottomleftcorner","woodenwallbottomrightcorner","woodenwalltopleftcorner","woodenwalltoprightcorner",
                                           "woodenwallvertical","woodenwallhorizontal1","HappyButton","HappyButtonPressed","AngryButton","AngryButtonPressed","HoleTile", "HoleTilePressed",
                                           "KeyButton","KeyButtonPressed","stone","blackBlock","woodfloor","darkwoodfloor","grate","grateRust","WoodenWall","woodenWallR","woodenWallU","woodenWallD","woodenWallL",
                                           "waterTile","waterTile2","waterTile3","waterTile4","waterTileBad","waterTileBad2","waterTileBad3","waterTileBad4","muddytile1","muddytile2",};
        public static string[] spritefonts = { "mistral16", "arial12" };
        public static string[] soundsNames = { "beep","unlock","lock","hit","pickup","weakhit"};

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
                {'g', "grate,grateRust,w"},
                {'u', "KeyButtonPressed,KeyButton,"},
                {'+', "woodfloor,darkwoodfloor,"},
                {'d', "desk,deskBad,w"},
                {'w', "waterTile,waterTileBad,w"},
                {'m', "muddytile1,muddytile1, w"},
                {'n', "muddytile2,muddytile2, w"}
            };
        

    }
}
