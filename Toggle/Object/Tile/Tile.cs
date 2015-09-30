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
        public Tile(int xLocation, int yLocation, bool initialState, String gGraphic, String bGraphic) : base(xLocation, yLocation, initialState)
        {
            goodGraphic = Textures.textures[gGraphic];
            badGraphic = Textures.textures[bGraphic];
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
            hitBox = new Rectangle(xLocation, yLocation, 32, 32);
            width = 32;
            height = 32;
            //charToCollision(fileCharacter);
        }

        /*
        //When loading a map, use chars from it to determine if this tile is collidable
        public void charToCollision(char input)
        {
            bool isWall = false;
            switch (input)
            {
                default:
                    break;
                case '!':
                case '#':
                case '$':
                case '%':
                case '5':
                case '6':
                    isWall = true;
                    break;
            }
            collidable = isWall;
        }
         * */
    }
}
