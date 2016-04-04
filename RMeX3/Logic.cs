using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Radios;
using Windows.System.Threading;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;


namespace RMeX3
{
    public class Logic
    {
        public static List<Beacon> beacons = new List<Beacon>();
        public static List<Room> rooms = new List<Room>();
        public Graph graph;
        public static ThreadPoolTimer PeriodicTimer1;
        public static ThreadPoolTimer PeriodicTimer2;
        public static bool bluetooth_flag = false;
        public static bool update = false;
        public Scan s = new Scan();
        public Trilateration tt = new Trilateration();
        public ShortestPath sp = new ShortestPath();
        private static Logic instance = new Logic();
        Logic l = Logic.getInstance();
        public static int current_x;
        public static int current_y;

        private Logic() { }

        public static Logic getInstance()
        {
            return instance;
        }

        public void create_logic()
        {
            create_beacons(); //create instances for beacons
            create_rooms(); //create instances for rooms
            checkblt(); //check if bluetooth is on/off
            graph = new Graph();
            graph.create_nodes(); //create instances for nodes
            graph.create_graph(); //create the main graph for the floorplan
        }

        public void scan()
        {

            // **** Step 1: Begin Scanning for Beacons   ****    //
            s.scan_beacons(beacons);
            // **** Step 1: End Scanning for Beacons   ****    //

        }


        public void trilaterate()
        {
            

                three_signals();

            // **** Step 5: Begin Trilateration Algorithm   ****    //

            //      if (beacons[0].distances[0] == 1000 || beacons[1].distances[1] == 1000 || beacons[2].distances[2] == 1000)
            if (false)
                {

                    Debug.WriteLine("****  Trilateration cannot start !!!   *****");
                    String str1 = "You must be inside the building to start the navigation!";
                    String str2 = "Ok";

                    //   message_dialog(str1, str2);
                }
                else {
                    Debug.WriteLine("Beacons for Trilateration:  ");
                    Debug.WriteLine(beacons[0].x + "  " + beacons[0].y + " " + beacons[0].dist_pixel);
                    Debug.WriteLine(beacons[1].x + "  " + beacons[1].y + " " + beacons[1].dist_pixel);
                    Debug.WriteLine(beacons[2].x + "  " + beacons[2].y + " " + beacons[2].dist_pixel);

                    Debug.WriteLine("Step 1: " + tt.config(beacons[0].x, beacons[0].y, beacons[0].dist_pixel, tt.res1));
                    Debug.WriteLine("Step 2: " + tt.config(beacons[1].x, beacons[1].y, beacons[1].dist_pixel, tt.res2));
                    Debug.WriteLine("Step 3: " + tt.subtractCircle(tt.res1, tt.res2));
                    Debug.WriteLine("Step 4: " + tt.y);
                    Debug.WriteLine("Step 5: " + tt.config(beacons[1].x, beacons[1].y, beacons[1].dist_pixel, tt.res1));
                    Debug.WriteLine("Step 6: " + tt.config(beacons[2].x, beacons[2].y, beacons[2].dist_pixel, tt.res2));
                    Debug.WriteLine("Step 7: " + tt.subtractCircle(tt.res1, tt.res2));
                    Debug.WriteLine("Step 8: " + tt.y);
                    tt.solveForXAndY();
                    Debug.WriteLine("Step 9: " + tt.a);
                    current_x = tt.res3[0];
                    current_y = tt.res3[1];
                    //current_x = 140;
                    // current_y = 471; //436 -471
                    Random rnd = new Random();
                    current_x = rnd.Next(0, 200);
                    current_y = rnd.Next(20, 300);
                    Graph.nodes[0].x = current_x;
                    Graph.nodes[0].y = current_y;
                    Debug.WriteLine("===============================================================" + Graph.nodes[0].x);
                    Debug.WriteLine("===============================================================" + Graph.nodes[0].y);

                }

            // **** Step 5: End Trilateration Algorithm   ****    //

        }

