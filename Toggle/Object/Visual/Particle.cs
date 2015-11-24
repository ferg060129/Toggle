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
            badGraphic = Textures.textures[graphicString];
            lifeTime = 10;
            directionVector = new Vector2(Game1.random.Next(7) - 3, Game1.random.Next(7) - 3);
            int randx = Game1.random.Next(1);
            int randy = Game1.random.Next(1);
            imageBoundingRectangle = new Rectangle(randx * 16,randy * 16,16,16);
        }

        public void update()
        {
            setPosition(getPosition() + directionVector);
            spriteAlpha -= 0.1f;
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
