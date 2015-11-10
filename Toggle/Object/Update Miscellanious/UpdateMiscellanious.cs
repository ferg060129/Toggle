using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class UpdateMiscellanious : Object
    {
        public UpdateMiscellanious(int xLocation, int yLocation)
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

        //Every update frame
        public virtual void onUpdate()
        {

        }

        //When the object does something
        public virtual void onTrigger()
        {

        }

        public virtual void move()
        {

        }
    }
}
