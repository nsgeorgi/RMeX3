using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace RMeX3
{


 public   class Node
    {
        public int id;
        public int x;
        public int y;
        public string name;
        public Image image;
        public int distfromsrc;
       public  List<int> neighbours;
   //     public List neighbours[];





        public Node(int id, int x, int y,string name, Image image)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.image = image;
            this.name = name;
            this.distfromsrc = 1000;
            Windows.UI.Xaml.Media.Imaging.BitmapImage bi = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            bi.UriSource = new Uri("ms-appx:/Assets/current_loc.png", UriKind.RelativeOrAbsolute);
            image.Source = bi;
            image.Height = 15;
            image.Width = 15;
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);

        }
    }
}
