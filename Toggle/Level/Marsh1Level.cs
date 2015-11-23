using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class Marsh1Level : Level
    {

        public Marsh1Level()
            : base()
        {
            map = "marsh1.txt";
            playerStartingX = 9 * 32;
            playerStartingY = 9 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {
            Game1.miscObjects.Add(new VineMoveBlock(32 * 15, 32 * 10));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 10, 32 * 15));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 5, 32 * 10));
            Game1.miscObjects.Add(new VineMoveBlock(32 * 10, 32 * 5));
            Game1.creatures.Add(new Ghost(32 * 13, 32 * 3, new Point(1, 1), new Point(49 * 32, 51 * 32)));
            Game1.creatures.Add(new Ghost(32 * 33, 32 * 31, new Point(1, 1), new Point(49 * 32, 51 * 32)));
            Game1.creatures.Add(new Ghost(32 * 13, 32 * 43, new Point(1, 1), new Point(49 * 32, 51 * 32)));
            Gate theGate = new Gate(26 * 32, 17 * 32,1);
            Game1.miscObjects.Add(theGate);
            ButtonShadow shadow = new ButtonShadow(4 * 32, 33 * 32,theGate,true);
            shadow.addPressPoint(8, 31);          
            shadow.addPressPoint(39, 35);          
            shadow.addPressPoint(35, 22);       
            shadow.addPressPoint(21, 17);    
            shadow.addPressPoint(36, 45);
            shadow.addPressPoint(26, 19);
            //place torches and link them to shadow queue
            for (int i = 0; i < 6; i++)
            {
                Torch tempTor;
                if (i % 2 == 0)
                    tempTor = new Torch((23 + i/2) * 32, 17 * 32,false);
                else
                    tempTor = new Torch((29 - i/2) * 32, 17 * 32, false);
                shadow.addTorchQueue(tempTor);
                Game1.miscObjects.Add(tempTor);
            }
            Game1.miscObjects.Add(shadow);
            

                levelTiles.Add(new LevelTile(1 * 32, 33 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(35 * 32, 20 * 32)));
        }

    }
}
