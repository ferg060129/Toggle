using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class TutorialLevel : Level
    {
        DiaryPlatform dp = null;
        KnifePlatform kp = null;
        public TutorialLevel()
            : base()
        {
            map = "tutorial.txt";
            playerStartingX = 5 * 32;
            playerStartingY = 5 * 32;
            playerStartLocation = new Point(playerStartingX, playerStartingY);
        }
        public override void loadLevelObjects()
        {

        }


        public override void addInitialLevelItems()
        {

        }
    }
}
