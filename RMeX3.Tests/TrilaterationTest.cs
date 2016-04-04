using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RMeX3.Tests
{
    [TestClass]
    public class TrilaterationTest
    {


        public static int[,] graph = new int[,] {{0, 20, 151, 0, 0, 0, 0, 0, 0},
                           {20, 0, 0, 0, 0, 0, 0, 0, 0},
                           {151, 0, 0, 30, 0, 0, 0, 0, 0},
                           {0, 0, 30, 0, 30, 0, 40, 0, 0},
                           {0, 0, 0, 30, 0, 40, 0, 0, 0},
                           {0, 0, 0, 0 ,40, 0, 30, 0, 0},
                           {0, 0, 0, 40, 0, 30, 0, 48, 0},
                           {0, 0, 0, 0, 0, 0, 48, 0, 33},
                           {0, 0, 0, 0, 0, 0, 0, 33, 0}
                          };


        [TestMethod]
        public void TrilaterateTest()
        {

            //Arrange
            Trilateration tt = new Trilateration();

            tt.config(119, 532, 74, tt.res1);
            tt.config(247, 272, 111, tt.res2);
            tt.subtractCircle(tt.res1, tt.res2);

            tt.config(247, 272, 111, tt.res1);
            tt.config(24, 121, 111, tt.res2);
            tt.subtractCircle(tt.res1, tt.res2);
            tt.solveForXAndY();
            //Assert
            Assert.AreEqual(36, tt.res3[0]);
            Assert.AreEqual(342, tt.res3[1]);
        }

        [TestMethod]

        public void calculate_distance_test()
        {
            int x1 = 100;
            int x2 = 200;
            int y1 = 100;
            int y2 = 200;

            Assert.AreEqual(141, (int)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
        }

        [TestMethod]
        public void dijkstra_test()
        {
            ShortestPath sp = new ShortestPath();
            ShortestPath.V = 9;


            int src = 0;
            int dest = 1;
            sp.dijkstra(graph, src, dest);
            sp.findShortestPath(src, dest);

            Debug.WriteLine("Dijksrtraasss");
            //  Assert.AreEqual(10, ShortestPath.shortestPath);
            List<int> expected = new List<int>(new int[] { 1, 0 });
            //Assert.AreEqual(1, ShortestPath.shortestPath[0]);
            //Assert.AreEqual(0, ShortestPath.shortestPath[1]);

            CollectionAssert.AreEqual(expected, ShortestPath.shortestPath);
        }


        [TestMethod]
        public void accuracy_test()
        {
            AccuracyCalculator cfdc = new CurveFittedDistanceCalculator(0.42093, 6.9476, 0.54992);
            double distance = Math.Round(cfdc.calculateAccuracy(-58, 50), 2);
            Assert.AreEqual(0.23, distance);
        }




        [TestMethod]
        public void checkblt_test()
        {
            Logic l = Logic.getInstance();
           
            l.checkblt();

            Assert.IsFalse(Logic.bluetooth_flag);
        }


        [Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer.UITestMethod]
        public void calculate_path_distance_test()
        {

           // ShortestPath sp = new ShortestPath();
            ShortestPath.shortestPath= new List<int>(new int[] { 1, 3, 4 ,5 ,6 });
            //assert
           Graph graph = new Graph();
           graph.create_nodes();
            for (int i = ShortestPath.shortestPath.Count - 3; i >= 0; i--)
            {

                Gui.total_distance = Gui.total_distance + Graph.nodes[ShortestPath.shortestPath[i]].distfromsrc;
            }

            Gui.total_distance = (int)(Gui.total_distance / 35);
            Assert.AreEqual(85, Gui.total_distance);

        }


        [Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer.UITestMethod]
        public void three_signals()
        {
            // **** Step 4: Begin Find 3 Strongest Signals  ****    //
            Logic l = Logic.getInstance();
            l.create_beacons();

            // find the 3 strongest signals and pass it as a parameters to trilateration algo
            Debug.WriteLine("=======  STEP 3 ========");

            for (int i = 0; i < Logic.beacons.Count; i++)

            {
                double sum = 0;
                // Debug.WriteLine("Beacon mac address: " +beacons[i].id);
                for (int j = 0; j < Logic.beacons[i].distances.Length; j++)

                {
                    Logic.beacons[i].distances[j] = i+j;
                    // sum =sum +  beacons[i].distances[j];
                }


                Logic.beacons[i].mean_counter = 0;
            }

            for (int i = 0; i < Logic.beacons.Count; i++)
            {
                Array.Sort(Logic.beacons[i].distances);
               
                if (Double.IsNaN(Logic.beacons[i].final_distance))
                {
                    
                    Logic.beacons[i].final_distance = 1000;

                }
                Logic.beacons[i].dist_pixel = (int)(Logic.beacons[i].distances[0] * 35); //  1 m =~ 35 pixels

            }
            Logic.beacons = Logic.beacons.OrderBy(x => x.distances[0]).ToList();
            Assert.IsTrue(Logic.beacons[0].distances[0] < Logic.beacons[0].distances[1]);
            Assert.IsTrue(Logic.beacons[1].distances[0] < Logic.beacons[1].distances[1]);
            Assert.IsTrue(Logic.beacons[2].distances[0] < Logic.beacons[2].distances[1]);
        }
    }

}