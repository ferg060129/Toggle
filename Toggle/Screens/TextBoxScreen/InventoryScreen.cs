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
    class InventoryScreen : TextBoxScreen
    {
        public InventoryScreen(Game1 eng): base(eng)
        {
            buttons.Add(new InventoryScreenButton(eng.GraphicsDevice.Viewport.Width / 2 + 160, 300, "start", "startHover", this));
            textBoxLocation = new Point(150, 100);
            string[] temp = { "Congratulations, you have found an item! All items are stored in your inventory at the top right. You can press the I key to toggle it on and off.", "Hover over your items to learn something about them.", "Maybe you can even combine them to make something better!" };
            
            instructions = temp;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            numInfoBlocks = 3;

        }
    }

}