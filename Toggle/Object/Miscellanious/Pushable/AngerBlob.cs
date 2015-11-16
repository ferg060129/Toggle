using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class AngerBlob : Miscellanious
    {
        bool awake;
        public AngerBlob(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["goodBlob"];
            badGraphic = Textures.textures["badBlob"];
            isSolid = true;
            collidable = true;
            //isPushable = Game1.worldState;
            awake = false;
            imageBoundingRectangle = new Rectangle(32, 0, 32, 32);
        }

        public override void onShift()
        {
            base.onShift();
            //isPushable = Game1.worldState;
            //when going to good world, fall back asleep
            if (Game1.worldState)
            {
                imageBoundingRectangle = new Rectangle(32, 0, 32, 32);
                awake = false;
            }
        }

        public override void onUpdate()
        {
            base.onUpdate();
            foreach (Creature c in Game1.creatures)
            {
                if (c is Player)
                {
                    if (Function.distanceTo(this,c) <= 96)
                    {
                        if (Game1.worldState)
                        {
                            awake = true;
                            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
                        }
                        else if (awake == true)
                        {
                            ((Player)c).damageProportion(.02, 10);
                        }
                    }
                }
            }
            
        }
    }
}
