using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Toggle
{
    class StartScreen : Screen
    {
        StartScreenButton start;
        StartScreenButton continueB;
        StartScreenButton exit;

        public StartScreen(Game1 eng) : base(eng)
        {
            buttons.Add(start = new StartScreenButton(eng.GraphicsDevice.Viewport.Width / 2 + 160, 300, "start","startHover","play"));
            buttons.Add(continueB = new StartScreenButton(eng.GraphicsDevice.Viewport.Width / 2 + 160, 350, "continue", "continueHover", "continue"));
            buttons.Add(exit = new StartScreenButton(eng.GraphicsDevice.Viewport.Width / 2 + 160, 400, "exit", "exitHover", "exit"));
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
