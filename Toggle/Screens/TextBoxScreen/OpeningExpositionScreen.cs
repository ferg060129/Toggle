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
    class OpeningExpositionScreen : TextBoxScreen
    {
        public OpeningExpositionScreen(Game1 eng)
            : base(eng)
        {
            string[] temp = { "What... what have I done...", "No... This can't be real, I'm not this kind of person", "I'm so sorry... I'm so sorry...", "Nothing makes sense anymore... nothing...", "Nothi... Zzzzz.... zzzz...", "zzzz....zzzzz....." };

            instructions = temp;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            numInfoBlocks = 6;

        }
    }

}