        public void find_path()
        {


            // Get user's input. If room exists then continue. Else display an error message !                                           

            if (Home_Page.search_change)
            {
                if (!Home_Page.textbox.Text.Equals(""))
                {
                    Home_Page.str = Home_Page.textbox.Text;
                    Debug.WriteLine("To text einai: " + Home_Page.str);
                }
            }

            //   Debug.WriteLine("Teext: " + str);
            bool flag_room = false;

            int room_index = 0;

            for (int i = 0; i < Logic.rooms.Count; i++)
            {

                // user's input exists
                if (Home_Page.str.Equals(Logic.rooms[i].id))
                {

                    Debug.WriteLine("Room found!");
                    //  Floorplan.Children.Add(rooms[i].image);
                    Logic.rooms[i].image.Opacity = 1.0;
                    // Canvas.SetLeft(rooms[i].image, 60);
                    room_index = i;
                    flag_room = true;
                    break;

                }


            }

            for (int i = 0; i < Logic.rooms.Count; i++)
            {
                if (room_index == i) { continue; }
                Logic.rooms[i].image.Opacity = 0.0;
            }


            if (Home_Page.search_change)
            {
                if (flag_room == false)
                {
                    String str1 = "No results found for your research!";
                    String str2 = "Try again";
                    message_dialog(str1, str2);
                }
            }


            for (int i = 0; i < Graph.nodes.Count; i++)
            {
                if (Home_Page.str.Equals(Graph.nodes[i].name))
                {
                    continue_find_path(Graph.nodes[i].id);
                    update = true;
                    break;

                }
            }
            Home_Page.search_change = false;
        }

        public void continue_find_path(int destination)
        {


            //prepei na vrw ta 2 kontina nodes sto user's current location kai na ta valw sto neighbours tou.
            Debug.WriteLine("Times tou 0: " + Graph.nodes[0].x + Graph.nodes[0].y);
            for (int i = 1; i < Graph.nodes.Count; i++)
            {

                Graph.nodes[i].distfromsrc = (graph.calculate_distance(Graph.nodes[0].x, Graph.nodes[i].x, Graph.nodes[0].y, Graph.nodes[i].y));
                Debug.WriteLine("Graph node id: " + Graph.nodes[i].id + " DIstance from src " + Graph.nodes[i].distfromsrc);
            }
            Graph.nodes = Graph.nodes.OrderBy(x => x.distfromsrc).ToList();
            Graph.matrix[0, Graph.nodes[0].id] = Graph.nodes[0].distfromsrc;
            Graph.matrix[0, Graph.nodes[1].id] = Graph.nodes[1].distfromsrc;
            //  Debug.WriteLine("Min Value: " + graph.nodes[0].distfromsrc);
            //   Debug.WriteLine("Min Value: " + graph.nodes[1].distfromsrc);


            Debug.WriteLine("===== Graph with source node ====== ");
            for (int i = 0; i < 10; i++)

            {

                Debug.Write(Graph.matrix[0, i] + " ");
            }

            ShortestPath t = new ShortestPath();
            int src = 0;
            int dest = destination;
            t.dijkstra(Graph.matrix, src, dest);
            Debug.WriteLine("Dijksrtraa");
            t.printShortestPath(src, dest);
            Debug.WriteLine("Dijksrtraa");
            Graph.nodes = Graph.nodes.OrderBy(x => x.id).ToList();
            for (int i = 0; i < 10; i++)
            {
                Graph.matrix[0, i] = 0;

            }
            Debug.WriteLine("Dijksrtraa");
        }

        public void create_beacons()
        {

            // Create Beacon instances


            beacons.Add(new Beacon("78:A5:04:5B:35:EA", 24, 121, new Image())); //beacon 5
            beacons.Add(new Beacon("B4", 158, 230, new Image())); //beacon 4
            beacons.Add(new Beacon("78:A5:04:5B:35:90", 247, 272, new Image())); //beacon 3
            beacons.Add(new Beacon("78:A5:04:5B:2E:80", 158, 305, new Image())); //beacon 2
            beacons.Add(new Beacon("78:A5:04:5B:21:3C", 119, 532, new Image())); //beacon 1
        }

