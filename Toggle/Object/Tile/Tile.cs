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
    class Tile : Object
    {
        bool wall, pressed;
        public Tile(int xLocation, int yLocation, String gGraphic, String bGraphic)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures[gGraphic];
            badGraphic = Textures.textures[bGraphic];
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
            hitBox = new Rectangle(xLocation, yLocation, 32, 32);
            width = 32;
            height = 32;
            //charToCollision(fileCharacter);
        }
    }
}
