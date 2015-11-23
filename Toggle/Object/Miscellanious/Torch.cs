using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class Torch : Miscellanious
    {
        bool isLit;
        int frameDelay = 10;
        int currentDelay = 0;
        int frame = 0;
        public Torch(int xLocation, int yLocation,bool initLit)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["torchGood"];
            badGraphic = Textures.textures["torchBad"];
            imageBoundingRectangle = new Rectangle(0, 32, 32, 32);
            isLit = initLit;
            currentDelay = Game1.random.Next(9);
        }
        public override void onUpdate()
        {
            base.onUpdate();
            if (isLit)
            {
                currentDelay++;
                if (currentDelay > frameDelay)
                {
                    frame++;
                    if (frame == 2)
                        frame = 0;
                    currentDelay = 0;
                }
                imageBoundingRectangle = new Rectangle(frame * 32, 0, 32, 32);
            }
        }

        public override void onButton()
        {
            isLit = true;
        }
    }
}
