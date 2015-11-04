using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            FlowerTentacles ft = new FlowerTentacles(32 * 11, 32 * 4);
            VineMoveBlock vm = new VineMoveBlock(32 * 7, 32 * 25);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 13, 32 * 19);
            Game1.miscObjects.Add(vm);
            LaserBlock ls = new LaserBlock(32 * 5, 32 * 5);
            Game1.miscObjects.Add(ls);


          
            GreenBlock gb = new GreenBlock(12 * 32, 25 * 32);
            Game1.items.Add(gb);
            /* gb = new GreenBlock(11 * 32, 25 * 32);
           Game1.items.Add(gb);
           gb = new GreenBlock(10 * 32, 25 * 32);
           Game1.items.Add(gb);
           */

            //level tiles
            levelTiles.Add(new LevelTile(19 * 32, 5 * 32, "blackBlock", "blackBlock", "gate1Level"));
        }

    }
}
