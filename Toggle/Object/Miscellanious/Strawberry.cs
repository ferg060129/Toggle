using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Strawberry : Miscellanious
    {
        public Strawberry(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["berry"];
            badGraphic = Textures.textures["berryRot"];
            collidable = Game1.worldState;

        }

        public override void onShift()
        {
            collidable = Game1.worldState;
        }
    }
}
