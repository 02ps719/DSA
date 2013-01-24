using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSA
{
    public class Number
    {
        public Number(int radix,List<char> sequence)
        {
            Radix = radix;
            Sequence = sequence;
        }
        public int Radix { get; set; }
        List<char> sequence;
        public List<char> Sequence
        {
            get
            {
                if (sequence == null)
                    sequence = new List<char>();
                return sequence;
            }
            set 
            {
                sequence = value;
            }
        }
    }
}
