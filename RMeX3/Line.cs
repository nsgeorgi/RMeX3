using System;
using Windows.UI.Xaml.Controls;

public class Line
{

   
    public int x;
    public int y;
    public Image image;


    public Line( int x, int y, Image image)
    {
        
        this.x = x;
        this.y = y;
        this.image = image;
        Windows.UI.Xaml.Media.Imaging.BitmapImage bi = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
        bi.UriSource = new Uri("ms-appx:/Assets/line3.png", UriKind.RelativeOrAbsolute);
        image.Source = bi;
      //  image.Height =10;
        image.Width = 200;
        Canvas.SetLeft(image, x);
        Canvas.SetTop(image, y);



    }
}

