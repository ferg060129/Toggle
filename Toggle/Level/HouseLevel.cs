using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class HouseLevel : Level
    {

        public HouseLevel() : base()
        {
            map = "home.txt";
            playerStartingX = 5 * 32;
            playerStartingY = 3 * 32;
        }


        public override void loadLevelObjects()
        {
            FlowerTentacles ft = new FlowerTentacles(32 * 11, 32 * 4);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(11, 4);
            ft.setDefendTileBad(11, 7);

            ft = new FlowerTentacles(32 * 12, 32 * 4);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(12, 4);
            ft.setDefendTileBad(12, 6);

            ft = new FlowerTentacles(32 * 12, 32 * 11);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(12, 11);
            ft.setDefendTileBad(13, 7);

            ft = new FlowerTentacles(32 * 11, 32 * 11);
            Game1.creatures.Add(ft);
            ft.setDefendTileGood(11, 11);
            ft.setDefendTileBad(12, 8);
            
            //ft = new FlowerTentacles(500, 400, worldState);
            //creatures.Add(ft);

            GreenBlock b = new GreenBlock(32 * 12, 32 * 7);
            Game1.items.Add(b);
            VineMoveBlock vm = new VineMoveBlock(32 * 17, 32 * 8);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 18, 32 * 8);
            Game1.miscObjects.Add(vm);
        }
    }
}
