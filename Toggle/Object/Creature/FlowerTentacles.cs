using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Toggle
{
    class FlowerTentacles : Creature
    {
        public FlowerTentacles(int xLocation, int yLocation, bool initialState)
            : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures["kitten"];
            badGraphic = Textures.textures["zombie"];
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
            width = 32;
            height = 32;
        }

        public override void goodMove()
        {
            y -= velocity;
        }

        public override void badMove()
        {
            y += velocity;
        }
    }
}
