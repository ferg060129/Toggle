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

        private string[] buttonInfo = {"Button|buttonUp|buttonUp|This is a thingy you should step on|This is still a tihngy",
                                      "Desk|desk|deskBad|This is a desk|whoooo hooo",
                                      "Ghost|ghost|unghost|cute|spooky"
                                      
                                      };


        public AboutScreen(Game1 eng) : base(eng)
        {
            int xCtr = 32, yCtr = 32;
            foreach(string s in buttonInfo)
            {
                string[]fields =  s.Split('|');
                ObjectButton b = new ObjectButton(xCtr, yCtr, fields[1], fields[2], fields[3], fields[4]);
                stringToButton.Add(fields[0], b);
                buttons.Add(b);
                xCtr += 64;
                if(xCtr >= 300)
                {
                    xCtr = 0;
                    yCtr += 32;
                }
            }



            
            
            
        }

        public override void drawScreen(SpriteBatch sb)
        {
            sb.Draw(Textures.textures["aboutScreen"], new Vector2(0, 0), Color.White);
            base.drawScreen(sb);

            if(hoveredButton != null)
            { 
                sb.Draw(hoveredButton.getGraphic(), drawHoveredLoc, hoveredButton.getImageBoundingRectangle(), Color.White);
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

    }
}
