using System;
using System.Diagnostics;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace RMeX3
{

    public sealed partial class Home_Page : Page
    {
        
        public static string str;    //store room search box  
        public int counter = 0; //checks if map was created (helps to remove and add elements)
        public static int c = 0;
        public static int lines_counter = 0; // number of lines that were created
        public static Image marker = new Image(); 
        public static int  scan_counter = 1;  //number of scans
        public static int current_x = 0; //user's x coordinate current location
        public static int current_y = 0; //user's y coordinate current location
        public static TextBox textbox; // searchbox
        public static Canvas floorplan; //floor image
        public static  TextBlock text1; //distance text
        public static TextBlock text2;  //time text
        public static bool btnclick_flag = false; //checks if button was pressed the very first time
        public static bool search_change = false; //checks if button was pressed
        public static ThreadPoolTimer PeriodicTimer;  // Thread instance
        Logic l = Logic.getInstance(); // Singleton Logic instance
        public Gui g = Gui.getInstance(); // Singleton Gui instance

        public Home_Page()
        {
            this.InitializeComponent();
            Debug.WriteLine("**************************************************************");
            Debug.WriteLine("Children:  " + Floorplan.Children.Count);
            Debug.WriteLine("Counter:  " + counter);
            floorplan = Floorplan;        
            text1 = textBlock_1;          
            text2 = textBlock_2;
            textbox = tb;       
            l.create_logic(); //create logic of the program
            g.bluetooth_detection(); //detect if bluetooth is on/off
            g.create_gui(); //create gui of the program
            run_thread(); //run core thread forever
            
        }


        private async void run_thread()
        {

            try
            {

               // Debug.WriteLine("====   Start ScanBeacon    ====");
                TimeSpan period2 = TimeSpan.FromMilliseconds(800);
                                            
                PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(async (source) =>
                {

                    if (scan_counter == 4)
                    {
                   
                        l.scan(); //step 1
                         
                        await Dispatcher.RunAsync(CoreDispatcherPriority.High,
                                      () =>
                                      {
                                          l.trilaterate(); //step 5
                                          g.display_user();

                                          // user's input doesnt exists
                                          if (btnclick_flag)
                                          {

                                              l.find_path(); //step 6 
                                              if (Logic.update) {
                                                  g.display_path(); 
                                                  g.display_time();
                                              }
                                          }
                                          scan_counter = 1;


                                      });
                    }
                    scan_counter++;
                }, period2);
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("Catch");
            }

        }

     
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            search_change = true;
            btnclick_flag = true;
            for (int i = 0; i < 10; i++)
            {
                Graph.matrix[0, i] = 0;
            }
        }

        

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            l.clear();
            Frame.Navigate(typeof(Home_Page));
        }
        private void AudioGuidance_Button_Click(object sender, RoutedEventArgs e)
        {
            l.clear();                    
            Frame.Navigate(typeof(AudioGuidance_Page));
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            l.clear();
            Frame.Navigate(typeof(Settings_page));
        }

        private void Feedback_Button_Click(object sender, RoutedEventArgs e)
        {
            l.clear();
            Frame.Navigate(typeof(Feedback_Page));
        }

        private void About_Button_Click(object sender, RoutedEventArgs e)
        {
            l.clear();
            Frame.Navigate(typeof(About_Page));
        }

        private void SignOut_Button_Click(object sender, RoutedEventArgs e)
        {
            l.clear();
            Frame.Navigate(typeof(MainPage));
        }

        private void PrivacyPolicy_Button_Click(object sender, RoutedEventArgs e)
        {
            l.clear();
            Frame.Navigate(typeof(PrivacyPolicy_page));
        }

        private void TermsofService_Button_Click(object sender, RoutedEventArgs e)
        {
            l.clear();
            Frame.Navigate(typeof(TermsOfService_Page));
        }
    }

}
