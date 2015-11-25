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
    class KnifeExpositionScreen : TextBoxScreen
    {
        public KnifeExpositionScreen(Game1 eng)
            : base(eng)
        {
            string[] temp = { "October 9, 2003...", "Dear Diary...", "My dad shoved me into the wall today after I told my mom I hated her and that she doesn't get me.", "He shouldn't have done that.", "He's going to tell me he's sorry...", "I stole this knife from the art room at school today. No one will know it's missing." };

            instructions = temp;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            numInfoBlocks = 6;

        }
    }

}