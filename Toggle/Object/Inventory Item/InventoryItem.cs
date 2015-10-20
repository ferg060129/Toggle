﻿using System;
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
        protected string itemTipGood = "Banana";
        protected string itemTipBad = "Rotten banana :c";
        protected bool hovered = false;
        public InventoryItem() : base()
        {
            width = 32;
            height = 32;
        }

        public void setHitBox(Rectangle r)
        {
            hitBox = r;

        }
        public string getItemTip()
        {
            if(state)
            {
                return itemTipGood;
            }
            else
            {
                return itemTipBad;
            }
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
