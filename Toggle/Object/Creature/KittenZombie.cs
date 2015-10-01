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
    class KittenZombie : Creature
    {
        public KittenZombie(int xLocation, int yLocation, bool initialState) : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures["sprites"];
            badGraphic = Textures.textures["sprites"];
            row = 3;
            imageBoundingRectangle = new Rectangle(32*row, 32, 32, 32);
            
            width = 32;
            height = 32;
            direction = 0;
        
        }

        public override void goodMove()
        {
            switch(direction){
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
