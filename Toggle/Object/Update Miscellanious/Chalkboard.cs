using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Toggle
{
    class Chalkboard : UpdateMiscellanious
    {

        string currentAnswer = "";
        bool blinkUnderscore = false;
        KeyboardState newKeyBoardState, oldKeyBoardState;
        int ctr = 0;
        SpriteFont sf;
        int maxAnswerLength = 15;

        public Chalkboard(int xLoc, int yLoc) : base(xLoc,yLoc)
        {
            goodGraphic = Textures.textures["chalkboard"];
            badGraphic = Textures.textures["chalkboard"];
            width = 576;
            height = 384;
            imageBoundingRectangle = new Rectangle(0, 0, width, height);
            sf = Textures.fonts["mistral16"];
        }

        public override void move()
        {
            newKeyBoardState = Keyboard.GetState();

            Keys[] pressedKeys = newKeyBoardState.GetPressedKeys();
            
            foreach (Keys k in pressedKeys)
            {
                if(!oldKeyBoardState.IsKeyDown(k))
                {
                    if (k.Equals(Keys.Space))
                    {
                        if (maxAnswerLength >= currentAnswer.Length+1)
                        {
                            currentAnswer += " ";
                        }
                    }
                    else if (k.Equals(Keys.Back) && currentAnswer.Length > 0)
                    {
                        currentAnswer = currentAnswer.Substring(0, currentAnswer.Length - 1);
                    }
                    else if (maxAnswerLength >= currentAnswer.Length + 1 && k.ToString().Length == 1)
                    {
                        currentAnswer += k.ToString();
                    }
                }
            }

            oldKeyBoardState = newKeyBoardState;

            ctr++;
            if(blinkUnderscore && ctr >= 15)
            {
                blinkUnderscore = false;
                ctr = 0;
            }
            else if(!blinkUnderscore && ctr >= 15)
            {
                blinkUnderscore = true;
                ctr = 0;
            }

        }

        public string getAnswer()
        {
            if(blinkUnderscore)
            {
                return currentAnswer+" ";
            }
            else
            {
                return currentAnswer + "_";
            }
            
        }

        public SpriteFont getFont()
        {
            return sf;
        }

        public int getAnswerWidth()
        {
            return (int)sf.MeasureString(currentAnswer).X;
        }

        public int getAnswerHeight()
        {
            return (int)sf.MeasureString(currentAnswer).Y;
        }
    }
}
