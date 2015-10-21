using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class Miscellanious : Object
    {
        public Miscellanious(int xLocation, int yLocation)
            : base(xLocation, yLocation)
        {

        }

        //When the world is shifted
        public virtual void onShift()
        {

        }

        //When a button linked to the object is activated
        public virtual void onButton()
        {

        }

        //When the object does something
        public virtual void onTrigger()
        {

        }


    }
}
