using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Toggle
{
    class Particle : Object
    {
        int lifeTime;
        Vector2 directionVector;
        public Particle(int xLocation, int yLocation,string graphicString)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures[graphicString];
            badGraphic = Textures.textures["berry"];
            lifeTime = 50;
            directionVector = new Vector2(Game1.random.Next(11) - 5, Game1.random.Next(11) - 5);
            imageBoundingRectangle = new Rectangle(0,0,32,32);
        }

        public void update()
        {
            setPosition(getPosition() + directionVector);
            spriteAlpha -= 0.02f;
            imageBoundingRectangle = new Rectangle(0, 0, 32 , 32);
            lifeTime--;
        }
        
        public int getLifetime()
        {
            return lifeTime;
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
