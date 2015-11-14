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
    class Screen
    {

        protected ArrayList buttons = new ArrayList();
        protected MouseState oldMouseState;
        protected Game1 engine;
        protected ScreenButton hoveredButton = null;
        public Screen(Game1 eng)
        {
            engine = eng;

        }


        public void checkButtonClicks()
        {
            MouseState mouseState = Mouse.GetState();
            Point cursorLocation = engine.convertCursorLocation(mouseState);
            if(mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
            {
                foreach(ScreenButton sb in buttons)
                {
                    
                    if(sb.getClickBox().Contains(cursorLocation))
                    {
                        sb.onClick();
                    }
                }
            }
            oldMouseState = mouseState;
        }

        public void checkButtonHovers()
        {
            MouseState mouseState = Mouse.GetState();
            foreach (ScreenButton sb in buttons)
            {
                Point cursorLocation = engine.convertCursorLocation(mouseState);
                if (sb.getClickBox().Contains(cursorLocation))
                {
                    sb.onHover();
                    hoveredButton = sb;
                    return;
                }
            }
            hoveredButton = null;
        }

        public virtual void drawScreen(SpriteBatch sb)
        {
            MouseState mouseState = Mouse.GetState();
            Point mouseLoc = engine.convertCursorLocation(mouseState);
            foreach (ScreenButton b in buttons)
            {
                sb.Draw(b.getGraphic(), new Vector2(b.getLocation().X, b.getLocation().Y), b.getImageBoundingRectangle(), Color.White);
                
            }
            sb.Draw(Textures.textures["cursor"], new Vector2(mouseLoc.X, mouseLoc.Y), Color.White);
        }
    }
}
