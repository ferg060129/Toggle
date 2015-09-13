using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toggle
{
    class Player : Creature
    {
        KeyboardState oldKeyBoardState;

        public Player(int xLocation, int yLocation, bool initialState) : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures["player"];
            badGraphic = Textures.textures["player"];
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
            width = 32;
            height = 32;
        }


        public override void move()
        {
            KeyboardState newKeyBoardState = Keyboard.GetState();
            if (newKeyBoardState.IsKeyDown(Keys.Up))
            {
                y -= velocity;
            }
            else if (newKeyBoardState.IsKeyDown(Keys.Down))
            {
                y += velocity;
            }
            else if (newKeyBoardState.IsKeyDown(Keys.Left))
            {
                x -= velocity;
            }
            else if (newKeyBoardState.IsKeyDown(Keys.Right))
            {
                x += velocity;
            }

            oldKeyBoardState = newKeyBoardState;
            hitBox = new Rectangle(x, y, width, height);
        }

        public void pickUp(Item i)
        {

        }
    }
}
