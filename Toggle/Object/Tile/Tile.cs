using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Collections;
>>>>>>> origin/master
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
<<<<<<< HEAD

=======
>>>>>>> origin/master
namespace Toggle
{
    class Tile : Object
    {
<<<<<<< HEAD

        public Tile(int xLocation, int yLocation, bool initialState) : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures["BigPicture"];
            badGraphic = Textures.textures["BigPicture"];
            width = 32;
            height = 32;
            imageBoundingRectangle = new Rectangle(64, 0, width, height);
            hitBox = new Rectangle(xLocation, yLocation, width, height);

        }

=======
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



>>>>>>> origin/master
    }
}
