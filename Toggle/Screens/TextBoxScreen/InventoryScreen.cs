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
            string[] temp = { "We the people of the United States in order to form a more perfect union, establish justice, ensure domestic tranquility, provide for the common defense, promote the general welfare, and secure the blessings of liberty to ourselves and our posterity, do ordain and establish this constitution for the United States of America.", "Kevin you're a pretty cool dude c:", "the context is aqui" };
            
            instructions = temp;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            numInfoBlocks = 3;

        }
    }

}