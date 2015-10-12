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

        protected int defendTileGoodX, defendTileGoodY, defendTileBadX, defendTileBadY;

        //Variables to keep track of animation sprite.
        protected int column = 1, columnGroup = 0, increment = 1, row = 0, waitCounter = 0, oldDirection = -1; protected bool moving = true;


        public Creature(int xLocation, int yLocation) : base(xLocation, yLocation)
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
                    //x = hBO.X + hBO.Width;
                    x = previousHitBox.X;
                    y = previousHitBox.Y;
                }
                else if (direction == 1)
                {
                    //y = hBO.Y + hBO.Height;
                    x = previousHitBox.X;
                    y = previousHitBox.Y;
                }
                else if (direction == 2)
                {
                    //x = hBO.X - hitBox.Width;
                    x = previousHitBox.X;
                    y = previousHitBox.Y;
                }
                else if (direction == 3)
                {
                    //y = hBO.Y - hitBox.Height;
                    x = previousHitBox.X;
                    y = previousHitBox.Y;
                }
            }
            if (o is Creature)
            {
                //x = previousHitBox.X;
                //y = previousHitBox.Y;

                Rectangle previousHBO = ((Creature)o).getPreviousHitBox();
                int directionOther = ((Creature)o).getDirection();

                if (hitBox.Intersects(previousHBO))
                {
                    if (direction == 0)
                    {
                        x = previousHitBox.X;
                        y = previousHitBox.Y;
                        //x = previousHBO.X + previousHBO.Width;
                    }
                    else if (direction == 1)
                    {
                        x = previousHitBox.X;
                        y = previousHitBox.Y;
                        //y = previousHBO.Y + previousHBO.Height;
                    }
                    else if (direction == 2)
                    {
                        x = previousHitBox.X;
                        y = previousHitBox.Y;
                        //x = previousHBO.X - hitBox.Width;
                    }
                    else if (direction == 3)
                    {
                        x = previousHitBox.X;
                        y = previousHitBox.Y;
                        // y = previousHBO.Y - hitBox.Height;
                    }
                }
                /*
            else
            {
                //They were going in opposite directions when they collided
                x = previousHitBox.X;
                y = previousHitBox.Y;
                /*
                if (Math.Abs(directionOther - direction) == 2 && ((Creature)o).moving && moving)
                {
                    double proportion = (double)direction/(direction + directionOther);
                        
                    switch(direction){
                        case 0:
                            int distance = previousHBO.X - (previousHitBox.X + previousHitBox.Width);
                            int addx = (int)(proportion * distance);
                            x = previousHitBox.X + addx;
                        break;
                        case 2:
                        distance = previousHitBox.X - (previousHBO.X + previousHBO.Width);
                            addx = (int)(proportion * distance);
                            x = previousHitBox.X - addx;
                        break;
                        case 1:
                            distance = previousHitBox.Y - (previousHBO.Y + previousHBO.Height);
                            int addy = (int)(proportion * distance);
                            y = previousHitBox.Y - (addy - 1);
                        break;
                        case 3:
                        distance = previousHBO.Y - (previousHitBox.Y + previousHitBox.Height);
                            addy = (int)(proportion * distance);
                            y = previousHitBox.Y + addy;
                        break;
                    }
                }
                else
                {
                   // x = previousHitBox.X;
                   // y = previousHitBox.Y;
                }
            }
            */
            }

            if (o is Pushable)
            {
                if (((Pushable)o).push(direction, velocity))
                {

                }
                else
                {
                    x = previousHitBox.X;
                    y = previousHitBox.Y;
                }
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




        public int getNextPathDirection(int currentTileX, int currentTileY, int desiredTileX, int desiredTileY)
        {

            if (currentTileX == desiredTileX && currentTileY == desiredTileY)
            {
                return -1;
            }

            int yTiles = Game1.wallArray.GetLength(0);
            int xTiles = Game1.wallArray.GetLength(1);


            bool[,] visited = new bool[yTiles, xTiles];
            Queue<TileNode> q = new Queue<TileNode>();

            TileNode start = new TileNode (currentTileX, currentTileY);
            TileNode end = new TileNode(desiredTileX, desiredTileY);
            q.Enqueue(start);
            visited[start.Y, start.X] = true;

            while(q.Count != 0)
            {
                TileNode next = q.Dequeue();
                if (next.X!=end.X || next.Y!=end.Y)
                {
                    if (next.X + 1 < xTiles && !visited[next.Y, next.X + 1] && !Game1.wallArray[next.Y, next.X + 1])
                    {
                        TileNode temp = new TileNode(next.X + 1, next.Y, next);
                        visited[next.Y, next.X + 1] = true;
                        q.Enqueue(temp);
                    }
                    if (next.X - 1 >= 0 && !visited[next.Y, next.X - 1] && !Game1.wallArray[next.Y, next.X - 1])
                    {
                        TileNode temp = new TileNode(next.X - 1, next.Y, next);
                        visited[next.Y, next.X - 1] = true;
                        q.Enqueue(temp);
                    }
                    if (next.Y + 1 < yTiles && !visited[next.Y + 1, next.X] && !Game1.wallArray[next.Y + 1, next.X])
                    {
                        TileNode temp = new TileNode(next.X, next.Y + 1, next);
                        visited[next.Y + 1, next.X] = true;
                        q.Enqueue(temp);
                    }
                    if (next.Y - 1 >= 0 && !visited[next.Y - 1, next.X] && !Game1.wallArray[next.Y - 1, next.X])
                    {
                        TileNode temp = new TileNode(next.X, next.Y - 1, next);
                        visited[next.Y - 1, next.X] = true;
                        q.Enqueue(temp);
                    }
                }
                else
                {
                    while (!next.Parent.Equals(start))
                    {
                        next = (TileNode)next.Parent;
                    }
                    if (next.X < start.X)
                    {
                        return 0;
                    }
                    if (next.X > start.X)
                    {
                        return 2;
                    }
                    if (next.Y < start.Y)
                    {
                        return 1;
                    }
                    if (next.Y > start.Y)
                    {
                        return 3;
                    }
                }
                
            }
            return -1;
        }




        public void setDefendTileGood(int dTX, int dTY)
        {
            defendTileGoodX = dTX;
            defendTileGoodY = dTY;
        }

        public void setDefendTileBad(int dTX, int dTY)
        {
            defendTileBadX = dTX;
            defendTileBadY = dTY;
        }





        public class TileNode
        {
            public int X;
            public int Y;
            public TileNode Parent;

            public TileNode(int X, int Y, TileNode Parent)
            {
                this.X = X;
                this.Y = Y;
                this.Parent = Parent;
            }
            public TileNode(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }





    }
}
