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
        }
        public override void loadLevelObjects()
        {

            Gate myGate = new Gate(12 * 32, 2 * 32);
            Game1.miscObjects.Add(myGate);

            FlowerTentacles ft = new FlowerTentacles(32 * 11, 32 * 4);
            VineMoveBlock vm = new VineMoveBlock(32 * 7, 32 * 25);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 13, 32 * 19);
            Game1.miscObjects.Add(vm);
            Game1.miscObjects.Add(new VineMoveBlock(32 * 25, 32 * 20));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 20, 32 * 25));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 15, 32 * 20));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 20, 32 * 15));
            Game1.creatures.Add(new Ghost(32 * 23, 32 * 3, new Point(1, 1), new Point(54, 5)));


          
            

            House house = new House(6 * 32, 3 * 32);
            Game1.visuals.Add(house);

            School school = new School(30 * 32, 3 * 32);
            Game1.visuals.Add(school);
            Lake lake = new Lake(29 * 32, 22 * 32);
            Game1.visuals.Add(lake);

            Boat boat = new Boat(28 * 32, 28 * 32);
            Game1.updateMiscObjects.Add(boat);

            ChalkboardTop ch = new ChalkboardTop(10 * 32, 0*32, myGate);
            Game1.miscObjects.Add(ch);

            BoxTop boxTop = new BoxTop(20 * 32, 10 * 32);
            Game1.miscObjects.Add(boxTop);
            /* gb = new GreenBlock(11 * 32, 25 * 32);
           Game1.items.Add(gb);
           gb = new GreenBlock(10 * 32, 25 * 32);
           Game1.items.Add(gb);
           */

            //level tiles
            levelTiles.Add(new LevelTile(19 * 32, 5 * 32, "blackBlock", "blackBlock", "gate1Level"));
            levelTiles.Add(new LevelTile(17 * 32, 3 * 32, "blackBlock", "blackBlock", "ghostTestLevel"));
            levelTiles.Add(new LevelTile(21 * 32, 3 * 32, "blackBlock", "blackBlock", "laserTestLevel"));
            levelTiles.Add(new LevelTile(35 * 32, 8 * 32, "blackBlock", "blackBlock", "gate1Level"));
        }


        public override void addInitialLevelItems()
        {
            GreenBlock gb = new GreenBlock(12 * 32, 25 * 32);
            levelItems.Add(gb);
        }
    }
}
