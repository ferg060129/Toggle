using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Toggle
{
    class PauseScreen : Screen
    {
        Game1 engine;
        public PauseScreen(Game1 eng)
            : base(eng)
            {
                engine = eng;
                buttons.Add(new MenuButton(290, 280, "title", "start"));
                buttons.Add(new MenuButton(290, 380, "help", "help"));
            }

        public override void checkButtonClicks()
        {
            MouseState mouseState = Mouse.GetState();
            Point cursorLocation = engine.convertCursorLocation(mouseState);
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
            {
                foreach (MenuButton sb in buttons)
                {

                    if (sb.getClickBox().Contains(cursorLocation))
                    {
                        string newState = sb.onClick();
                        engine.setState(newState,"");
                    }
                }
            }
            oldMouseState = mouseState;
        }
    }
}
