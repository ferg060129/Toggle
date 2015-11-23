using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Collections;

namespace Toggle
{
    class InventoryScreen : Screen
    {
        Point textLocation = new Point(100, 100);
        int index = 0;
        string[] instructions = { "first", "second", "third" };

        public InventoryScreen(Game1 eng)
            : base(eng)
        {
            buttons.Add(new InventoryScreenButton(eng.GraphicsDevice.Viewport.Width / 2 + 160, 300, "start", "startHover", this));


        }

        public void nextString()
        {
            index++;
        }

        public override void drawScreen(SpriteBatch sb)
        {
            MouseState mouseState = Mouse.GetState();
            Point mouseLoc = engine.convertCursorLocation(mouseState);
            foreach (ScreenButton b in buttons)
            {
                sb.Draw(b.getGraphic(), new Vector2(b.getLocation().X, b.getLocation().Y), b.getImageBoundingRectangle(), Color.White);

            }
            sb.DrawString(Textures.fonts["arial12"], instructions[index], new Vector2(textLocation.X, textLocation.Y), Color.Black);
            sb.Draw(Textures.textures["cursor"], new Vector2(mouseLoc.X, mouseLoc.Y), Color.White);
        }

       
      





    }
}
