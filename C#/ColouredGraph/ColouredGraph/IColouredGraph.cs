using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ColouredGraph
{
    public interface IColouredGraph
    {
        /// <summary>
        /// Creates an empty graph
        /// </summary>
        /// <returns></returns>
        IColouredGraph CreateGraph();

        /// <summary>
        ///  Adds an edge from vertex v1 to vertex v2 to the graph g.If a new vertex is found, then an entry has to be added in the adjacency list 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        IColouredGraph AddEdge(IColouredGraph g, Vertex v1, Vertex v2);

        /// <summary>
        /// Gets a list of neighbors of the vertex.
        /// </summary>
        IEnumerable<Vertex> GetNeighbors(IColouredGraph g, Vertex v);


        /// <summary>
        /// Sorts the adjacency list based on degree of the vertices. 
        /// The vertex with smallest degree will appear first, and the vertex with the largest degree will appear last. 
        /// If two  vertices are having the same degree, then their order of appearance will not change.
        /// </summary>        
        /// <param name="g"></param>
        /// <param name="sortAdjacentNodesOfEachNode"></param>
        IColouredGraph SortGraphbyDegree(IColouredGraph g, bool sortAdjacentNodesOfEachNode);

        /// <summary>
        /// Returns the first color in the list of colors that satisfies the condition mentioned in step (2) above
        /// which is 
        /// Let u be the un-colored vertex with the smallest degree. [Break ties in favor of the vertex with the smaller id]
        /// a. Assign first color ci in the list of colors to u such that
        /// color(u)  ci where ci != color(vj ) for any vertex vj in the adjacency list of u.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        Colour ChooseColor(IColouredGraph g, Vertex v);

        /// <summary>
        /// Invokes chooseColor vertex by vertex and stores the chosen color in the corresponding vertex. This function returns the number of colors used.
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        int AssignColors(IColouredGraph g);

        /// <summary>
        /// Prints the number of colors used, num_colors_used, in the
        /// first line of the output file. It then prints the graph into the file using the following format:
        /// (vertexid,color):v1,v2,v3,vn
        /// where vertexid is the unique id of the vertex, color is the color assigned to the vertex, and v1,v2,…,vn
        /// correspond the unique identification numbers of the vertices that are adjacent to the current vertex.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="num_colors_used"></param>
        /// <param name="file"></param>
        void PrintGraph(IColouredGraph g, int num_colors_used, string file);

        int VerticesCount { get; set; }
    }
}
