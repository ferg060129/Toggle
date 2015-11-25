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
    class DeadScreen : Screen
    {
        StartScreenButton respawn;
        StartScreenButton startover;
        StartScreenButton exit;
        private SoundEffectInstance beep;
        public DeadScreen(Game1 eng)
            : base(eng)
        {
            buttons.Add(respawn = new StartScreenButton(eng.GraphicsDevice.Viewport.Width / 2 + 160, 300, "respawn", "respawnHover", "reload", eng));
            buttons.Add(startover = new StartScreenButton(eng.GraphicsDevice.Viewport.Width / 2 + 160, 350, "startover", "startoverHover", "startscreen", eng));
            buttons.Add(exit = new StartScreenButton(eng.GraphicsDevice.Viewport.Width / 2 + 160, 400, "exitDead", "exitDeadHover", "exit", eng));

        }


        public override void checkButtonClicks()
        {
            MouseState mouseState = Mouse.GetState();
            Point cursorLocation = engine.convertCursorLocation(mouseState);
            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
            {
                foreach (StartScreenButton sb in buttons)
                {

                    if (sb.getClickBox().Contains(cursorLocation))
                    {
                        string command = sb.onClick();
                        engine.buttonCommand(command);
                    }
                }
            }
            oldMouseState = mouseState;
        }

    }
}
