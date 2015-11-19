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
            if ((x % 32 == 0 && y % 32 == 0) && Function.distanceTo(this,getPlayer()) > 96)
            {
                defendTileGoodX = (int)(getPlayer().getCenter().X / 32);
                defendTileGoodY = (int)(getPlayer().getCenter().Y / 32);
                direction = getNextPathDirection((int)x / 32, (int)y / 32, defendTileGoodX, defendTileGoodY);
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
                    moving = false;
                    break;
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
