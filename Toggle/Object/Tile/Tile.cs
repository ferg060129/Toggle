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
        bool wall;
        public Tile(int xLocation, int yLocation, bool initialState, String gGraphic, String bGraphic, bool solid) : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures[gGraphic];
            badGraphic = Textures.textures[bGraphic];
            wall = solid;
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
            width = 32;
            height = 32;
        }

        public bool isWall()
        {
            return wall;
        }



    }
}
