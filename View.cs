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
            DrawVertices();
            DrawGrid();
        }

        private void DrawVertices()
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

            //foreach (Vector4[] ar in Model.DDAFaces)
            //{
            //    foreach (Vector4 point in ar)
            //    {
            //        Ellipse ellipse = new Ellipse();
            //        ellipse.Width = 2;
            //        ellipse.Height = 2;
            //        ellipse.Stroke = Brushes.White;
            //        ellipse.Fill = Brushes.White;

            //        Canvas.SetLeft(ellipse, point.X - 2 / 2);
            //        Canvas.SetTop(ellipse, point.Y - 2 / 2);

            //        MainWindow.ModelCanvas.Children.Add(ellipse);
            //    }
            //}
        }
        private void DrawGrid()
        {
            void DrawLine(Canvas canvas, Vector4 point1, Vector4 point2)
            {
                Line line = new Line
                {
                    X1 = point1.X,
                    Y1 = point1.Y,
                    X2 = point2.X,
                    Y2 = point2.Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 0.15
                };

                canvas.Children.Add(line);
            }
            if (Model.SourceFaces == null || Model.DDAFaces == null)
                return;

           

            foreach (var face in Model.DDAFaces)
            {
                for (int i = 0; i < face.Length -1 ; i++)
                {
                 
                    DrawLine(MainWindow.ModelCanvas, face[i], face[i+1]);
                }
            }
        }


    }
}
