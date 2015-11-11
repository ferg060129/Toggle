using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class LaserIntro : Level
    {
        public LaserIntro()
            : base()
        {
            map = "laserIntro.txt";
            playerStartingX = 12 * 32;
            playerStartingY = 10 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {
            Gate gate1 = new Gate(4 * 32, 5 * 32);
            Gate gate2 = new Gate(4 * 32, 6 * 32);
            Button btn1 = new Button(10 * 32, 8 * 32, gate2);
            Button btn2 = new Button(4 * 32, 7 * 32, gate1);
            btn2.setHeavyButton(true);
            Game1.miscObjects.Add(btn1);
            Game1.miscObjects.Add(btn2);
            Game1.miscObjects.Add(gate1);
            Game1.miscObjects.Add(gate2);
            Game1.miscObjects.Add(new LaserBlock(15 * 32, 13 * 32, false));
            Game1.miscObjects.Add(new LaserBlock(4 * 32, 8 * 32, true));
            Game1.miscObjects.Add(new Strawberry(13 * 32, 10 * 32));
            //level tiles
            levelTiles.Add(new LevelTile(7 * 32, 3 * 32, "blackBlock", "blackBlock", "gate2Level",new Point(12 * 32, 10 * 32)));
            levelTiles.Add(new LevelTile(27 * 32, 25 * 32, "blackBlock", "blackBlock", "gate1Level", new Point(2 * 32, 8 * 32)));
        }
    }
}
