using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AKG
{
    internal class View
    {
        MyModel Model { get; set; }
        MainWindow MainWindow { get; set; }
        public View(MyModel model, MainWindow window)
        {
            Model = model;
            MainWindow = window;
        }
        
        public void UpdateView()
        {
            MainWindow.ModelCanvas.Children.Clear();
            DrawingVertices();
        }

        private void DrawingVertices()
        {
            foreach (Vector4 point in Model.Vertices)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 2;
                ellipse.Height = 2;
                ellipse.Stroke = Brushes.White;
                ellipse.Fill = Brushes.White;

                Canvas.SetLeft(ellipse, point.X - 2 / 2);
                Canvas.SetTop(ellipse, point.Y - 2 / 2);

                MainWindow.ModelCanvas.Children.Add(ellipse);
            }
        }
    }
}
