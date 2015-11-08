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
            goodGraphic = Textures.textures["ClosedGate"];
            badGraphic = Textures.textures["ClosedGate"];
            collidable = true;
            isSolid = true;
        }

        public override void onButton()
        {
            goodGraphic = Textures.textures["OpenGate"];
            badGraphic = Textures.textures["OpenGate"];
            collidable = false;
            isSolid = false;
        }
    }
}
