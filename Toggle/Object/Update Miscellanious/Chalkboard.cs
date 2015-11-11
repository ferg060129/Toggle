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

        bool holdingBackspace = false;
        int backSpaceCtr = 0;
        int backSpaceWait = 0;

        Gate myGate;

        public Chalkboard(int xLoc, int yLoc, Gate g) : base(xLoc,yLoc)
        {
            goodGraphic = Textures.textures["chalkboard2"];
            badGraphic = Textures.textures["chalkboard3"];
            width = 576;
            height = 384;
            imageBoundingRectangle = new Rectangle(0, 0, width, height);
            sf = Textures.fonts["mistral16"];
            myGate = g;
        }

        public override void move()
        {
            newKeyBoardState = Keyboard.GetState();

            Keys[] pressedKeys = newKeyBoardState.GetPressedKeys();
            
            if(!newKeyBoardState.IsKeyDown(Keys.Back))
            {
                holdingBackspace = false;
                backSpaceCtr = 0;
                backSpaceWait = 0;
            }

            foreach (Keys k in pressedKeys)
            {
                if(k.Equals(Keys.Back))
                {
                    if(!oldKeyBoardState.IsKeyDown(k) || holdingBackspace)
                    {
                        
                        if (backSpaceWait == 0 && currentAnswer.Length > 0)
                        {
                            currentAnswer = currentAnswer.Substring(0, currentAnswer.Length - 1);
                            backSpaceWait = 3;
                        }
                        backSpaceWait--;

                        
                    }
                    else
                    {
                        backSpaceCtr++;
                        if (backSpaceCtr >= 20)
                        {
                            holdingBackspace = true;
                            backSpaceCtr = 0;
                        }
                    }
                }

                if(!oldKeyBoardState.IsKeyDown(k))
                {
                    if (k.Equals(Keys.Space))
                    {
                        if (maxAnswerLength >= currentAnswer.Length+1)
                        {
                            currentAnswer += " ";
                        }
                    }
                    else if (maxAnswerLength >= currentAnswer.Length + 1 && k.ToString().Length == 1)
                    {
                        currentAnswer += k.ToString();
                    }
                    else if(k.Equals(Keys.Enter))
                    {
                        submitAnswer();
                        currentAnswer = "";
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

        public void submitAnswer()
        {
            //Game1.chalkboardCommand(currentAnswer);

            if(currentAnswer.Equals("BANANA"))
            {
                myGate.onButton();
            }
        }
    }
}
