using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Toggle
{
    class DogBoogieman : Creature
    {
        private Point boundTopLeft, boundBottomRight;

        public DogBoogieman(int xLocation, int yLocation, Point bTL, Point bBR)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["sprites"];
            badGraphic = Textures.textures["sprites"];
            row = 1;

           // imageBoundingRectangle = new Rectangle(0, 0, 32, 32);

            width = 32;
            height = 32;
            direction = 0;
            velocity = 8;
            boundTopLeft = bTL;
            boundBottomRight = bBR;
        }


        public override void goodMove()
        {
            if (row == 0) row = 1;
            /*
            if (x % 32 == 0 && y % 32 == 0)
                direction = getNextPathDirection((int)x / 32, (int)y / 32, defendTileGoodX, defendTileGoodY);


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
            }*/
            moving = false;
        }

        public override void badMove()
        {
            if (row == 1) row = 0;

            if (x % 32 == 0 && y % 32 == 0)
            {
                defendTileBadX = (int)(getPlayer().getCenter().X / 32);
                defendTileBadY = (int)(getPlayer().getCenter().Y / 32);

                if (playerInBounds())
                {
                    int temp = navigateOutOfLight((int)x / 32, (int)y / 32);
                    if(temp < 0)
                    {
                        int tempDir = getNextPathDirection((int)x / 32, (int)y / 32, defendTileBadX, defendTileBadY);
                        if (nextTileIsDark((int)x / 32, (int)y / 32, tempDir))
                            direction = tempDir;
                        else
                        {
                            direction = -1;
                        }
                    }
                    else
                    {
                        direction = temp;
                    }
                }
                else
                {
                    direction = -1;
                }
                
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
            if(px >= boundTopLeft.X && px <= boundBottomRight.X && py >= boundTopLeft.Y && py <= boundBottomRight.Y)
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

            Player p = getPlayer();
            bool playerIsObstacle =  Game1.darkTileArray[ p.getY() / 32, p.getX() / 32] != 0;
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
                    if (next.X + 1 < xTiles && !visited[next.Y, next.X + 1] && !Game1.wallArray[next.Y, next.X + 1] && !tileIsOccupied(next.X + 1, next.Y, playerIsObstacle))
                    {
                        TileNode temp = new TileNode(next.X + 1, next.Y, next);
                        visited[next.Y, next.X + 1] = true;
                        q.Enqueue(temp);
                    }
                    if (next.X - 1 >= 0 && !visited[next.Y, next.X - 1] && !Game1.wallArray[next.Y, next.X - 1] && !tileIsOccupied(next.X - 1, next.Y, playerIsObstacle))
                    {
                        TileNode temp = new TileNode(next.X - 1, next.Y, next);
                        visited[next.Y, next.X - 1] = true;
                        q.Enqueue(temp);
                    }
                    if (next.Y + 1 < yTiles && !visited[next.Y + 1, next.X] && !Game1.wallArray[next.Y + 1, next.X] && !tileIsOccupied(next.X, next.Y + 1, playerIsObstacle))
                    {
                        TileNode temp = new TileNode(next.X, next.Y + 1, next);
                        visited[next.Y + 1, next.X] = true;
                        q.Enqueue(temp);
                    }
                    if (next.Y - 1 >= 0 && !visited[next.Y - 1, next.X] && !Game1.wallArray[next.Y - 1, next.X] && !tileIsOccupied(next.X, next.Y - 1, playerIsObstacle))
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

        //Slight modification of creature function. Change both if change either
        public bool tileIsOccupied(int tx, int ty, bool playerIsObstacle)
        {
            Rectangle r = new Rectangle(tx * 32, ty * 32, 32, 32);

            foreach (Creature c in Game1.creatures)
            {
                if (c.getHitBox().Intersects(r) && (!(c is Player) || playerIsObstacle))
                {
                    return true;
                }

            }
            foreach (Miscellanious m in Game1.miscObjects)
            {
                if (m.getHitBox().Intersects(r))
                {
                    if (m is Pushable && m.getState())
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;

        }


    }
}
