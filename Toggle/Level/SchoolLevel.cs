using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class SchoolLevel : Level
    {

        public SchoolLevel()
            : base()
        {
            map = "laser.txt";
            playerStartingX = 15*32;
            playerStartingY = 16*32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
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

            VineMoveBlock vm = new VineMoveBlock(32 * 10, 32 * 5);
            Game1.miscObjects.Add(vm);

            
        }

        public override void addInitialLevelItems(){
            GreenBlock b = new GreenBlock(32 * 12, 32 * 7);
            levelItems.Add(b);
        }
    }
}
