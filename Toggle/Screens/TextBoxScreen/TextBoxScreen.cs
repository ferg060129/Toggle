using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Collections;

namespace Toggle
{
    class TextBoxScreen : Screen
    {
        protected Point textBoxLocation = new Point(0, 0);
        protected int padding = 15;
        protected int infoIndex = 0;
        protected string[] instructions;
        protected int currentStringLength = 0;
        protected int waitCtr = 0;
        protected int numInfoBlocks;
        protected Texture2D textBox;
        protected string currentInfoText = "";
        public TextBoxScreen(Game1 eng)
            : base(eng)
        {
            textBox = Textures.textures["textBox"];
            

        }

        public void nextString()
        {
            infoIndex++;
            if (infoIndex >= numInfoBlocks)
            {
                engine.setState("play","");
                infoIndex = 0;
            }
            currentStringLength = 0;
            waitCtr = 0;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            
        }

        public override void drawScreen(SpriteBatch sb)
        {
            MouseState mouseState = Mouse.GetState();
            Point mouseLoc = engine.convertCursorLocation(mouseState);
            sb.Draw(Textures.textures["textBox"], new Vector2(textBoxLocation.X, textBoxLocation.Y), Color.White);
            foreach (ScreenButton b in buttons)
            {
                sb.Draw(b.getGraphic(), new Vector2(b.getLocation().X, b.getLocation().Y), b.getImageBoundingRectangle(), Color.White);

            }
            sb.DrawString(Textures.fonts["arial12"], currentInfoText.Substring(0, currentStringLength), new Vector2(textBoxLocation.X + padding, textBoxLocation.Y + padding), Color.Black);
            sb.Draw(Textures.textures["cursor"], new Vector2(mouseLoc.X, mouseLoc.Y), Color.White);
            if (currentStringLength < currentInfoText.Length && waitCtr == 0)
            {
                currentStringLength++;

            }
            waitCtr++;
            if(waitCtr >= 2)
            {
                waitCtr = 0;
            }

        }


        public override void checkButtonClicks()
        {
            MouseState mouseState = Mouse.GetState();
            Point cursorLocation = engine.convertCursorLocation(mouseState);
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
            {
                foreach (InventoryScreenButton sb in buttons)
                {

                    if (sb.getClickBox().Contains(cursorLocation))
                    {
                        sb.onClick();
                    }
                }
            }
            oldMouseState = mouseState;
        }

        public string adjustTextForWrap(string s, SpriteFont sf)
        {
            string returnString = "     ";
            string currentLine = "     ";
            string nextWord = "";
            int idx = 0;
            while(idx < s.Length)
            {

                if(s[idx].Equals(' '))
                {
                    int currentLength = (int)sf.MeasureString(currentLine + nextWord).X;

                    if (currentLength + padding * 2 > textBox.Width)
                    {
                        returnString += '\n';
                        currentLine = "";
                        nextWord += " ";
                        idx++;
                        continue;
                    }
                    else
                    {
                        currentLine += nextWord+ " ";
                        returnString += nextWord + " ";
                        nextWord = "";
                        idx++;
                    }

                }
                else
                {
                    nextWord += s[idx];
                    idx++;
                }

                
                
            }

            //the last word
            int cLength = (int)sf.MeasureString(currentLine + nextWord).X;
            if (cLength + padding * 2 > textBox.Width)
            {
                returnString += '\n';
            }
            returnString += nextWord;
            return returnString;


        }



    }
}
