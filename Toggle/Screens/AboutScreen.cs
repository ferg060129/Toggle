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

        private string[] buttonInfo = {"ButtonPlayer|buttonUp|buttonUp|This is a thingy you should step on|This is still a tihngy",
                                      "ButtonHeavy|buttonHUp|buttonHUp|You'll need to push a heavier object \nonto this|You'll need to push a heavier object \nonto this",
                                      "Desk|desk|deskBad|This is a desk|whoooo hooo",
                                      "Ghost|ghost|unghost|cute|spooky",
                                      "VineMoveBlock|BoxLight|BoxDark|This is a pushable object\nNote, enemies can push these too|This object is not pushable",
                                      "Gate|ClosedGate|ClosedGate|This is an impassable gate opened\noften by utilizing a button|This is an impassable gate opened\noften by utilizing a button",
                                      "LaserBlock|lasBoxHori|lasBoxVert|This is a laser which cannot be touched\nShifting causes the laser to rotate\nand change colors|This is a laser which cannot be touched\nShifting causes the laser to rotate\nand change colors",
                                      "Strawberry|berry|berryRot|This is not passable|This is passable",
                                      "KnifePlatform|knifePlatform|knifePlatform|place knife here|place knife here",
                                      "LanternPlatform|lanternPlatform|lanternPlatform|place lantern here|place lantern here",
                                      "Boat|boat|boat|Hop aboard to win the game|Hop aboard to win the game",
                                      "BadTile|AngryButton|AngryButton|Stepping on this shifts to the bad world|Stepping on this shifts to the bad world",
                                      "GoodTile|HappyButton|HappyButton|Stepping on this shifts to the good world|Stepping on this shifts to the good world",
                                      "LockTile|HoleTile|HoleTile|Stepping on this locks your ability\nto shift|Stepping on this locks your ability\nto shift",
                                      "UnlockTile|KeyButton|KeyButton|Stepping on this unlocks your ability\nto shift|Stepping on this unlocks your ability\nto shift",
                                      "Grate|grate|grateRust|Lasers can pass through, but you can't|Lasers can pass through, but you can't",
                                      "DiaryPlatform|diaryPlatformG|diaryPlatformB|Place your diary here|Place you diary here"
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

        //Add extra condition for blue button
        public void addSeenObject(Object o)
        {
            string s = o.GetType().Name;
            if (stringToButton.ContainsKey(s))
            ((ObjectButton)stringToButton[s]).setSeen(true);
        }

        //Add extra condition for blue button
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
