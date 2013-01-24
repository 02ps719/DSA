using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColouredGraph
{
    public class Colour
    {
        private string _colour = string.Empty;

        public Colour(string colour)
        {
            _colour = colour;
        }

        public string GetColour()
        {
            return _colour;
        }
      

    }
}
