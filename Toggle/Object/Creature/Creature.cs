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
    class Creature : Object
    {
        protected int velocity = 1;
        //0,1,2,3 for left, up, right,down
        protected int direction;
        protected Rectangle previousHitBox;

        //Variables to keep track of animation sprite.
        protected int column = 1, columnGroup = 0, increment = 1, row = 0, waitCounter = 0, oldDirection = -1; protected bool moving = true;


        public Creature(int xLocation, int yLocation, bool initialState) : base(xLocation, yLocation, initialState)
        {
            
        }

        public virtual void move()
        {
            oldDirection = direction;
            moving = true;

            previousHitBox = new Rectangle(x, y, width, height);
            if (state)
                goodMove();
            else
                badMove();

             imageBoundingRectangle = getNextImageRectangle(direction, oldDirection, moving);
            hitBox = new Rectangle(x, y, width, height);
        }

        public virtual void goodMove()
        {

        }
        public virtual void badMove()
        {

        }

        public virtual void reportCollision(Object o)
        {
            if (o is Wall)
            {
                Rectangle hBO = o.getHitBox(); //hitBoxOther
                if (direction == 0)
                {
                    x = hBO.X + hBO.Width;
                }
                else if (direction == 1)
                {
                    y = hBO.Y + hBO.Height;
                }
                else if (direction == 2)
                {
                    x = hBO.X - hitBox.Width;
                }
                else if (direction == 3)
                {
                    y = hBO.Y - hitBox.Height;
                }
            }
            if (o is Creature)
            {
                x = previousHitBox.X;
                y = previousHitBox.Y;
                /*
                Rectangle previousHBO = ((Creature)o).getPreviousHitBox();
                int directionOther = ((Creature)o).getDirection();

                if (hitBox.Intersects(previousHBO))
                {
                    if (direction == 0)
                    {
                        x = previousHBO.X + previousHBO.Width;
                    }
                    else if (direction == 1)
                    {
                        y = previousHBO.Y + previousHBO.Height;
                    }
                    else if (direction == 2)
                    {
                        x = previousHBO.X - hitBox.Width;
                    }
                    else if (direction == 3)
                    {
                        y = previousHBO.Y - hitBox.Height;
                    }
                }
                else
                {
                    //They were going in opposite directions when they collided
                    if (Math.Abs(directionOther - direction) == 2)
                    {
                        double proportion = (double)direction/(direction + directionOther);
                        
                        switch(direction){
                            case 0:
                                int distance = previousHBO.X - (previousHitBox.X + previousHitBox.Width);
                                int addx = (int)proportion * distance;
                                x = previousHitBox.X + addx;
                            break;
                            case 2:
                            distance = previousHitBox.X - (previousHBO.X + previousHBO.Width);
                                addx = (int)proportion * distance;
                                x = previousHitBox.X - addx;
                            break;
                            case 1:
                                distance = previousHitBox.Y - (previousHBO.Y + previousHBO.Height);
                                int addy = (int)proportion * distance;
                                y = previousHitBox.Y - addy;
                            break;
                            case 3:
                            distance = previousHBO.Y - (previousHitBox.Y + previousHitBox.Height);
                                addy = (int)proportion * distance;
                                y = previousHitBox.Y + addy;
                            break;
                        }
                    }
                    else
                    {
                        x = previousHitBox.X;
                        y = previousHitBox.Y;
                    }

                }
                */
            }
        }
        public int getDirection()
        {
            return direction;
        }
        public int getVelocity()
        {
            return velocity;
        }
        public Rectangle getPreviousHitBox()
        {
            return previousHitBox;
        }


        public Rectangle getNextImageRectangle(int currentDirection, int lastDirection, bool moving)
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
                column = 1;
                increment = 1;
                waitCounter = 0;
            }
            return new Rectangle(32 * (3 * columnGroup + column), 32 * row, width, height);
        }


    }
}
