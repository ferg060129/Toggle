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
    public class Tile : Object
    {
        bool wall, pressed;
        public Tile(int xLocation, int yLocation, String gGraphic, String bGraphic)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures[gGraphic];
            badGraphic = Textures.textures[bGraphic];
            //if tile is grass, have a chance to give it random variation
            if (gGraphic == "grass5")
            {
                switch (Game1.random.Next(0,10))
                {
                    default:
                        break;
                    case 7:
                        goodGraphic = Textures.textures["grass6"];
                        badGraphic = Textures.textures["darkgrass6"];
                        break;
                    case 8:
                        goodGraphic = Textures.textures["grass7"];
                        badGraphic = Textures.textures["darkgrass7"];
                        break;
                    case 9:
                        goodGraphic = Textures.textures["grass8"];
                        badGraphic = Textures.textures["darkgrass8"];
                        break;
                }
            }
           
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
            hitBox = new Rectangle(xLocation, yLocation, 32, 32);
            width = 32;
            height = 32;
            //charToCollision(fileCharacter);
        }
    }
}
