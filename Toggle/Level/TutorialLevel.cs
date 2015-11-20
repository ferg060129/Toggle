using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Toggle
{
    class TutorialLevel : Level
    {
        public TutorialLevel()
        {
            map = "tutorial.txt";
            indoors = true;
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
