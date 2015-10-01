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
    //small change
    class FlowerTentacles : Creature
    {
        int counter = 0;
        public FlowerTentacles(int xLocation, int yLocation, bool initialState)
            : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures["sprites"];
            badGraphic = Textures.textures["sprites"];
            row = 3;
            imageBoundingRectangle = new Rectangle(32 * row, 32, 32, 32);
            
            width = 32;
            height = 32;
            direction = 0;
        }

        public override void goodMove()
        {
            if (row == 2) row = 3;

            if(x%32 ==0 && y % 32 == 0)
            direction = getNextPathDirection((int)x/32,(int)y/32,defendTileGoodX,defendTileGoodY);


            switch (direction)
            {
                case 0:
                    x -= velocity;
                    break;
                case 1:
                    y -= velocity;
                    break;
                case 2:
                    x += velocity;
                    break;
                case 3:
                    y += velocity;
                    break;
                default:
                    moving = false;
                    break;
            }
        }

        public override void badMove()
        {
            if (row == 3) row = 2;

            if (x % 32 == 0 && y % 32 == 0)
                direction = getNextPathDirection((int)x / 32, (int)y / 32, defendTileBadX, defendTileBadY);

            switch (direction)
            {
                case 0:
                    x -= velocity;
                    break;
                case 1:
                    y -= velocity;
                    break;
                case 2:
                    x += velocity;
                    break;
                case 3:
                    y += velocity;
                    break;
                default:
                    moving = false;
                    break;
            }
        }
    }
}
