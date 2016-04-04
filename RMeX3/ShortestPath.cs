using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMeX3
{
   public class ShortestPath
    {
        //Number of nodes
        public static int V = 10;

        //Dictionary to store previousNode of the current node to show the shorterst path
        public  Dictionary<int, int> previousNode = new Dictionary<int, int>();
        public static   List<int> shortestPath = new List<int>();

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
      public  void findShortestPath(int start, int end)
        {
            Debug.WriteLine("Dijksrtraasss");

            while (start != end)
            {
                shortestPath.Add(end);
                end = previousNode[end];
            }
            shortestPath.Add(start);
            Debug.WriteLine("Dijksrtraaddd");
        }

        //Method that prints the shortest path between a start and an end node

      public  void printShortestPath(int src, int dest)
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

     public   void dijkstra(int[,] graph, int src, int dest)
        {
            Debug.WriteLine("Dijksrtraa");
            // The output array. dist[i] will hold the shortest distance from src to i 
            int[] dist = new int[V];

            /* sptSet[i] will true if vertex i is included in shortest
               path tree or shortest distance from src to i is finalized */

            Boolean[] sptSet = new Boolean[V];

            // Initialize all distances as INFINITE and stpSet[] as false
            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

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

        }

      

    }
}
