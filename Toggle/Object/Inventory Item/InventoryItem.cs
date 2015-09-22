using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{

    //Maybe this shouldn't inherit from object
    class InventoryItem : Object
    {
        public InventoryItem(bool initialState) : base(initialState)
        {

        }

    }
}
