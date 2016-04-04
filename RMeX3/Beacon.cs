using System;
using Windows.UI.Xaml.Controls;

public class Beacon
{

   public  String id;
   public  int x;
   public  int y;
    public Image image;
    public double rssi;
    public double[] distances;
    public double final_distance;
    public int dist_pixel;
    public int mean_counter;

    public Beacon(String id, int x, int y, Image image)
    {
        this.id = id;
        this.x = x;
        this.y = y;
        this.image = image;
        this.mean_counter = 0;
        this.final_distance = 0.0;
        rssi = 0.0;
        //  distance = 0.0;

        dist_pixel = 0;
        this.distances = new Double[4]; 
        for (int i=0; i<this.distances.Length; i++)
        {
            this.distances[i] = 1000;

        }
        Windows.UI.Xaml.Media.Imaging.BitmapImage bi = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
        bi.UriSource = new Uri("ms-appx:/Assets/beacon.png", UriKind.RelativeOrAbsolute);
        image.Source = bi;
        image.Height = 15;
        image.Width = 15;
        Canvas.SetLeft(image,x);
        Canvas.SetTop(image, y);

       
        
    }
}
