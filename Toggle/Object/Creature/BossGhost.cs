using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Toggle
{
    class BossGhost : Creature
    {
        private Point boundTopLeft, boundBottomRight;

        int patternState; //0 is idle, 1 is move transition, 2 is attack phase

        public BossGhost(int xLocation, int yLocation, Point bTL, Point bBR)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["ghost"];
            badGraphic = Textures.textures["unghost"];
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);

            direction = 0;
            velocity = 8;
            boundTopLeft = bTL;
            boundBottomRight = bBR;
        }

        public override void move()
        {
            
        }

        public override void onShift()
        {
          
        }

        public override void goodMove()
        {

        }

    }
}
