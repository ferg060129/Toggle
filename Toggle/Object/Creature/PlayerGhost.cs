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
    //small change
    class PlayerGhost : Creature
    {
        public PlayerGhost(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {
            goodGraphic = Textures.textures["playerGhost"];
            badGraphic = Textures.textures["playerGhost"];
           
            imageBoundingRectangle = new Rectangle(0, 0, 32, 32);
         
        }

        public override void move()
        {
            
        }

       
    }
    
}