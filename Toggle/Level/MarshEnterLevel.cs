using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class MarshEnterLevel : Level
    {

        public MarshEnterLevel()
            : base()
        {
            map = "marshEnter.txt";
            playerStartingX = 9 * 32;
            playerStartingY = 9 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {
            Gate theGate = new Gate(40 * 32, 3 * 32,1);
            Game1.miscObjects.Add(theGate);
            ButtonShadow shadow = new ButtonShadow(38 * 32, 6 * 32,theGate,true);
            Torch tempTor;
            tempTor = new Torch(36 * 32, 5 * 32, false);
            shadow.addTorchQueue(tempTor);
            Game1.miscObjects.Add(tempTor);
            shadow.addPressPoint(37, 5);
            tempTor = new Torch(41 * 32, 8 * 32, false);
            shadow.addTorchQueue(tempTor);
            Game1.miscObjects.Add(tempTor);
            shadow.addPressPoint(40, 8);
            tempTor = new Torch(36 * 32, 8 * 32, false);
            shadow.addTorchQueue(tempTor);
            Game1.miscObjects.Add(tempTor);
            shadow.addPressPoint(37, 8);
            tempTor = new Torch(41 * 32, 5 * 32, false);
            shadow.addTorchQueue(tempTor);
            Game1.miscObjects.Add(tempTor);
            shadow.addPressPoint(40, 5);
            //place torches and link them to shadow queue
            Game1.miscObjects.Add(shadow);
            //decor
            Game1.miscObjects.Add(new Torch(5 * 32, 12 * 32, true));
            Game1.miscObjects.Add(new Torch(21 * 32, 5 * 32,true));
            Game1.miscObjects.Add(new Torch(47 * 32, 6 * 32,true));
            Game1.miscObjects.Add(new Torch(28 * 32, 12 * 32,true));
            //next level
            levelTiles.Add(new LevelTile(40 * 32, 2 * 32, "blackBlock", "blackBlock", "marsh1Level", new Point(2 * 32, 35 * 32)));
            //previous level
            levelTiles.Add(new LevelTile(2 * 32, 9 * 32, "blackBlock", "blackBlock", "hubLevel", new Point(35 * 32, 20 * 32)));
        }

    }
}
