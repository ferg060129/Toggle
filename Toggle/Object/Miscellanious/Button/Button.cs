using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Button : Miscellanious
    {
        //heavy buttons are only pressable by pushable objects
        protected bool heavyButton;
        //When you press the button, link does something
        protected Miscellanious link;
         public Button(int xLocation, int yLocation, Miscellanious linked)
            : base(xLocation, yLocation)
        {

        }
    
        public override void onTrigger()
        {
            if (heavyButton)
            {
                goodGraphic = Textures.textures["buttonHDown"];
                badGraphic = Textures.textures["buttonHDown"];
            }
            else
            {
                goodGraphic = Textures.textures["buttonDown"];
                badGraphic = Textures.textures["buttonDown"];
            }
            link.onButton();
        }
        public bool isHeavyButton()
        {
            return heavyButton;
        }


        public void setHeavyButton(bool status)
        {
            heavyButton = status;
            if (status)
            {
                goodGraphic = Textures.textures["buttonHUp"];
                badGraphic = Textures.textures["buttonHUp"];
            }
        }
    }
}