        public void create_rooms()
        {
            //Create Room Instances

            rooms.Add(new Room("Room 2", 199, 230, new Image()));
            rooms.Add(new Room("Kitchen", 243, 303, new Image()));
            rooms.Add(new Room("Room 1", 175, 450, new Image()));

        }

        // core function of the system running in a separate thread every x sec
        public void metrestopixels()
        {
            // **** Step 3: Begin Converting Metres to Pixels    ****    //



            Debug.WriteLine("=======  STEP 3 ========");

            for (int i = 0; i < beacons.Count; i++)

            {
                double sum = 0;
                // Debug.WriteLine("Beacon mac address: " +beacons[i].id);
                for (int j = 0; j < beacons[i].distances.Length; j++)

                {
                    Debug.WriteLine("Distance No " + i + ":  " + beacons[i].distances[j]);
                    // sum =sum +  beacons[i].distances[j];
                }

                //  beacons[i].final_distance = sum / beacons[i].mean_counter;
                Debug.WriteLine("Final Distance: " + beacons[i].final_distance);
                Debug.WriteLine("Mean: " + beacons[i].mean_counter);

                beacons[i].mean_counter = 0;
            }


            // **** Step 3: End Converting Metres to Pixels   ****    //


        }


        public void three_signals()
        {
            // **** Step 4: Begin Find 3 Strongest Signals  ****    //

            // find the 3 strongest signals and pass it as a parameters to trilateration algo
            Debug.WriteLine("=======  STEP 3 ========");

            for (int i = 0; i < beacons.Count; i++)

            {
                double sum = 0;
                // Debug.WriteLine("Beacon mac address: " +beacons[i].id);
                for (int j = 0; j < beacons[i].distances.Length; j++)

                {
                    Debug.WriteLine("Distance No " + i + ":  " + beacons[i].distances[j]);
                    // sum =sum +  beacons[i].distances[j];
                }

                //  beacons[i].final_distance = sum / beacons[i].mean_counter;
                Debug.WriteLine("Final Distance: " + beacons[i].final_distance);
                Debug.WriteLine("Mean: " + beacons[i].mean_counter);

                beacons[i].mean_counter = 0;
            }

            for (int i = 0; i < beacons.Count; i++)
            {
                Array.Sort(beacons[i].distances);
                Debug.WriteLine("Beacon id: " + beacons[i].id + " Final distance: " + beacons[i].distances[0]);
                if (Double.IsNaN(beacons[i].final_distance))
                {
                    Debug.WriteLine("?????????????????????????????????????????");
                    beacons[i].final_distance = 1000;

                }
                beacons[i].dist_pixel = (int)(beacons[i].distances[0] * 35); //  1 m =~ 35 pixels

            }
            beacons = beacons.OrderBy(x => x.distances[0]).ToList();
            // **** Step 4: End Find 3 Strongest Signals  ****    //
        }


        public async void checkblt()
        {
            bluetooth_flag = await GetBluetoothIsEnabledAsync();

        }

        public static async Task<bool> GetBluetoothIsEnabledAsync()
        {
            var radios = await Radio.GetRadiosAsync();
            var bluetoothRadio = radios.FirstOrDefault(radio => radio.Kind == RadioKind.Bluetooth);
            return bluetoothRadio != null && bluetoothRadio.State == RadioState.On;
        }
        public async void message_dialog(String str1, String str2)
        {

            // Create the message dialog and set its content
            var messageDialog = new MessageDialog(str1);

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                str2,
                new UICommandInvokedHandler(this.CommandInvokedHandler)));


            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();

        }

        // public delegate void UpdateBeaconNumber(string text);
        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            // MainPage.NotifyUser("The '" + command.Label + "' command has been selected.",
            //  NotifyType.StatusMessage);
        }

        public void clear()
        {
            Gui.lines.Clear();
            Gui.counter = 0;
            Graph.nodes.Clear();
            Logic.beacons.Clear();
            Logic.rooms.Clear();
            Home_Page.PeriodicTimer.Cancel();

        }

    }
}