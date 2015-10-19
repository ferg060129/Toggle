using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Toggle
{

    //Maybe this shouldn't inherit from object
    class InventoryItem : Object
    {
        protected string itemTip = "This is an item";
        protected bool hovered = false;
        public InventoryItem() : base()
        {

        }

        public void setHitBox(Rectangle r)
        {
            hitBox = r;

        }
        public string getItemTip()
        {
            return itemTip;
        }

        public bool isHovered()
        {
            return hovered;
        }

        public void setHovered(bool b)
        {
            hovered = b;
        }


    }
}
