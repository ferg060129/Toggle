using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toggle
{
    class InventoryScreen : Screen
    {
        public InventoryScreen(Game1 eng)
            : base(eng)
            {
                buttons.Add(new InventoryScreenButton(0, 0, "blah", "blah", this));

            }
      





    }
}
