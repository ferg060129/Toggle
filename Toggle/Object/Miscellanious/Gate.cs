using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Gate : Miscellanious
    {
        public Gate(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["gateClose"];
            badGraphic = Textures.textures["gateClose"];
            collidable = true;
        }

        public override void onButton()
        {
            goodGraphic = Textures.textures["gateOpen"];
            badGraphic = Textures.textures["gateOpen"];
            collidable = false;
        }
    }
}
