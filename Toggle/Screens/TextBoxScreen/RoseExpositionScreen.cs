using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Collections;

namespace Toggle
{
    class RoseExpositionScreen : TextBoxScreen
    {
        public RoseExpositionScreen(Game1 eng)
            : base(eng)
        {
            string[] temp = { "Mom....... ?", "Dad.......?", "I take it back...", "Please come back...", "PLEASE............", "I miss you....", ".........", "This rose......", "It's beautiful.....", "Life is beautiful...", "I love you mom and dad.... I'm so sorry..." };

            instructions = temp;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            numInfoBlocks = 11;

        }
    }

}