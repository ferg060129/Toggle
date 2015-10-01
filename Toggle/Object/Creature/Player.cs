using System;
using System.Collections.Generic;
using System.Collections;
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
        Inventory inventory;
        Game1 engine;
        bool stateLocked = false;

        //Variables to keep track of animation sprite.
        int column = 1, columnGroup = 0, increment = 0, row = 2, waitCounter = 0;

        public Player(int xLocation, int yLocation, bool initialState, Inventory i, Game1 eng) : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures["sprites"];
            badGraphic = Textures.textures["sprites"];
            imageBoundingRectangle = new Rectangle(32, 32, 32, 32);
            width = 32;
            height = 32;
            velocity = 3;
            collidable = false;
            inventory = i;
            engine = eng;
           
        }


        public override void move()
        {
            previousHitBox = new Rectangle(x, y, width, height);
            KeyboardState newKeyBoardState = Keyboard.GetState();

            //Variables to keep track of animation sprite.
            int oldDirection = direction; bool moving = true;
            
            if (newKeyBoardState.IsKeyDown(Keys.Up))
            {
                direction = 1;
                y -= velocity;
            }
            else if (newKeyBoardState.IsKeyDown(Keys.Down))
            {
                direction = 3;
                y += velocity;
            }
            else if (newKeyBoardState.IsKeyDown(Keys.Left))
            {
                direction = 0;
                x -= velocity;
            }
            else if (newKeyBoardState.IsKeyDown(Keys.Right))
            {
                direction = 2;
                x += velocity;
            }
            else
            {
                moving = false;
            }
            if (newKeyBoardState.IsKeyDown(Keys.T) && oldKeyBoardState != null && !oldKeyBoardState.IsKeyDown(Keys.T))
            {
                if(!stateLocked)
                    engine.switchStates();
            }
            oldKeyBoardState = newKeyBoardState;

            //Get next image for sprite
            imageBoundingRectangle = getNextImageRectangle(direction, oldDirection, moving);
            hitBox = new Rectangle(x, y, width, height);
        }


        public void pickUp(Item i)
        {
            inventory.addInventoryItem(i.pickUpItem());
        }
        public override void reportCollision(Object o)
        {
            base.reportCollision(o);
            if (o is Item)
            {
                pickUp((Item)o);
            }
            if (o is GoodTile)
            {
                if (!state)
                {
                    engine.switchStates();
                }
            }
            if(o is BadTile)
            {
                if (state)
                {
                    engine.switchStates();
                }
            }
            if (o is LockTile)
            {
                stateLocked = true;
            }
            if(o is UnlockTile)
            {
                stateLocked = false;
            }

        }

        public Rectangle getNextImageRectangle(int currentDirection, int lastDirection, bool moving)
        {
            if (currentDirection == lastDirection && moving)
            {
                if (column == 2)
                {
                    increment = -1;
                }
                if (column == 0)
                {
                    increment = 1;
                }
                waitCounter++;

                if (waitCounter == 5)
                {
                    column += increment;
                    waitCounter = 0;
                }
            }
            else
            {
                switch (currentDirection)
                {
                    case 0:
                        columnGroup = 3;
                        break;
                    case 1:
                        columnGroup = 2;
                        break;
                    case 2:
                        columnGroup = 1;
                        break;
                    case 3:
                        columnGroup = 0;
                        break;
                }
                column = 1;
                increment = 1;
                waitCounter = 0;
            }
            return new Rectangle(32 * (3 * columnGroup + column), 32 * row, width, height);
        }
    }
}
