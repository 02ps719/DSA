using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColouredGraph
{
    public class Edge
    {
        public Edge(Vertex startingVertex, Vertex endingVertex)
        {
            StartingVertex = startingVertex;
            EndingVertex = endingVertex;
        }

        public Vertex StartingVertex { get; set; }

        public Vertex EndingVertex { get; set; }

    }
}
