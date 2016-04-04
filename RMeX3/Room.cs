using System;
using Windows.UI.Xaml.Controls;

public class Room
{

  public  String id;
    public int x;
   public  int y;
    public Image image;

    public Room(String id, int x ,int y, Image image)
	{
        this.id = id;
        this.x = x;
        this.y = y;
        this.image = image;
        Windows.UI.Xaml.Media.Imaging.BitmapImage bi = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
        bi.UriSource = new Uri("ms-appx:/Assets/marker.png", UriKind.RelativeOrAbsolute);
        image.Source = bi;
        image.Height = 15;
        image.Width = 15;
        Canvas.SetLeft(image, x);
        Canvas.SetTop(image, y);
    }
}
