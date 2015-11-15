using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Toggle
{
    class DanceScreen
    {
        Tile[,] tiles;
        Game1 engine;
        Random rand = new Random();
        int ctr = 0;
        ArrayList points = new ArrayList();
        int screenWidth;
        int screenHeight;


        public DanceScreen(Game1 eng)
        {
            engine = eng;
            screenWidth = engine.getScreenWidth();
            screenHeight = engine.getScreenWidth();

            if(screenWidth < 800)
            {
                screenWidth = 800;
            }
            tiles = new Tile[screenWidth / 32, screenHeight / 32];
            for (int x = 0; x < tiles.GetLength(0); x++ )
            {
                for(int y = 0; y < tiles.GetLength(1); y++ )
                {
                    
                    int temp = rand.Next(3);
                    tiles[x,y] = new Tile(temp,x,y);
                }
            }
            makePoints();
            addPointOffset();
        }

        public void makePoints()
        {
            //int spaceSize = 1;
            int xOffset = 0;
            int yOffset = 0;
            int previousHeight = 0;

            points.Add(new Point(0,0));
             points.Add(new Point(0,1));
             points.Add(new Point(0,2));
             points.Add(new Point(0,3));
             points.Add(new Point(0,4));
             points.Add(new Point(1,0));
             points.Add(new Point(2,0));
             points.Add(new Point(3,1));
             points.Add(new Point(3,2));
             points.Add(new Point(3,3));
             points.Add(new Point(1,4));
            points.Add(new Point(2,4));

            int previousLength = 5;

            points.Add(new Point(previousLength, 0));
            points.Add(new Point(1 + previousLength, 0));
            points.Add(new Point(2 + previousLength, 0));
            points.Add(new Point(previousLength, 1));
            points.Add(new Point(previousLength, 2));
            points.Add(new Point(previousLength, 3));
            points.Add(new Point(previousLength, 4));
            points.Add(new Point(1 + previousLength, 2));
            points.Add(new Point(2 + previousLength, 2));
            points.Add(new Point(3 + previousLength, 2));
            points.Add(new Point(3 + previousLength, 1));
            points.Add(new Point(2 + previousLength, 3));
            points.Add(new Point(3 + previousLength, 4));

            previousLength += 5;

            points.Add(new Point(previousLength, 0));
            points.Add(new Point(previousLength + 1, 0));
            points.Add(new Point(previousLength + 2, 0));
            points.Add(new Point(previousLength + 3, 0));
            points.Add(new Point(previousLength + 0, 1));
            points.Add(new Point(previousLength + 0, 2));
            points.Add(new Point(previousLength + 1, 2));
            points.Add(new Point(previousLength + 2, 2));
            points.Add(new Point(previousLength + 0, 3));
            points.Add(new Point(previousLength + 0, 4));
            points.Add(new Point(previousLength + 1, 4));
            points.Add(new Point(previousLength + 2, 4));
            points.Add(new Point(previousLength + 3, 4));

            previousLength += 5;

            points.Add(new Point(previousLength + 1, 0));
            points.Add(new Point(previousLength + 2, 0));
            points.Add(new Point(previousLength + 0, 1));
            points.Add(new Point(previousLength + 0, 2));
            points.Add(new Point(previousLength + 0, 3));
            points.Add(new Point(previousLength + 0, 4));
            points.Add(new Point(previousLength + 1, 2));
            points.Add(new Point(previousLength + 2, 2));
            points.Add(new Point(previousLength + 3, 3));
            points.Add(new Point(previousLength + 3, 1));
            points.Add(new Point(previousLength + 3, 2));
            points.Add(new Point(previousLength + 3, 3));
            points.Add(new Point(previousLength + 3, 4));

            previousLength += 5;

            points.Add(new Point(previousLength + 1, 0));
            points.Add(new Point(previousLength + 2, 0));
            points.Add(new Point(previousLength + 3, 0));
            points.Add(new Point(previousLength + 0, 1));
            points.Add(new Point(previousLength + 0, 2));
            points.Add(new Point(previousLength + 0, 3));
            points.Add(new Point(previousLength + 0, 4));
            points.Add(new Point(previousLength + 2, 1));
            points.Add(new Point(previousLength + 2, 2));
            points.Add(new Point(previousLength + 2, 3));
            points.Add(new Point(previousLength + 2, 4));
            points.Add(new Point(previousLength + 4, 1));
            points.Add(new Point(previousLength + 4, 2));
            points.Add(new Point(previousLength + 4, 3));
            points.Add(new Point(previousLength + 4, 4));


            previousHeight +=6;
            previousLength = 2;

            points.Add(new Point(previousLength, 0 + previousHeight));
            points.Add(new Point(1 + previousLength, 0 + previousHeight));
            points.Add(new Point(2 + previousLength, 0 + previousHeight));
            points.Add(new Point(3 + previousLength, 0 + previousHeight));
            points.Add(new Point(previousLength, 1 + previousHeight));
            points.Add(new Point(previousLength, 2 + previousHeight));
            points.Add(new Point(1 + previousLength, 2 + previousHeight));
            points.Add(new Point(2 + previousLength, 2 + previousHeight));
            points.Add(new Point(3 + previousLength, 2 + previousHeight));
            points.Add(new Point(3 + previousLength, 3 + previousHeight));
            points.Add(new Point(3 + previousLength, 4 + previousHeight));
            points.Add(new Point(previousLength, 4 + previousHeight));
            points.Add(new Point(1 + previousLength, 4 + previousHeight));
            points.Add(new Point(2 + previousLength, 4 + previousHeight));

            previousLength += 5;

            points.Add(new Point(previousLength, 0 + previousHeight));
            points.Add(new Point(previousLength, 1 + previousHeight));
            points.Add(new Point(previousLength, 2 + previousHeight));
            points.Add(new Point(previousLength, 3 + previousHeight));
            points.Add(new Point(previousLength, 4 + previousHeight));
            points.Add(new Point(previousLength + 1, 2 + previousHeight));
            points.Add(new Point(previousLength + 2, 2 + previousHeight));
            points.Add(new Point(previousLength + 3, 0 + previousHeight));
            points.Add(new Point(previousLength + 3, 1 + previousHeight));
            points.Add(new Point(previousLength + 3, 2 + previousHeight));
            points.Add(new Point(previousLength + 3, 3 + previousHeight));
            points.Add(new Point(previousLength + 3, 4 + previousHeight));

            previousLength += 5;

            points.Add(new Point(previousLength, previousHeight));
            points.Add(new Point(previousLength + 1, previousHeight));
            points.Add(new Point(previousLength + 2, previousHeight));
            points.Add(new Point(previousLength + 1, previousHeight));
            points.Add(new Point(previousLength + 1, previousHeight + 1));
            points.Add(new Point(previousLength + 1, previousHeight + 2));
            points.Add(new Point(previousLength + 1, previousHeight + 3));
            points.Add(new Point(previousLength, previousHeight + 4));
            points.Add(new Point(previousLength + 1, previousHeight + 4));
            points.Add(new Point(previousLength + 2, previousHeight + 4));

            previousLength += 4;

            points.Add(new Point(previousLength, previousHeight));
            points.Add(new Point(previousLength + 1, previousHeight));
            points.Add(new Point(previousLength + 2, previousHeight));
            points.Add(new Point(previousLength + 0, 1 + previousHeight));
            points.Add(new Point(previousLength + 0, 2 + previousHeight));
            points.Add(new Point(previousLength + 0, 3 + previousHeight));
            points.Add(new Point(previousLength + 0, 4 + previousHeight));
            points.Add(new Point(previousLength + 1, 2 + previousHeight));

            previousLength += 4;

            points.Add(new Point(previousLength, previousHeight));
            points.Add(new Point(previousLength + 1, previousHeight));
            points.Add(new Point(previousLength + 2, previousHeight));
            points.Add(new Point(previousLength + 1, previousHeight));
            points.Add(new Point(previousLength + 1, previousHeight + 1));
            points.Add(new Point(previousLength + 1, previousHeight + 2));
            points.Add(new Point(previousLength + 1, previousHeight + 3));
            points.Add(new Point(previousLength + 1, previousHeight + 4));

        }

        public void changeColors(){
            

            if(ctr == 0)
            {
                foreach(Point p in points)
                {
                    int temp = rand.Next(2);
                    tiles[p.X, p.Y].setColor(temp);
                }
            }
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (!containsPoint(x,y))
                    {
                        int temp = rand.Next(2) + 2;
                        tiles[x, y] = new Tile(temp, x, y);
                    }
                    
                }
            }


        }




        public void drawDanceSreen(SpriteBatch sb)
        {

            changeColors();

            foreach(Tile t in tiles)
            {

                sb.Draw(Textures.textures["blocks"], new Vector2(t.getX() * 32, t.getY() * 32), new Rectangle(t.getColor() * 32, 0, t.getColor() * 32 + 32, 32), Color.White);
                /*if(ctr == 0)
                {
                    sb.Draw(Textures.textures["blocks"], new Vector2(t.getX() * 32, t.getY() * 32), new Rectangle(t.getColor() * 32, 0, t.getColor() * 32 + 32, 32), Color.White);
                }*/

            }
            if(ctr == 0)
            {
                ctr = 5;
            }
            else{
                ctr--;
            }
        }

        public bool containsPoint(int x, int y)
        {
            foreach (Point p in points)
            {
                if (p.X == x && p.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        public void addPointOffset()
        {
            int cx = (int)(engine.getScreenWidth() / 64);
            int cy = (int)(engine.getScreenHeight()/64);



            for (int x = 0; x < points.Count; x ++)
            {
                int tempX = ((Point)(points[0])).X + cx - 12;
                int tempY = ((Point)(points[0])).Y + cy - 5;

                points.RemoveAt(0);
                points.Add(new Point(tempX, tempY));


            }
        }


        class Tile{
            int color,x,y;
            public Tile(int color,int x,int y)
            {
                this.color = color;
                this.x = x;
                this.y = y;
            }

            public void setColor(int c)
            {
                color = c;
            }

            public int getX()
            {
                return x;
            }

            public int getY()
            {
                return y;
            }

            public int getColor()
            {
                return color;
            }



        }


    }


    
}
