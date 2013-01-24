using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ColouredGraph
{
    public class ColouredGraph: IColouredGraph
    {
        //A Graph G is defined as (V,E), where V is a collection of all nodes and E is a collection of all the edges. 
        //This graph is an undirected graph. 

        private List<Edge> _edges;
        private List<GraphNode> _nodes;
        private List<Colour> _colours = null;
        int colourCount = 0;
        string colour = "c";
        string chosenColor = "";

        public int VerticesCount { get; set; }

        public IColouredGraph CreateGraph()
        {
            if(_edges == null)
                   _edges = new List<Edge>();
            if (_nodes == null)
                _nodes = new List<GraphNode>();
            return this;
        }

        //Keeping the signature same for the heck of it. 
        //Otherwise I wouldnt be needing the graph datatype to be passed in here. 
        //May be in a pure functional programming this might make sense. 
        public IColouredGraph AddEdge(IColouredGraph g, Vertex startingVertex, Vertex endingVertex)
        {
            AddVerticesAndItsNeighbours(startingVertex, endingVertex);
            _edges.Add(new Edge(startingVertex, endingVertex));
            return this;
        }       
        
        public IEnumerable<Vertex> GetNeighbors(IColouredGraph g, Vertex v)
        {
            int graphNodeCount = 0;
            IEnumerable<Vertex> neighbours = null;
            for (graphNodeCount = 0; graphNodeCount < _nodes.Count; graphNodeCount++)
            {
                if (_nodes[graphNodeCount].Vertex.ID == v.ID)
                {
                    neighbours = _nodes[graphNodeCount].AdjacentVertices;
                }                
            }
            if (neighbours == null)
                neighbours = new List<Vertex>();
            return neighbours;            
        }

        public IColouredGraph SortGraphbyDegree(IColouredGraph g,bool sortAdjacentNodesOfEachNode)
        {
            SortGraphNodesByDegree(_nodes);
            if (sortAdjacentNodesOfEachNode)
            {
                //Question mentioned something about sorting adjacency list thoughit doesnt affect the coloring scheme.
                foreach (GraphNode node in _nodes)
                    SortAdjacentNodesOfEachNodeByDegree(node.AdjacentVertices);
            }
            return this;
        }       

        public Colour ChooseColor(IColouredGraph g, Vertex v)
        {
            //some FP here //breaking away from C-style syntax            
            IEnumerable<Vertex> neighbours = g.GetNeighbors(g,v);
            var validColours = _colours.Where(c => !neighbours.Any(n => _nodes.Where(_n => _n.Vertex.ID == n.ID).Any(node => (node.Vertex.Colour != null && node.Vertex.Colour.GetColour().Equals(c.GetColour()))))).Select(c => c);
            if (validColours.Any())
                return validColours.First();
            else
            {
                // should not come here. Just a fail safe precaution. 
                _colours.Add(new Colour(colour + (VerticesCount + 1).ToString()));
                return _colours.Last();
            }
        }

        //The graph is already sorted at this stage. This is the assumption. 
        //procedural programming!!! People...!
        public int AssignColors(IColouredGraph g)
        {
            int verticesCount = 0;
            //lazy assignent of colours list. Done only once. 
            if (_colours == null)
            {
                _colours = new List<Colour>();
                //C-style
                for (verticesCount = 0; verticesCount < VerticesCount; verticesCount++)
                {
                    _colours.Add(new Colour(colour + (verticesCount + 1).ToString()));
                }
            }

            int graphNodeCount = 0;
            Colour vertexColour;
            Vertex v;
            for (graphNodeCount = 0; graphNodeCount < _nodes.Count; graphNodeCount++)
            {
                v = _nodes[graphNodeCount].Vertex;
                vertexColour = ChooseColor(g,v);
                v.Colour = vertexColour;
                if (vertexColour.GetColour() != chosenColor)
                {
                    chosenColor = vertexColour.GetColour();
                    colourCount++;
                }                
            }
            return colourCount;
        }

        public void PrintGraph(IColouredGraph g, int num_colors_used, string outputFilePath)
        {
            bool writeToConsole = false;
            int nodeCount = 0;
            string adjacentNodes = "";
            int neighbourCount = 0;
            if (string.IsNullOrEmpty(outputFilePath)) // no output file path mentioned, write to console
                writeToConsole = true;
            //(vertexid,color):v1,v2,v3,vn
            if (writeToConsole)
            {
                Console.WriteLine("Number of colours used is " + colourCount);
                _nodes.All(n => { Console.WriteLine(String.Format("({0},{1}):{2}", n.Vertex.ID, n.Vertex.Colour.GetColour(), n.AdjacentVertices.Select(v => v.ID.ToString()).Aggregate((id1, id2) => id1 + "," + id2))); return true; });
            }
            else
            {
                StreamWriter fileWriter = new StreamWriter(outputFilePath);
                fileWriter.WriteLine("Number of colours used is " + colourCount);
                //C-style
                for (nodeCount = 0; nodeCount < _nodes.Count; nodeCount++)
                {
                    adjacentNodes = "";

                    for(neighbourCount = 0; neighbourCount < _nodes[nodeCount].AdjacentVertices.Count; neighbourCount++)
                    {
                        adjacentNodes += _nodes[nodeCount].AdjacentVertices[neighbourCount].ID.ToString() + ",";
                    }

                    adjacentNodes = adjacentNodes.Remove(adjacentNodes.LastIndexOf(","));

                    fileWriter.WriteLine(String.Format("({0},{1}):{2}",_nodes[nodeCount].Vertex.ID,_nodes[nodeCount].Vertex.Colour.GetColour(),adjacentNodes));

                }
                fileWriter.Close();

            }

        }

        #region private methods

        private void AddVerticesAndItsNeighbours(Vertex startingVertex, Vertex endingVertex)
        {
            int graphNodeCount = 0;
            bool startingVertexAlreadyAdded = false, endingVertexAlreadyAdded = false;

            //doing good old c-way
            for (graphNodeCount = 0; graphNodeCount < _nodes.Count; graphNodeCount++)
            {
                if (_nodes[graphNodeCount].Vertex.ID == startingVertex.ID)
                {
                    //implicitly - has atleast one neighbour  

                    if (!IsVertexPresentInNeighbours(_nodes[graphNodeCount].AdjacentVertices, endingVertex))
                    {
                        _nodes[graphNodeCount].AdjacentVertices.Add(endingVertex);
                        _nodes[graphNodeCount].DegreeOfVertex++;
                    }
                    startingVertexAlreadyAdded = true;
                }

                if (_nodes[graphNodeCount].Vertex.ID == endingVertex.ID)
                {
                    //implicitly - has atleast one neighbour
                    if (!IsVertexPresentInNeighbours(_nodes[graphNodeCount].AdjacentVertices, startingVertex))
                    {
                        _nodes[graphNodeCount].AdjacentVertices.Add(startingVertex);
                        _nodes[graphNodeCount].DegreeOfVertex++;
                    }
                    endingVertexAlreadyAdded = true;

                }
            }

            //C# style of iteration
            //foreach (var graphNode in _nodes)
            //{
            //    if (graphNode.Vertex.ID == startingVertex.ID)
            //    {
            //        if (!IsVertexPresentInNeighbours(graphNode.AdjacentVertices, endingVertex))
            //        {
            //            graphNode.AdjacentVertices.Add(new Vertex(endingVertex.ID));
            //            graphNode.DegreeOfVertex++;
            //        }
            //        startingVertexAlreadyAdded = true;
            //    }

            //    if (graphNode.Vertex.ID == endingVertex.ID)
            //    {
            //        //implicitly - has atleast one neighbour
            //        if (!IsVertexPresentInNeighbours(graphNode.AdjacentVertices, startingVertex))
            //        {
            //            graphNode.AdjacentVertices.Add(new Vertex(startingVertex.ID));
            //            graphNode.DegreeOfVertex++;
            //        }
            //        endingVertexAlreadyAdded = true;
            //    }

            //}


            if (!startingVertexAlreadyAdded)
                _nodes.Add(new GraphNode(startingVertex, 1, new List<Vertex>() { new Vertex(endingVertex.ID) }));
            if (!endingVertexAlreadyAdded)
                _nodes.Add(new GraphNode(endingVertex, 1, new List<Vertex>() { new Vertex(startingVertex.ID) }));
            if (_nodes.Count > VerticesCount)
                throw new Exception("Number of vertices exceeded the intial contract as stated in the file.");

        }

        private bool IsVertexPresentInNeighbours(List<Vertex> neighbours, Vertex v)
        {
            bool isPresent = false;

            foreach (var vertex in neighbours)
            {
                if (vertex.ID == v.ID)
                {
                    isPresent = true;
                    break;
                }
            }
            return isPresent;

        }

        private Degree Compare(GraphNode firstNode, GraphNode secondNode)
        {
            if (firstNode.DegreeOfVertex > secondNode.DegreeOfVertex)
                return Degree.GREATER;
            else if (firstNode.DegreeOfVertex < secondNode.DegreeOfVertex)
                return Degree.LESSER;
            else
                return Degree.EQUAL;
        }

        private void SwapNodes(List<GraphNode> nodes, int pos1, int pos2)
        {
            GraphNode temp;
            temp = nodes[pos1];
            nodes[pos1] = nodes[pos2];
            nodes[pos2] = temp;
        }

        private void SwapVertices(List<Vertex> vertices, int pos1, int pos2)
        {
            Vertex temp;
            temp = vertices[pos1];
            vertices[pos1] = vertices[pos2];
            vertices[pos2] = temp;
        }

        private void SortGraphNodesByDegree(List<GraphNode> _nodes)
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                for (int j = i + 1; j < _nodes.Count; j++)
                {
                    if (Compare(_nodes[i], _nodes[j]) == Degree.GREATER)
                    {
                        SwapNodes(_nodes, i, j);
                    }
                    else if (Compare(_nodes[i], _nodes[j]) == Degree.EQUAL)
                    {
                        if (_nodes[j].Vertex.ID < _nodes[i].Vertex.ID)
                            //node at j has id less than i => j should come before i.
                            SwapNodes(_nodes, i, j);
                    }
                }
            }
        }

        private void SortAdjacentNodesOfEachNodeByDegree(List<Vertex> vertices)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                for (int j = i + 1; j < vertices.Count; j++)
                {
                    if (GetDegreeOfVertex(vertices[i]) > GetDegreeOfVertex(vertices[j]))
                    {
                        SwapVertices(vertices, i, j);
                    }
                }
            }
        }

        private int GetDegreeOfVertex(Vertex v)
        {
            int degree = 0;
            foreach (var node in _nodes)
            {
                if (node.Vertex.ID == v.ID)
                {
                    degree = node.DegreeOfVertex;
                    break;
                }
            }
            return degree;
        }

        #endregion private methods
    }
}
