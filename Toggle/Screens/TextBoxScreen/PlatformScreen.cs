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
    class PlatformScreen : TextBoxScreen
    {
        public PlatformScreen(Game1 eng)
            : base(eng)
        {
            string[] temp = { "You can attempt to drop an item onto a platform by dragging it out of your inventory while standing on the platform.", "Use the I key to toggle the inventory on and off."};

            instructions = temp;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            numInfoBlocks = 2;

        }
    }

}