using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Toggle
{
    class ScreenButton
    {
        protected int x,y;
        protected Rectangle imageBoundingRectangle;
        protected Rectangle clickBox;
        public ScreenButton()
        {


        }

        public void onHover()
        {

        }

        public virtual void onClick()
        {

        }
        

        public Point getLocation()
        {
            return new Point(x, y);
        }

        public virtual Texture2D getGraphic()
        {
            return null;
        }

        public Rectangle getImageBoundingRectangle()
        {
            return imageBoundingRectangle;
        }
        public Rectangle getClickBox()
        {
            return clickBox;
        }
    }
}
