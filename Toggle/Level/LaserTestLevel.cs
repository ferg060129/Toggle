using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class LaserTestLevel : Level
    {

        public LaserTestLevel()
            : base()
        {
            map = "laser.txt";
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
            Game1.miscObjects.Add(new LaserBlock(32 * 10, 32 * 10));
            Game1.miscObjects.Add(new LaserBlock(32 * 11, 32 * 10,false));
            Game1.miscObjects.Add(new LaserBlock(32 * 12, 32 * 10,true));
            Game1.miscObjects.Add(new LaserBlock(32 * 13, 32 * 10,false));
            Game1.miscObjects.Add(new LaserBlock(32 * 14, 32 * 10,true));
            for (int i = 0; i < 20; i++ )
            {
                if (i % 4 == 0)
                {
                    Game1.miscObjects.Add(new LaserBlock(32 * (20 + i), 32 * (1 + i), true));
                    Game1.miscObjects.Add(new VineMoveBlock(32 * (18 + i), 32 * (1 + i)));
                } 
                else if ((i + 3) % 4 == 0)
                {
                    Game1.miscObjects.Add(new LaserBlock(32 * (20 + i), 32 * (1 + i), false));
                }
            }
            Game1.miscObjects.Add(new VineMoveBlock(32 * 15, 32 * 10));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 10, 32 * 15));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 5, 32 * 10));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 10, 32 * 5));

            levelTiles.Add(new LevelTile(9 * 32, 5 * 32, "blackBlock", "blackBlock", "hubLevel"));
        }

    }
}
