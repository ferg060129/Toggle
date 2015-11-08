using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
namespace Toggle
{
    class Visual : Object
    {
        public Visual(int xLocation, int yLocation) : base(xLocation, yLocation)
        {

        }
        public virtual Texture2D getGraphic()
        {
            if (state)
                return goodGraphic;
            else
                return badGraphic;
        }

    }
}
