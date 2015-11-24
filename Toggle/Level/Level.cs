using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    public class Level
    {

        protected string map = "";
        protected int mapSizeX;
        protected int mapSizeY;
        protected int playerStartingX;
        protected int playerStartingY;
        protected ArrayList levelTiles = new ArrayList();
        protected bool indoors;
        protected ArrayList levelItems;
        protected ArrayList removedItems;
        protected Point playerStartLocation;
        

        public Level()
        {
            indoors = false;
            levelItems = new ArrayList();
            removedItems = new ArrayList();
            addInitialLevelItems();

            
        }


        public void loadLevel()
        {
            makeMapFromFile(map);
            //calcLevelTileGraphics();
            loadLevelObjects();
            loadItemsToGameArray();
            addDarkTiles();
        }

        public void unloadLevel()
        {
            Game1.creatures.Clear();
            Game1.items.Clear();
            Game1.solidTiles.Clear();
            Game1.playerActivateTiles.Clear();
            Game1.tiles.Clear();
            Game1.miscObjects.Clear();
            Game1.levelTiles.Clear();
            Game1.darkTiles.Clear();
            Game1.visuals.Clear();
            Game1.updateMiscObjects.Clear();
            Game1.platforms.Clear();
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
            //string[] lines = System.IO.File.ReadAllLines(@System.IO.Directory.GetCurrentDirectory() + "/Map Files/" + filename);
            
            

            mapSizeX = lines[0].Length * 32;
            mapSizeY = lines.Length * 32;
            Game1.wallArray = new bool[lines.GetLength(0), lines[0].Length];
            Game1.darkTileArray = new double[lines.GetLength(0), lines[0].Length];
            foreach (string line in lines)
            {

                foreach (char c in line)
                {
                    string image = Textures.charToFileName[c];
                    string[] images = image.Split(',');
                    bool solid = (images[2].Length > 0);
                    /*if(c == 'a')
                    {
                        LevelTile t = new LevelTile(xposition, yposition, images[0], images[1], "schoolLevel",new Point(9 * 32, 9 * 32));
                        Game1.levelTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (c == 'b')
                    {
                        LevelTile t = new LevelTile(xposition, yposition, images[0], images[1], "houseLevel", new Point(9 * 32, 9 * 32));
                        Game1.levelTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (c == 'c')
                    {
                        LevelTile t = new LevelTile(xposition, yposition, images[0], images[1], "hubLevel", new Point(9 * 32, 9 * 32));
                        Game1.levelTiles.Add(t);
                        Game1.tiles.Add(t);
                    }*/
                    if (c == 'f')
                    {
                        BadTile t = new BadTile(xposition, yposition, images[0], images[1]);
                        Game1.playerActivateTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (c == 's')
                    {
                        GoodTile t = new GoodTile(xposition, yposition, images[0], images[1]);
                        Game1.playerActivateTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (c == 'l')
                    {
                        LockTile t = new LockTile(xposition, yposition, images[0], images[1]);
                        Game1.playerActivateTiles.Add(t);
                        Game1.tiles.Add(t);
                    } 
                    else if (c == 'u')
                    {
                        UnlockTile t = new UnlockTile(xposition, yposition, images[0], images[1]);
                        Game1.playerActivateTiles.Add(t);
                        Game1.tiles.Add(t);
                    }
                    else if (c == 'g')
                    {
                        Grate t = new Grate(xposition, yposition, images[0], images[1]);
                        Game1.solidTiles.Add(t);
                        Game1.tiles.Add(t);
                        Game1.wallArray[yposition / 32, xposition / 32] = true;
                    }
                    else if (c == 'w')
                    {
                        Wall t = new Wall(xposition, yposition, images[0], images[1]);
                        t.setProjectileBlock(false);
                        Game1.solidTiles.Add(t);
                        Game1.tiles.Add(t);
                        //Game1.wallArray[yposition / 32, xposition / 32] = true;
                    }
                    else if (!solid)
                    {
                        Tile t = new Tile(xposition, yposition, images[0], images[1]);
                        Game1.tiles.Add(t);
                    }
                    else
                    {
                        Wall w = new Wall(xposition, yposition, images[0], images[1]);
                        Game1.solidTiles.Add(w);
                        Game1.tiles.Add(w);
                        Game1.wallArray[yposition / 32, xposition / 32] = true;
                    }
                    xposition += 32;
                }
                xposition = 0;
                yposition += 32;
            }
        }

        public void calcLevelTileGraphics()
        {
            
            int horiWallCount = 0;
            int vertWallCount = 0;
            foreach (Tile w in Game1.solidTiles)
            {
                //help
                if ((w.getX() >= 32 && w.getY() >= 32) && (w.getX() <= ((Game1.wallArray.GetLength(1) * 32) - 64)) && (w.getY() <= ((Game1.wallArray.GetLength(0) * 32) - 64)))
                {
                    Console.Out.Write("HELP");
                    w.setGraphic("WoodenWall");
                    w.setImageRectangle(new Rectangle(0, 0, 0, 0));
                    int y = (w.getY() / 32);
                    int x = (w.getX() / 32);
                    if (Game1.wallArray[y + 1, x] == true)
                        vertWallCount++;
                    if (Game1.wallArray[y - 1, x] == true)
                        vertWallCount++;
                    if (Game1.wallArray[y, x + 1] == true)
                        horiWallCount++;
                    if (Game1.wallArray[y, x - 1] == true)
                        horiWallCount++;
                    Console.Out.Write(horiWallCount);
                    if (vertWallCount >= 1)
                    {
                        w.setGraphic("woodenWallL");
                        if (Game1.wallArray[y, x + 1] == true)
                        {
                            w.setImageRectangle(new Rectangle(32, 0, 32, 32));
                            w.setGraphic("woodenWallL");
                        }
                        else if (Game1.wallArray[y, x - 1] == true)
                        {
                            w.setImageRectangle(new Rectangle(96, 0, 32, 32));
                            w.setGraphic("woodenWallR");
                        }
                    }
                    if (horiWallCount == 2)
                    {
                        w.setGraphic("woodenWallD");
                        if (Game1.wallArray[y + 1, x] == true)
                        {
                            w.setImageRectangle(new Rectangle(0, 0, 32, 32));
                            w.setGraphic("woodenWallU");
                        }
                        else if (Game1.wallArray[y - 1, x] == true)
                        {
                            w.setImageRectangle(new Rectangle(64, 0, 32, 32));
                            w.setGraphic("woodenWallD");
                        }
                    }
                    if ((vertWallCount == 2) && (horiWallCount == 2))
                    {
                        w.setGraphic("blackBlock");
                    }
                    vertWallCount = 0;
                    horiWallCount = 0;
                }
            }
        }

        public virtual void addDarkTiles()
        {



        }

        public ArrayList getLevelTiles()
        {
            return levelTiles;
        }

        public int getMapSizeX(){
            return mapSizeX;
        }
        public int getMapSizeY(){
            return mapSizeY;
        }
        public int getPlayerStartingX()
        {
            return playerStartingX;
        }   
        public int getPlayerStartingY()
        {
            return playerStartingY;
        }
        public Point getPlayerStart()
        {
            return playerStartLocation;
        }
        public void setPlayerStart(Point loc)
        {
            playerStartLocation = loc;
            playerStartingX = loc.X;
            playerStartingY = loc.Y;
        }
        public bool getIndoors()
        {
            return indoors;
        }

        public void loadItemsToGameArray()
        {
            
            foreach(Item i in levelItems)
            {
                Game1.items.Add(i);
            }
        }

        public virtual void addInitialLevelItems()
        {
            
        }

        public void removeItem(Item i)
        {
            levelItems.Remove(i);
            removedItems.Add(i);
        }
        /*
        public void addLevelItem(Item i)
        {
            levelItems.Add(i);
        }*/

        public ArrayList getLevelItems()
        {
            return levelItems;
        }

        public void setLevelItems(ArrayList arr)
        {
            levelItems = arr;
        }

        //For continue
        /*
        public Array getLevelItemNames()
        {
            Array string myArray[] = new Array string [i.]
            foreach(Item i in levelItems)
            {
                i.GetType().Name
            }
        }*/
        //Actually just get the removed items
        public string[] getRemovedLevelItems()
        {
            string[] myArray = new string [removedItems.Count];
            for(int x = 0; x < myArray.Length; x++)
            {
                myArray[x] = removedItems[x].GetType().Name;
                int xL = ((Item)removedItems[x]).getX()/32;
                int yL = ((Item)removedItems[x]).getY()/32;

                myArray[x] += "|" + xL + "|" + yL;
            }
            return myArray;
        }

        /*
        public void removeLevelItems(string[] itemStrings)
        {
            foreach(string s in itemStrings)
            {
                Item i = findItemGivenString(s);
                if (i != null)
                {
                    removedItems.Add(i);
                    for (int x = levelItems.Count - 1; x >= 0; x-- )
                    { 
                        Item iii = (Item)levelItems[x];
                       
                        if (i.GetType().Name.Equals(iii.GetType().Name) && i.getX() == iii.getX() && i.getY() == iii.getY())
                        {
                            levelItems.Remove(iii);
                        }
                    }
                }

            }
        }*/

        public void removeLevelItem(string itemString)
        {
           
            Item i = findItemGivenString(itemString);
            if (i != null)
            {
                removedItems.Add(i);
                for (int x = levelItems.Count - 1; x >= 0; x--)
                {
                    Item iii = (Item)levelItems[x];
                    /*
                    if (iii is Lamp)
                    {
                        int temp = 0;
                    }*/
                    if (i.GetType().Name.Equals(iii.GetType().Name) && i.getX() == iii.getX() && i.getY() == iii.getY())
                    {
                        levelItems.Remove(iii);
                    }
                }
            }

            
        }

        public Item findItemGivenString(string str)
        {
            if (str.Equals("")) return null;

            string[] args = str.Split('|');

            int xlocation = Int32.Parse(args[1]) * 32;
            int ylocation = Int32.Parse(args[2]) * 32;
            foreach(Item i in levelItems)
            {
                if(i.GetType().Name.Equals(args[0]) && i.getX() == xlocation && i.getY() == ylocation)
                {
                    return i;
                }
            }
            //shouldn't happen
            return null;
        }

        public void reloadLevelItems()
        {

            foreach(Item i in removedItems)
            {
                levelItems.Add(i);
            }


            removedItems.Clear();


        }

    }
}
