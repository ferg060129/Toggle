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
            playerStartingX = 13 * 32;
            playerStartingY = 25 * 32;
        }
        public override void loadLevelObjects()
        {
            FlowerTentacles ft = new FlowerTentacles(32 * 11, 32 * 4);
            VineMoveBlock vm = new VineMoveBlock(32 * 7, 32 * 25);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 13, 32 * 19);
            Game1.miscObjects.Add(vm);
            Gate myGate = new Gate(32 * 18, 32 * 21);
            Game1.miscObjects.Add(myGate);
            Button myButton = new Button(32 * 19, 32 * 21,myGate);
            Game1.miscObjects.Add(myButton);
            for (int i = 0; i < 7; i++)
            {
                ft = new FlowerTentacles(32 * 19, (32 * (4 + i)));
                Game1.creatures.Add(ft);
                ft.setDefendTileGood(15 + (i / 3), 4 + i);
                ft.setDefendTileBad(26,4 + i);
            }
        }

    }
}
