using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class HubLevel : Level
    {

        public HubLevel()
            : base()
        {
            map = "hub.txt";
            playerStartingX = 9 * 32;
            playerStartingY = 9 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {

            FlowerTentacles ft = new FlowerTentacles(32 * 11, 32 * 4);
            VineMoveBlock vm = new VineMoveBlock(32 * 7, 32 * 25);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 13, 32 * 19);
            Game1.miscObjects.Add(vm);
            Game1.miscObjects.Add(new VineMoveBlock(32 * 25, 32 * 20));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 20, 32 * 25));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 15, 32 * 20));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 20, 32 * 15));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 34, 32 * 13));
            Game1.creatures.Add(new Ghost(32 * 23, 32 * 3, new Point(1, 1), new Point(54, 5)));


          
            

            House house = new House(6 * 32, 3 * 32);
            Game1.visuals.Add(house);

            School school = new School(30 * 32, 3 * 32);
            Game1.visuals.Add(school);
            Lake lake = new Lake(29 * 32, 22 * 32);
            Game1.visuals.Add(lake);

            /*
            BoxTop boxTop = new BoxTop(20 * 32, 10 * 32);
            Game1.miscObjects.Add(boxTop);
            
            Desk d = new Desk(20 * 32, 11 * 32);
            Game1.miscObjects.Add(d);
            */

            LanternPlatform lp = new LanternPlatform(25 * 32, 30 * 32);
            Game1.platforms.Add(lp);

            KnifePlatform kp = new KnifePlatform(25 * 32, 26 * 32);
            Game1.platforms.Add(kp);


            if (Game1.isBoatSpawned())
            {
                Boat boat = new Boat(28 * 32, 28 * 32);
                Game1.updateMiscObjects.Add(boat);
            }

            /* gb = new GreenBlock(11 * 32, 25 * 32);
           Game1.items.Add(gb);
           gb = new GreenBlock(10 * 32, 25 * 32);
           Game1.items.Add(gb);
           */

            //level tiles


            for (int x = 24; x <= 42; x++)
            {
                if(x != 34)
                Game1.miscObjects.Add(new Fence(32 * x, 32 * 13, "barbedHor"));
            }
            Game1.miscObjects.Add(new Fence(32 * 23, 32 * 13, "barbedBottomLeft"));

            for (int y = 1; y < 13; y++ )
            {
                Game1.miscObjects.Add(new Fence(32 * 23, 32 * y, "barbedVertical1"));
            }


                levelTiles.Add(new LevelTile(19 * 32, 5 * 32, "blackBlock", "blackBlock", "gate1Level", new Point(20 * 32, 10 * 32)));
            //levelTiles.Add(new LevelTile(17 * 32, 3 * 32, "blackBlock", "blackBlock", "schoolLevel",new Point(9 * 32, 10 * 32)));
            levelTiles.Add(new LevelTile(35 * 32, 8 * 32, "blackBlock", "blackBlock", "schoolLevel",new Point(34 * 32, 24 * 32)));
            levelTiles.Add(new LevelTile(9 * 32, 8 * 32, "blackBlock", "blackBlock", "houseLevel", new Point(24 * 32, 16 * 32)));
            levelTiles.Add(new LevelTile(26 * 32, 5 * 32, "blackBlock", "blackBlock", "complex1Level", new Point(22 * 32, 12 * 32)));
        }


        public override void addInitialLevelItems()
        {
            Scroll gb = new Scroll(12 * 32, 25 * 32, "I am a paper", "What are u?");
            levelItems.Add(gb);
        }
    }
}
