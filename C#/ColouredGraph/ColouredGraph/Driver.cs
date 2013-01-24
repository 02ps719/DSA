using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ColouredGraph
{
    public class Driver
    {
        /* Assumptions:
         * This system is based on the following assumptions.
         * 1.The input/output file path is a valid path. No check is introduced to verify its validity. 
         *      If an invalid path is entered, then the system exits with an error message
         * 2.The input file is well-formed. No check is made to ensure that the first line is actually a number. 
         *     System shuts down throwing an error if its not. 
         * 3. The input file doesnt specify edges which lead to having more than the vertices present in the first line of the file. 
         *      If that is not the case, the system shuts down flagging an error.
         * 4. All the edges mentioned in the file are valid numbers only. The system doesnt check for the validity of the edge data. Assumes to be numbers.
         * 5. All the edges are mentioned. Since this is an undirected graph, an edge 1,2  also indicates implicitly the existence of edge 2,1. But this 
         *      implementation ignores this implicit assumption but rather expects the input file to provide all the edges i.e. 1,2 and also 2,1
         * 6. Assuming worst case scenario of a complete graph where all sides are adjacent, if 'n' is the number of balls, we chose 'n' as the number of colors needed. 
         * 7. An invalid edge .. like 1,2,3 - having 3 vertices is considered invalid and the system exits.
         * 8. Since the focus is more on graphs and fundas relating to graph,(also since specific sorting algo is not specified), this implementation overlooks the way sorting is 
         *      to be achieved. In-place bubble sort is used. Could be improved to use In-place insertion sort or with some head-twisting, quick sort also.
         * 9. There seemed to be an ambiguity with respect to sorting adjacent nodes or not. So, am doing both. If the third argument in the command line is 
         *      - 1 - (numeric one), then the adjacent nodes list is also sorted, else, only the graph is sorted based on the degree.
         */

        static void Main(string[] args)
        {
            //C-Style declaration first approach
            string inputFilePath = "" , outputFilePath = "";
            StreamReader fileReader = null;
            int numberOfBalls,colourCount;
            IColouredGraph graph;
            string[] vertexEdges = null;
            bool sortAdjacentNodeListalso = false;
            int sortAdjacentNodesFlag = 0;
            try
            {
                //Read input and output files
                if(args.Length> 0) inputFilePath = args[0];
                if (args.Length > 1) outputFilePath = args[1]; // Read output file path with a guard
                if (args.Length > 2) 
                {
                    if(Int32.TryParse(args[2],out sortAdjacentNodesFlag))
                    {
                        if (sortAdjacentNodesFlag == 1)
                            sortAdjacentNodeListalso = true;
                    }
                }
                if (string.IsNullOrEmpty(inputFilePath))
                    throw new Exception("Please provide an input file to read from");                     

                //Read from file
                fileReader = new StreamReader(inputFilePath);

                //Find number of nodes/balls involved
                numberOfBalls = Int32.Parse(fileReader.ReadLine());
                graph = new ColouredGraph().CreateGraph();
                graph.VerticesCount = numberOfBalls;
                //Add edge for each line in the file
                while (!fileReader.EndOfStream)
                {
                    vertexEdges = fileReader.ReadLine().Split(',');
                    if (vertexEdges.Length > 2) throw new Exception("Number of end points of an edge cant be greater than 2");
                    graph.AddEdge(graph, new Vertex(Int32.Parse(vertexEdges[0])), new Vertex(Int32.Parse(vertexEdges[1])));
                }

                fileReader.Close();

                //Sort graph
                //This is craziness in an OO world! Passing the same graph by calling the method on the same and assigning back the same!!
                //But what to do, I cant change the method signature.
                graph = graph.SortGraphbyDegree(graph,sortAdjacentNodeListalso);

                //Assign colours to each vertex
                colourCount = graph.AssignColors(graph);

                //print the graph
                graph.PrintGraph(graph, colourCount, outputFilePath);

                if (!string.IsNullOrEmpty(outputFilePath))
                    Console.WriteLine("The graph has been written to the output file specified");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }
    }
}
