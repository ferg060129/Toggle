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

        public Gate(int xLocation, int yLocation, int graphic)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["ClosedGate"];
            badGraphic = Textures.textures["ClosedGate"];
            if (graphic == 1)
            {
                goodGraphic = Textures.textures["ghostGate"];
                badGraphic = Textures.textures["ghostGate"];
            }
            collidable = true;
            isSolid = true;
        }

        public override void onButton()
        {
            if (collidable == true)
            {
                for (int i = 0; i < 40; i++)
                {
                    Game1.particles.Add(new Particle(x + 16, y + 16, "particleSpark"));
                }
            }
            goodGraphic = Textures.textures["OpenGate"];
            badGraphic = Textures.textures["OpenGate"];
            collidable = false;
            isSolid = false;
        }
    }
}
