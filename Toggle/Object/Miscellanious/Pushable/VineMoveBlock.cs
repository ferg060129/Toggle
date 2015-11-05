using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class VineMoveBlock : Pushable
    {
        public VineMoveBlock(int xLocation, int yLocation) : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["BoxLight"];
            badGraphic = Textures.textures["BoxDark"];
            isPushable = Game1.worldState;
        }

        public override void onShift()
        {
            base.onShift();
            isPushable = Game1.worldState;
        }
    }
}
