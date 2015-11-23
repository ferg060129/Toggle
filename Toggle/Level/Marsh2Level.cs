using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


//This level is about setting up lasers to press the button in the center
namespace Toggle
{
    class Marsh2Level : Level
    {

        public Marsh2Level()
            : base()
        {
            map = "laserSetup.txt";
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
            Game1.miscObjects.Add(new LaserBlock(3 * 32, 6 * 32,false));
            Game1.miscObjects.Add(new LaserBlock(2 * 32, 7 * 32,true));
            Game1.miscObjects.Add(new LaserBlock(2 * 32, 9 * 32,false));
            
            Game1.miscObjects.Add(new LaserBlock(22 * 32, 6 * 32,false));
            Game1.miscObjects.Add(new LaserBlock(22 * 32, 9 * 32,true));
            Game1.miscObjects.Add(new LaserBlock(14 * 32, 3 * 32, false));
            Game1.miscObjects.Add(new LaserBlock(11 * 32, 1 * 32, true));
            Game1.miscObjects.Add(new LaserBlock(13 * 32, 1 * 32, true));
            Gate gate = new Gate(23 * 32, 13 * 32);
            Game1.miscObjects.Add(gate);
            Button button = new ButtonPlayer(9 * 32, 5 * 32, gate);
            Game1.miscObjects.Add(button);
            
            //Game1.miscObjects.Add(new LaserBlock(32 * 12, 32 * 10,true));
            //Game1.miscObjects.Add(new LaserBlock(32 * 13, 32 * 10,false));
            //Game1.miscObjects.Add(new LaserBlock(32 * 14, 32 * 10,true));

            //next level
            levelTiles.Add(new LevelTile(24 * 32, 13 * 32, "blackBlock", "blackBlock", "marshFinalLevel", new Point(8 * 32, 46 * 32)));
            //previous
            levelTiles.Add(new LevelTile(10 * 32, 14 * 32, "blackBlock", "blackBlock", "marsh1Level", new Point(26 * 32, 23 * 32)));
        }

    }
}
