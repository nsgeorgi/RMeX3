using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace RMeX3
{
    public class Gui
    {
        public static  List<Line> lines = new List<Line>();
        public Logic l;
        public static int lines_counter = 0;
        public static float total_distance = 0;
        public static float total_time;
        public static  int counter = 0;
        private static Gui instance = new Gui();

        private Gui() { }

        public static Gui getInstance()
        {
            return instance;
        }

        public void create_gui()
        {
            //Create Line Instances
            Debug.WriteLine("****   Stage 1.1  ***");
            for (int i = 0; i < 1000; i++)
            {
                lines.Add(new Line(0, 0, new Image()));

            }
            Debug.WriteLine("****   Stage 1.1  ***");
            for (int i = 0; i < Logic.rooms.Count; i++)
            {

                Logic.rooms[i].image.Opacity = 0.0;
                Home_Page.floorplan.Children.Add(Logic.rooms[i].image); //add rooms to the floorplan

            }
            
            for (int i = 0; i < Graph.nodes.Count; i++)
            {
                
                Graph.nodes[i].image.Opacity = 0.0;
                
                Home_Page.floorplan.Children.Add(Graph.nodes[i].image);  // add nodes to the floorplan
               
            }
            Debug.WriteLine("****   Stage 1.1  ***");

            for (int i = 0; i < Logic.beacons.Count; i++)
            {
                Home_Page.floorplan.Children.Add(Logic.beacons[i].image);  // add beacons to the floorplan
                             
            }

            Debug.WriteLine("****   Stage 1.2  ***");
            for (int i = 0; i < lines.Count; i++)
            {

                lines[i].image.Opacity = 0.0;
                Home_Page.floorplan.Children.Add(lines[i].image); //add lines to the floorplan

            }
            Debug.WriteLine("****   Stage 1.3  ***");
        }

        public void display_user()
        {
            int current_x = Logic.current_x;
            int current_y = Logic.current_y;

            // **** Step 6: Begin Creating UI ****    //

            if (counter != 0)
            {
                Debug.WriteLine("***  Debug 2  ****");
                Home_Page.floorplan.Children.RemoveAt(1025);


            }
            //Random rnd = new Random();
            //current_x = rnd.Next(0, 200);
            // current_y = rnd.Next(20, 300);
            Debug.WriteLine("***  Debug  3 ****");
            //  Button b = (Button)sender;
            //  b.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);
            Image marker = new Image();

            Windows.UI.Xaml.Media.Imaging.BitmapImage b2 = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            b2.UriSource = new Uri("ms-appx:/Assets/15.gif", UriKind.RelativeOrAbsolute);

            marker.Source = b2;
            //marker.Height = 1;
            marker.Width = 25; //15
            Canvas.SetLeft(marker, current_x);
            Canvas.SetTop(marker, current_y);
            marker.Opacity = 1.0;
            Home_Page.floorplan.Children.Add(marker);
            counter++;
            
            // **** Step 6: End Creating UI ****    //

        }
        public void display_path()
        {


            


            // **** Step 8: Start Display Path ****    //


            // if node1.x == node2.x && node1.y > node2.y then line is vertical (90 degrees angle) if node1.y1 < node2.y2 (-90 degrees angle)
            // if node1.y1 == node2.y2  &&  node1.x1 > node2.x2 then line horizontal (0 degrees) if node1.x1 < node2.x2 (180 degrees)

            //ta extra nodes ta exw valei gt gia paradeigma an to destination exei block mprosta i prepei na peraseis apo gwnia tote prepe
            //na exeis kapoia voithika nodes.ama thelw kapoia ta afairw xwris na epireastei to programma.
            Debug.WriteLine("Line Counter:  " + lines_counter);
          

            if (lines_counter != 0)
            {
                for (int i = 0; i < lines_counter; i++)
                    lines[i].image.Opacity = 0.0;
            }

            for (int i = ShortestPath.shortestPath.Count - 1; i > 0; i--)
            {
                int diff_top = 0;


                Debug.Write(ShortestPath.shortestPath[i]);
                if (Graph.nodes[ShortestPath.shortestPath[i]].x == Graph.nodes[ShortestPath.shortestPath[i - 1]].x && Graph.nodes[ShortestPath.shortestPath[i]].y > Graph.nodes[ShortestPath.shortestPath[i - 1]].y)
                {
                    Debug.WriteLine("Nodes are: " + ShortestPath.shortestPath[i] + " , " + ShortestPath.shortestPath[i - 1]);
                    diff_top = Graph.nodes[ShortestPath.shortestPath[i - 1]].y - Graph.nodes[ShortestPath.shortestPath[i]].y;
                    lines[lines_counter].image.Width = Math.Abs(diff_top);
                    RotateTransform rt1 = new RotateTransform();
                    rt1.Angle = -90;

                    lines[lines_counter].image.RenderTransform = rt1;
                    Canvas.SetLeft(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].x + 9);
                    Canvas.SetTop(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].y + 13);
                    lines[lines_counter].image.Opacity = 1.0;
                    lines_counter++;
                }

                else if (Graph.nodes[ShortestPath.shortestPath[i]].x == Graph.nodes[ShortestPath.shortestPath[i - 1]].x && Graph.nodes[ShortestPath.shortestPath[i]].y < Graph.nodes[ShortestPath.shortestPath[i - 1]].y)
                {
                    Debug.WriteLine("Nodes are: " + ShortestPath.shortestPath[i] + " , " + ShortestPath.shortestPath[i - 1]);
                    diff_top = Graph.nodes[ShortestPath.shortestPath[i]].y - Graph.nodes[ShortestPath.shortestPath[i - 1]].y;
                    lines[lines_counter].image.Width = Math.Abs(diff_top);
                    RotateTransform rt1 = new RotateTransform();
                    rt1.Angle = 90;

                    lines[lines_counter].image.RenderTransform = rt1;
                    Canvas.SetLeft(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].x + 9);
                    Canvas.SetTop(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].y + 13);
                    lines[lines_counter].image.Opacity = 1.0;
                    lines_counter++;
                }

                else if (Graph.nodes[ShortestPath.shortestPath[i]].y == Graph.nodes[ShortestPath.shortestPath[i - 1]].y && Graph.nodes[ShortestPath.shortestPath[i]].x > Graph.nodes[ShortestPath.shortestPath[i - 1]].x)
                {
                    Debug.WriteLine("Nodes are: " + ShortestPath.shortestPath[i] + " , " + ShortestPath.shortestPath[i - 1]);
                    diff_top = Graph.nodes[ShortestPath.shortestPath[i]].x - Graph.nodes[ShortestPath.shortestPath[i - 1]].x;
                    lines[lines_counter].image.Width = Math.Abs(diff_top);
                    RotateTransform rt1 = new RotateTransform();


                    lines[lines_counter].image.RenderTransform = rt1;
                    Canvas.SetLeft(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].x + 9);
                    Canvas.SetTop(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].y + 13);
                    lines[lines_counter].image.Opacity = 1.0;
                    lines_counter++;
                }
                else if (Graph.nodes[ShortestPath.shortestPath[i]].y == Graph.nodes[ShortestPath.shortestPath[i - 1]].y && Graph.nodes[ShortestPath.shortestPath[i]].x < Graph.nodes[ShortestPath.shortestPath[i - 1]].x)
                {
                    Debug.WriteLine("Nodes are: " + ShortestPath.shortestPath[i] + " , " + ShortestPath.shortestPath[i - 1]);
                    diff_top = Graph.nodes[ShortestPath.shortestPath[i - 1]].x - Graph.nodes[ShortestPath.shortestPath[i]].x;
                    lines[lines_counter].image.Width = Math.Abs(diff_top);
                    RotateTransform rt1 = new RotateTransform();


                    lines[lines_counter].image.RenderTransform = rt1;
                    Canvas.SetLeft(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].x + 9);
                    Canvas.SetTop(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].y + 13);
                    lines[lines_counter].image.Opacity = 1.0;
                    lines_counter++;
                }
                else
                {
                    Debug.WriteLine("Nodes are: " + ShortestPath.shortestPath[i] + " , " + ShortestPath.shortestPath[i - 1]);
                    diff_top = Graph.nodes[ShortestPath.shortestPath[i]].y - Graph.nodes[ShortestPath.shortestPath[i - 1]].y;

                    if (diff_top < 0) // user location   up (1)case
                    {

                        lines[lines_counter].image.Width = Math.Abs(diff_top);
                        total_distance = total_distance + (float)lines[lines_counter].image.Width;
                        RotateTransform rt1 = new RotateTransform();
                        rt1.Angle = 90;

                        lines[lines_counter].image.RenderTransform = rt1;
                        Canvas.SetLeft(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].x + 9);
                        Canvas.SetTop(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].y + 13);
                        lines[lines_counter].image.Opacity = 1.0;
                        lines_counter++;

                        lines[lines_counter].image.Width = Math.Abs(Graph.nodes[ShortestPath.shortestPath[i - 1]].x - Graph.nodes[ShortestPath.shortestPath[i]].x + 9);
                        total_distance = total_distance + (float)lines[lines_counter].image.Width;
                        Canvas.SetLeft(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].x + 9);   // Room 1 (225,527)
                        Canvas.SetTop(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].y + 13 + lines[lines_counter - 1].image.Width);
                        lines[lines_counter].image.Opacity = 1.0;
                        lines_counter++;

                    }

                    else   // user location   down (2)case
                    {

                        lines[lines_counter].image.Width = Math.Abs(diff_top);
                        total_distance = total_distance + (float)lines[lines_counter].image.Width;
                        RotateTransform rt1 = new RotateTransform();
                        rt1.Angle = -90;

                        lines[lines_counter].image.RenderTransform = rt1;
                        Canvas.SetLeft(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].x + 9);
                        Canvas.SetTop(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].y + 13);
                        lines[lines_counter].image.Opacity = 1.0;
                        lines_counter++;
                        lines[lines_counter].image.Width = Math.Abs(Graph.nodes[ShortestPath.shortestPath[i - 1]].x - Graph.nodes[ShortestPath.shortestPath[i]].x + 9);
                        total_distance = total_distance + (float)lines[lines_counter].image.Width;
                        Canvas.SetLeft(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].x + 9);  // Room 1 (225,527)
                        Canvas.SetTop(lines[lines_counter].image, Graph.nodes[ShortestPath.shortestPath[i]].y + 13 - lines[lines_counter - 1].image.Width);
                        lines[lines_counter].image.Opacity = 1.0;
                        lines_counter++;


                    }



                }
                Debug.WriteLine("END");
            }
            
            // **** Step 8: End Display Path ****    //

        }

        public void display_time ()
        {


            // **** Step 8: Start Display Distance and Time ****    //

           // total_distance = 0;
            for (int i = ShortestPath.shortestPath.Count - 3; i >= 0; i--)
            {

                total_distance = total_distance + Graph.nodes[ShortestPath.shortestPath[i]].distfromsrc;
            }

            total_distance = total_distance / 35;
            
            total_time = (total_distance / 2) / 60;
           
            total_time.ToString("0.00");
            string str1 = total_time.ToString("0.00"); ;
            string str2 = total_distance.ToString("0.00");
            Home_Page.text1.Text = "Distance:" + str2 + " m";
            Home_Page.text2.Text = "Time:" + str1 + " min";

            // **** Step 8: End Display Path ****    //


            ShortestPath.shortestPath.Clear();
        }

        public void bluetooth_detection()
        {

            //bluetooth detection

            //bluetooth detection
            if (Logic.bluetooth_flag == false)
            {

                Debug.WriteLine(" ****** NO BLUETOOTH *****");
                String str1 = "You must enable bluetooth to start the navigation!";
                String str2 = "Ok";
                message_dialog(str1, str2);
            }
            else {

                Debug.WriteLine(" ****** YES BLUETOOTH *****");
                String str1 = "Bluetooth is on";
                String str2 = "Ok";
                // message_dialog(str1, str2);
            }


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
    }
}
