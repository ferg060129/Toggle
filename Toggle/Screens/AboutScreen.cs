using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Toggle
{
    class AboutScreen : Screen
    {

        //public ObjectButton hoveredButton;
        Vector2 drawHoveredLoc = new Vector2(500, 300);
        public AboutScreen(Game1 eng) : base(eng)
        {
            buttons.Add(new ObjectButton(3, 3, "buttonUp", "buttonUp", "This is a thingy you should step on", "This is still a tihngy"));
        }

        public override void drawScreen(SpriteBatch sb)
        {
            sb.Draw(Textures.textures["aboutScreen"], new Vector2(0, 0), Color.White);
            base.drawScreen(sb);

            if(hoveredButton != null)
                sb.Draw(hoveredButton.getGraphic(), drawHoveredLoc, hoveredButton.getImageBoundingRectangle(), Color.White);

        }


    }
}
