using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Toggle
{
    class AboutScreen : Screen
    {

        //public ObjectButton hoveredButton;
        Vector2 drawHoveredLoc = new Vector2(640, 84);
        Vector2 descriptionLoc = new Vector2(525, 224);
        Dictionary<string, ScreenButton> stringToButton = new Dictionary<string, ScreenButton>();

        private string[] buttonInfo = {
                                      "Desk|desk|deskBad|This is a desk.|This is a metal desk.",
                                      "Ghost|ghost|unghost|This little guy will follow you. \nHe can pass through walls.|This scary fellow will chase you down. \nHe cannot go through walls.",
                                      "VineMoveBlock|BoxLight|BoxDark|This is a pushable object.\nNote, enemies can push these too.|This object is not pushable.",
                                      "LaserBlock|lasBoxHori|lasBoxVert|This is a pushable block emitting a \ndeadly laser. Shifting causes the laser \nto rotate and change colors.|This is a pushable block emitting a \ndeadly laser. Shifting causes the laser \nto rotate and change colors.",
                                      "Strawberry|berry|berryRot|This is not passable.|This is passable.",
                                      "ButtonPlayer|buttonUp|buttonUp|Step on this to perform an effect.|Step on this to perform an effect.",
                                      "ButtonHeavy|buttonHUp|buttonHUp|You'll need to push a heavier object \nonto this.|You'll need to push a heavier object \nonto this.",
                                      "ButtonShadow|buttonSUp|buttonSUp|This tricky button takes a lot of presses\n to work. Seems to alternate between\n worlds. |This tricky button takes a lot of presses\n to work. Seems to alternate between\n worlds. ",
                                      "Gate|ClosedGate|ClosedGate|This is an impassable gate opened\noften by utilizing a button.|This is an impassable gate opened\noften by utilizing a button.",
                                      "BadTile|AngryButton|AngryButton|Stepping on this shifts to the bad \nworld.|Stepping on this shifts to the bad \nworld.",
                                      "GoodTile|HappyButton|HappyButton|Stepping on this shifts to the good \nworld.|Stepping on this shifts to the good \nworld.",
                                      "LockTile|HoleTile|HoleTile|Stepping on this locks your ability\nto shift.|Stepping on this locks your ability\nto shift.",
                                      "UnlockTile|KeyButton|KeyButton|Stepping on this unlocks your ability\nto shift.|Stepping on this unlocks your ability\nto shift.",
                                      "Grate|grate|grateRust|Lasers can pass through, but you can't.|Lasers can pass through, but you can't.",
                                      "DiaryPlatform|diaryPlatformG|diaryPlatformB|Place your diary here.|Place your diary here.",
                                      "KnifePlatform|knifePlatform|knifePlatform|Place knife here.|Place knife here.",
                                      "RosePlatform|rosePlatform|rosePlatform|Place rose here.|Place rose here.",
                                      "Boat|boat|boat|Hop aboard to leave this world.|Hop aboard to leave this world."
                                      };


        public AboutScreen(Game1 eng) : base(eng)
        {
            int xCtr = 32, yCtr = 64;
            foreach(string s in buttonInfo)
            {
                string[]fields =  s.Split('|');
                ObjectButton b = new ObjectButton(xCtr, yCtr, fields[1], fields[2], fields[3], fields[4]);
                stringToButton.Add(fields[0], b);
                buttons.Add(b);
                xCtr += 64;
                if(xCtr >= 448)
                {
                    xCtr = 32;
                    yCtr += 64;
                }
            }
        }

        public override void drawScreen(SpriteBatch sb)
        {
            sb.Draw(Textures.textures["aboutScreen2"], new Vector2(0, 0), Color.White);
            base.drawScreen(sb);

            if(hoveredButton != null)
            { 
                sb.Draw(hoveredButton.getGraphic(), drawHoveredLoc, hoveredButton.getImageBoundingRectangle(), Color.White);
                sb.Draw(Textures.textures["glow32"], new Vector2(hoveredButton.getLocation().X - 3, hoveredButton.getLocation().Y - 3), new Rectangle(0, 0, 38, 38), Color.White);;
                sb.DrawString(Textures.fonts["arial12"], ((ObjectButton)hoveredButton).getDescription(), descriptionLoc, Color.Black);
            }

        }


        public void addSeenObject(Object o)
        {
            string s = o.GetType().Name;
            if (stringToButton.ContainsKey(s))
            ((ObjectButton)stringToButton[s]).setSeen(true);
        }
        public void addSeenObject(string s)
        {
            if (stringToButton.ContainsKey(s))
                ((ObjectButton)stringToButton[s]).setSeen(true);
        }


        //seenObjects is an arraylist of strings storing the file names of the objects which were seen during last play.
        public void setSeenObjectsContinue(ArrayList seenObjects)
        {
            foreach(string s in seenObjects)
            {
                if (stringToButton.ContainsKey(s))
                    ((ObjectButton)stringToButton[s]).setSeen(true);
            }
        }

        public ArrayList getSeenObjects()
        {
            ArrayList strs = new ArrayList();
            foreach(string s in stringToButton.Keys)
            {
                if (((ObjectButton)stringToButton[s]).wasSeen())
                {
                    strs.Add(s);
                }
            }
            return strs;
        }

        public override void checkButtonHovers()
        {
            MouseState mouseState = Mouse.GetState();
            foreach (ScreenButton sb in buttons)
            {
                Point cursorLocation = engine.convertCursorLocation(mouseState);
                if (sb.getClickBox().Contains(cursorLocation))
                {
                    sb.onHover();
                    hoveredButton = sb;
                    return;
                }
            }
            hoveredButton = null;
        }

    }
}
