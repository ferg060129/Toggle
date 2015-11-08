using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Toggle
{
    class Lake : Visual
    {
        int ctr = 0;
        int image = 0;
        Texture2D water0 = Textures.textures["water"];
        Texture2D water1 = Textures.textures["water2"];
        Texture2D water2 = Textures.textures["water3"];
        Texture2D water3 = Textures.textures["water4"];
        public Lake(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["water"];
            badGraphic = Textures.textures["water"];
            imageBoundingRectangle = new Rectangle(0, 0, 448, 320);
        }

        public override Texture2D getGraphic()
        {

            if (ctr >= 10)
            {
                switch (image)
                {
                    case 0:
                        image = 1;
                        goodGraphic = water1;
                        badGraphic = water1;
                        break;
                    case 1:
                        image = 2;
                        goodGraphic = water2;
                        badGraphic = water2;
                        break;
                    case 2:
                        image = 3;
                        goodGraphic = water3;
                        badGraphic = water3;
                        break;
                    case 3:
                        image = 0;
                        goodGraphic = water0;
                        badGraphic = water0;
                        break;
                    default:
                        break;
                }
                ctr = 0;
            }
            ctr++;
            return goodGraphic;

        }




    }
}
