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
    class ShiftLockScreen : TextBoxScreen
    {
        public ShiftLockScreen(Game1 eng)
            : base(eng)
        {
            string[] temp = { "The tile you have just stepped on has locked your ability to Shift! Notice the lock symbol on your Shift cooldown bar to the upper left.","Find a key tile to unlock it, so you may Shift once again!"};

            instructions = temp;
            currentInfoText = adjustTextForWrap(instructions[infoIndex], Textures.fonts["arial12"]);
            numInfoBlocks = 2;

        }
    }

}