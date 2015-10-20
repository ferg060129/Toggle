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
            VineMoveBlock vm = new VineMoveBlock(32 * 7, 32 * 25);
            Game1.miscObjects.Add(vm);
            vm = new VineMoveBlock(32 * 13, 32 * 19);
            Game1.miscObjects.Add(vm);
        }
    }
}
