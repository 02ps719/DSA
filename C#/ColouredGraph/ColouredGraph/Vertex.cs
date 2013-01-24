using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColouredGraph
{
    public class Vertex
    {
        int _id; 
        
        public Vertex(int id)
        {
            _id = id;            
        }

        public int ID
        {
            get
            {
                return _id;
            }
        }       
        public Colour Colour { get;set;}
    }
}

