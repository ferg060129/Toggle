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
    class DiaryExpositionScreen : TextBoxScreen
    {
        public DiaryExpositionScreen(Game1 eng)
            : base(eng)
        {
            textBoxLocation = new Point(150, 100);
            string[] temp = { "My diary...", "September 23, 2003 ...", "Dear Diary, ...", "Today mom told me I had to go to that stupid therapy meeting after school again. If I have to talk to Dr. Boring one more time I'm going to blow my brains out... Or moms brain...", "And dad said if he catches me smoking one more time he won't buy me a car. Are all parents this lame? I want them gone. I hate them." };

            instructions = temp;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            numInfoBlocks = 5;

        }
    }

}