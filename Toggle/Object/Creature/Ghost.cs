using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toggle
{
    class Ghost : Creature
    {
        private Point boundTopLeft, boundBottomRight;

        public Ghost(int xLocation, int yLocation, Point bTL, Point bBR)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["ghost"];
            badGraphic = Textures.textures["unghost"];
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);

            direction = 0;
            velocity = 8;
            boundTopLeft = bTL;
            boundBottomRight = bBR;
        }

        public override void move()
        {
            oldDirection = direction;
            moving = true;

            previousHitBox = new Rectangle(x, y, width, height);
            if (state)
            {
                goodMove();
                hitBox = new Rectangle(0, 0, 0, 0);
            }
            else
            {
                badMove();
                hitBox = new Rectangle(x, y, width, height);
            }
           
        }

        public override void onShift()
        {
            if (state)
            {
                spriteAlpha = .75f;
                imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
            }
            else
            {
                spriteAlpha = 1;
            }
            for (int i = 0; i < 32; i++)
            {
                if (x % 32 != 0)
                {
                    x++;
                }
                if (y % 32 != 0)
                {
                    y++;
                }
            }
        }

        public override void goodMove()
        {
            velocity = 1;
            if ((x % 32 == 0 && y % 32 == 0) && Function.distanceTo(this, getPlayer()) > 96)
            {
                defendTileGoodX = (int)(getPlayer().getCenter().X / 32);
                defendTileGoodY = (int)(getPlayer().getCenter().Y / 32);
                direction = getNextPathDirection((int)x / 32, (int)y / 32, defendTileGoodX, defendTileGoodY);
            }

            int xprev = x;
            int yprev = y;
            switch (direction)
            {
                case 0:
                    x -= velocity;
                    break;
                case 1:
                    y -= velocity;
                    break;
                case 2:
                    x += velocity;
                    break;
                case 3:
                    y += velocity;
                    break;
                default:
                    moving = false;
                    break;
            }

            if (x < 0 || x > boundBottomRight.X)
            {
                x = xprev;
            }
            if (y < 0 || y > boundBottomRight.Y)
            {
                y = yprev;
            }
        }

        public override void badMove()
        {
            if (x % 32 == 0 && y % 32 == 0)
            {
                imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
                velocity = 4;
                if (Game1.random.Next(0, 3) == 1)
                {
                    velocity = 8;
                    imageBoundingRectangle = new Rectangle(32, 0, 32, 32);
                }
                defendTileBadX = (int)(getPlayer().getCenter().X / 32);
                defendTileBadY = (int)(getPlayer().getCenter().Y / 32);
                direction = getNextPathDirection((int)x / 32, (int)y / 32, defendTileBadX, defendTileBadY);
            }
            switch (direction)
            {
                case 0:
                    x -= velocity;
                    break;
                case 1:
                    y -= velocity;
                    break;
                case 2:
                    x += velocity;
                    break;
                case 3:
                    y += velocity;
                    break;
                default:
                    imageBoundingRectangle = new Rectangle(64, 0, 32, 32);
                    moving = false;
                    break;
            }
        }

        public override void reportCollision(Object o)
        {
            if (!state)
            {
                if ((o is Wall) || ((o is Lake) && !onBoat))
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
                    if (o is Ghost)
                    {
                        x = Game1.random.Next(boundBottomRight.X / 32) * 32;
                        y = Game1.random.Next(boundBottomRight.Y / 32) * 32;
                    }
                }
                
            }

            if (o is Miscellanious)
            {
                if (o.getCollision() == true)
                {
                    x = previousHitBox.X;
                    y = previousHitBox.Y;
                }
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
            hitBox = new Rectangle(x, y, width, height);

        }

        public override int getNextPathDirection(int currentTileX, int currentTileY, int desiredTileX, int desiredTileY)
        {

            if (currentTileX == desiredTileX && currentTileY == desiredTileY)
            {
                return -1;
            }

            if (tileIsOccupied(desiredTileX, desiredTileY))
            {
                return -1;
            }
            int yTiles = Game1.wallArray.GetLength(0);
            int xTiles = Game1.wallArray.GetLength(1);


            bool[,] visited = new bool[yTiles, xTiles];
            Queue<TileNode> q = new Queue<TileNode>();

            TileNode start = new TileNode(currentTileX, currentTileY);
            TileNode end = new TileNode(desiredTileX, desiredTileY);
            q.Enqueue(start);
            visited[start.Y, start.X] = true;



            while (q.Count != 0)
            {
                TileNode next = q.Dequeue();
                if (next.X != end.X || next.Y != end.Y)
                {
                    if (next.X + 1 < xTiles && !visited[next.Y, next.X + 1] && (state || !Game1.wallArray[next.Y, next.X + 1]) && !tileIsOccupied(next.X + 1, next.Y))
                    {
                        TileNode temp = new TileNode(next.X + 1, next.Y, next);
                        visited[next.Y, next.X + 1] = true;
                        q.Enqueue(temp);
                    }
                    if (next.X - 1 >= 0 && !visited[next.Y, next.X - 1] && (state || !Game1.wallArray[next.Y, next.X - 1]) && !tileIsOccupied(next.X - 1, next.Y))
                    {
                        TileNode temp = new TileNode(next.X - 1, next.Y, next);
                        visited[next.Y, next.X - 1] = true;
                        q.Enqueue(temp);
                    }
                    if (next.Y + 1 < yTiles && !visited[next.Y + 1, next.X] && (state || !Game1.wallArray[next.Y + 1, next.X]) && !tileIsOccupied(next.X, next.Y + 1))
                    {
                        TileNode temp = new TileNode(next.X, next.Y + 1, next);
                        visited[next.Y + 1, next.X] = true;
                        q.Enqueue(temp);
                    }
                    if (next.Y - 1 >= 0 && !visited[next.Y - 1, next.X] && (state || !Game1.wallArray[next.Y - 1, next.X]) && !tileIsOccupied(next.X, next.Y - 1))
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



        public Player getPlayer()
        {
            return (Player)Game1.creatures[0];
            
                /*
            foreach (Creature c in Game1.creatures)
            {
                if (c is Player)
                {
                    return (Player)c;
                }
            }
            return null;
                 * */
        }

        bool playerInBounds(){
            int px = getPlayer().getX()/32;
            int py = getPlayer().getY()/32;
            
            //Player is in bounds, so is in room
            if(px > boundTopLeft.X && px < boundBottomRight.X && py > boundTopLeft.Y && py < boundBottomRight.Y)
            {
                return true;
            }
            return false;
        }

        public int navigateOutOfLight(int currentTileX, int currentTileY)
        {
            int yTiles = Game1.wallArray.GetLength(0);
            int xTiles = Game1.wallArray.GetLength(1);
            bool[,] visited = new bool[yTiles, xTiles];
            Queue<TileNode> q = new Queue<TileNode>();

            TileNode start = new TileNode(currentTileX, currentTileY);
            //TileNode end = new TileNode(desiredTileX, desiredTileY);

            if (Game1.darkTileArray[currentTileY, currentTileX] == 0)
            {
                return -1;
            }

            //navigate out of the light

            
            q.Enqueue(start);
            visited[start.Y, start.X] = true;

            while (q.Count != 0)
            {
                TileNode next = q.Dequeue();
                if (Game1.darkTileArray[next.Y, next.X] != 0)
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

        bool nextTileIsDark(int currentTileX, int currentTileY, int dir)
        {

            int nextTileX = currentTileX;
            int nextTileY = currentTileY;
            switch(dir)
            {
                case 0:
                    nextTileX --;
                    break;
                case 1:
                    nextTileY --;
                    break;
                case 2:
                    nextTileX++;
                    break;
                case 3:
                    nextTileY++;
                    break;
            }
            if(Game1.darkTileArray[nextTileY, nextTileX] == 0)
            {
                return true;
            }
            return false;
            
        }




    }
}
