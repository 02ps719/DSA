using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColouredGraph
{
    public class GraphNode
    {

        public GraphNode(Vertex vertex, int degree, List<Vertex> adjacentVertices)
        {
            Vertex = vertex;
            DegreeOfVertex = degree;
            AdjacentVertices = adjacentVertices;
        }
        public Vertex Vertex { get; set; }
        public int DegreeOfVertex { get; set; }
        public List<Vertex> AdjacentVertices { get; set; }
    }
}
