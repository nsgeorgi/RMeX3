using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RMeX3
{
    public  class Graph
    {
        public static List<Node> nodes = new List<Node>();
        public static int[,] matrix = new int[10, 10];

        //Number of nodes
        public static int V = 10;

        //Dictionary to store previousNode of the current node to show the shorterst path
        public Dictionary<int, int> previousNode = new Dictionary<int, int>();
        public List<int> shortestPath = new List<int>();

        /// <summary> A utility function to find the vertex with minimum distance value,
        /// from the set of vertices not yet included in shortest path tree</summary>
        /// <param name="dist"></param>
        /// <param name="sptSet"></param>



        public Graph()
        {
           
           
        }

        public void create_nodes()
        {
            Node n0 = new Node(0, 0, 0, "", new Image());
            n0.neighbours = new List<int> { 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            nodes.Add(n0);


            Node n1 = new Node(1, 140, 450, "", new Image());
            n1.neighbours = new List<int> { 0, 0, 1, 1, 0, 0, 0, 0, 0, 0 };
            nodes.Add(n1);


            Node n2 = new Node(2, 160, 450, "Room 1", new Image());
            n2.neighbours = new List<int> { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            nodes.Add(n2);

            Node n3 = new Node(3, 140, 300, "", new Image());
            n3.neighbours = new List<int> { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0 };
            nodes.Add(n3);

            Node n4 = new Node(4, 140, 270, "", new Image());
            n4.neighbours = new List<int> { 0, 0, 0, 1, 0, 1, 0, 1, 0, 0 };
            nodes.Add(n4);

            Node n5 = new Node(5, 140, 240, "", new Image());
            n5.neighbours = new List<int> { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0 };
            nodes.Add(n5);

            Node n6 = new Node(6, 180, 240, "Room 2", new Image());
            n6.neighbours = new List<int> { 0, 0, 0, 0, 0, 1, 0, 1, 0, 0 };
            nodes.Add(n6);

            Node n7 = new Node(7, 180, 270, "", new Image());
            n7.neighbours = new List<int> { 0, 0, 0, 0, 1, 0, 1, 0, 1, 0 };
            nodes.Add(n7);

            Node n8 = new Node(8, 228, 270, "", new Image());
            n8.neighbours = new List<int> { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 };
            nodes.Add(n8);

            Node n9 = new Node(9, 228, 303, "Kitchen", new Image());
            n9.neighbours = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 };
            nodes.Add(n9);



        }

        public void create_graph()
        {

            // create graph matrix
            Debug.WriteLine("Graph Matrix: ");
            for (int i = 1; i < nodes.Count; i++)
            {
                Debug.Write("[ ");
                for (int j = 1; j < nodes[i].neighbours.Count; j++)
                {
                    if (nodes[i].neighbours[j] == 1)

                    {
                        //Debug.WriteLine("Megethos listas : " + graph.nodes[i].neighbours.Count);
                        // Debug.WriteLine("Id x: " + graph.nodes[i].id + " Id y: " + graph.nodes[j].id); 
                        matrix[i, j] = calculate_distance(nodes[i].x, nodes[j].x, nodes[i].y,nodes[j].y);
                        //  Debug.WriteLine(" x1: " + graph.nodes[i].x + " y1: "+ graph.nodes[i].y + " x2: "+ graph.nodes[j].x + " y2: "+ graph.nodes[j].y + " Distance: "+ graph.nodes[i].neighbours[i]);
                    }
                    else
                    {
                        matrix[i, j] = 0;

                    }
                    if (j == nodes[i].neighbours.Count - 1) Debug.Write(matrix[i, j]);
                    else Debug.Write(matrix[i, j] + " , ");
                }

                Debug.Write(" ] ");
                Debug.WriteLine("");
            }

        }

        // calculate distance between 2 nodes in the graph
        public int calculate_distance(int x1, int x2, int y1, int y2)
        {

            return (int)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }



        private void RouteCalculation_Dijkstra(int destination)
        {

            //prepei na vrw ta 2 kontina nodes sto user's current location kai na ta valw sto neighbours tou.
            // Debug.WriteLine("Times tou 0: " + graph.nodes[0].x + graph.nodes[0].y);
            for (int i = 1; i < nodes.Count; i++)
            {

                nodes[i].distfromsrc = (calculate_distance(nodes[0].x,nodes[i].x, nodes[0].y, nodes[i].y));
                Debug.WriteLine("Graph node id: " + nodes[i].id + " DIstance from src " + nodes[i].distfromsrc);
            }
            nodes = nodes.OrderBy(x => x.distfromsrc).ToList();
            matrix[0, nodes[0].id] = nodes[0].distfromsrc;
            matrix[0, nodes[1].id] = nodes[1].distfromsrc;
            //  Debug.WriteLine("Min Value: " + graph.nodes[0].distfromsrc);
            //   Debug.WriteLine("Min Value: " + graph.nodes[1].distfromsrc);

            Debug.WriteLine("===== Graph with source node ====== ");
            for (int i = 0; i < 10; i++)

            {

                Debug.Write(matrix[0, i] + " ");
            }

            /*     int[,] graph = new int[,] {{0, 4, 0, 0, 0, 0, 0, 8, 0},
                           {4, 0, 8, 0, 0, 0, 0, 11, 0},
                           {0, 8, 0, 7, 0, 4, 0, 0, 2},
                           {0, 0, 7, 0, 9, 14, 0, 0, 0},
                           {0, 0, 0, 9, 0, 10, 0, 0, 0},
                           {0, 0, 4, 0, 10, 0, 2, 0, 0},
                           {0, 0, 0, 14, 0, 2, 0, 1, 6},
                           {8, 11, 0, 0, 0, 0, 1, 0, 7},
                           {0, 0, 2, 0, 0, 0, 6, 7, 0}
                          };

         */
            
            int src = 0;
            int dest = destination;

            Debug.WriteLine("****   Stage 3  ***");
            dijkstra(matrix, src, dest);
            Debug.WriteLine("****   Stage 3  ***");
            printShortestPath(src, dest);
            Debug.WriteLine("****   Stage 3  ***");
            nodes = nodes.OrderBy(x => x.id).ToList();
            for (int i = 0; i < 10; i++)
            {
                matrix[0, i] = 0;

            }

           


        }
        

        /// <summary> A utility function to find the vertex with minimum distance value,
        /// from the set of vertices not yet included in shortest path tree</summary>
        /// <param name="dist"></param>
        /// <param name="sptSet"></param>

        int minDistance(int[] dist, Boolean[] sptSet)
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
            {
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }
            }
            return min_index;
        }

        //Method that finds the shortest path between a start and an end node and stores its nodes in a list
        public void findShortestPath(int start, int end)
        {

            while (start != end)
            {
                shortestPath.Add(end);
                end = previousNode[end];
            }
            shortestPath.Add(start);

        }

        //Method that prints the shortest path between a start and an end node

        public void printShortestPath(int src, int dest)
        {

            findShortestPath(src, dest);

            Debug.WriteLine("Shortest path from node " + src + " to node " + dest + " :");

            for (int i = shortestPath.Count; i >= 1; i--)
            {
                Debug.WriteLine(shortestPath[i - 1]);
                Debug.WriteLine(" ");
            }
            Debug.WriteLine("");
        }

        /* 
        Funtion that implements Dijkstra's single source shortest path
        algorithm for a graph represented using adjacency matrix
        representation given a start and destination node */

        public void dijkstra(int[,] graph, int src, int dest)
        {
            Debug.WriteLine("****   Stage 3  ***");
            // The output array. dist[i] will hold the shortest distance from src to i 
            int[] dist = new int[V];

            /* sptSet[i] will true if vertex i is included in shortest
               path tree or shortest distance from src to i is finalized */

            Boolean[] sptSet = new Boolean[V];
            Debug.WriteLine("****   Stage 3  ***");
            // Initialize all distances as INFINITE and stpSet[] as false
            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }
            Debug.WriteLine("****   Stage 3  ***");
            // Distance of source vertex from itself is always 0
            dist[src] = 0;

            // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++)
            {
                /* 
                 Pick the minimum distance vertex from the set of vertices
                 not yet processed. u is always equal to src in first
                 iteration. */
                int u = minDistance(dist, sptSet);

                // Mark the picked vertex as processed
                sptSet[u] = true;

                //If we reach the final destination node the method returns
                if (u == dest) return;

                // Update dist value of the adjacent vertices of the picked vertex.
                for (int v = 0; v < V; v++)

                    /* 
                     Update dist[v] only if is not in sptSet, there is an
                     edge from u to v, and total weight of path from src to
                     v through u is smaller than current value of dist[v] */

                    if (!sptSet[v] && graph[u, v] != 0 &&
                            dist[u] != int.MaxValue &&
                            dist[u] + graph[u, v] < dist[v])
                    {

                        dist[v] = dist[u] + graph[u, v];

                        //v's previous node is u
                        if (!previousNode.ContainsKey(v))
                            previousNode.Add(v, u);
                        else
                            previousNode[v] = u;
                    }
            }
            Debug.WriteLine("****   Stage 3  ***");

        }




    }
}