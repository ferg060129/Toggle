using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Level
    {

        protected string map = "";
        protected int mapSizeX;
        protected int mapSizeY;

        public Level()
        {
            
        }


        public void loadLevel()
        {
            makeMapFromFile(map);
            loadLevelObjects();
        }

        public void unloadLevel()
        {
            Game1.creatures.Clear();
            Game1.items.Clear();
            Game1.collidableTiles.Clear();
            Game1.tiles.Clear();
        }

        public virtual void loadLevelObjects()
        {
          
        }

        public void makeMapFromFile(string filename)
        {
            int xposition = 0;
            int yposition = 0;

            //This directory navigation might have to change for the final product, or even sooner
            string[] lines = System.IO.File.ReadAllLines(@"../../../Map Files/" + filename);
            mapSizeX = lines[0].Length * 32;
            mapSizeY = lines.Length * 32;
            Game1.wallArray = new bool[lines.GetLength(0), lines[0].Length];
            foreach (string line in lines)
            {

                foreach (char c in line)
                {
                    string image = Textures.charToFileName[c];
                    string[] images = image.Split(',');
                    bool solid = (images[2].Length > 0);
                    if (c == 'f')
                    {
                        BadTile t = new BadTile(xposition, yposition, images[0], images[1]);
                        Game1.collidableTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (c == 's')
                    {
                        GoodTile t = new GoodTile(xposition, yposition, images[0], images[1]);
                        Game1.collidableTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (c == 'l')
                    {
                        LockTile t = new LockTile(xposition, yposition, images[0], images[1]);
                        Game1.collidableTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (c == 'u')
                    {
                        UnlockTile t = new UnlockTile(xposition, yposition, images[0], images[1]);
                        Game1.collidableTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (!solid)
                    {
                        Tile t = new Tile(xposition, yposition, images[0], images[1]);
                        Game1.tiles.Add(t);
                    }
                    else
                    {
                        Wall w = new Wall(xposition, yposition, images[0], images[1]);
                        Game1.collidableTiles.Add(w);
                        Game1.tiles.Add(w);
                        Game1.wallArray[yposition / 32, xposition / 32] = true;
                    }
                    xposition += 32;
                }
                xposition = 0;
                yposition += 32;
            }
           
            
        }

        public int getMapSizeX(){
            return mapSizeX;
        }
        public int getMapSizeY(){
            return mapSizeY;
        }
        
     

    }
}
