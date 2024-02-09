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
            //DrawLineGrid();
            //DrawDDAGrid();
        }

        private void DrawVertices()
        {
            foreach (Vector4 point in Model.Vertices)
            {
                DrawPoint(point, 2);
            }

        }
        private void DrawLineGrid()
        {

            foreach (var face in Model.SourceFaces)
            {
                for (int i = 0; i < face.Length - 1; i++)
                {
                    DrawLine(MainWindow.ModelCanvas, Model.Vertices[face[i]], Model.Vertices[face[i + 1]]);
                }
            }

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
        }

        private void DrawDDAGrid()
        {
            List<Vector4> DDAVertices = new List<Vector4>();

            foreach (int[] face in Model.SourceFaces)
            {
                for (int i = 0; i < face.Length - 1; i++)
                {
                    Rasterization(Model.Vertices[face[i]], Model.Vertices[face[i + 1]]);
                }
                Rasterization(Model.Vertices[face[face.Length - 1]], Model.Vertices[face[0]]); //??
            }

            foreach (Vector4 DDAVertex in DDAVertices)
            {
                DrawPoint(DDAVertex, 2);
            }

            void Rasterization(Vector4 point1, Vector4 point2)
            {
                float dx = point2.X - point1.X;
                float dy = point2.Y - point1.Y;

                int steps = Math.Abs((int)(dx > dy ? dx : dy))/1;


                float xIncrement = dx / steps;
                float yIncrement = dy / steps;

                float x = point1.X;
                float y = point1.Y;


                for (int i = 0; i < steps; i++)
                {
                    var vertix = new Vector4(x, y, 0, 0);
                    DDAVertices.Add(vertix);

                    x += xIncrement;
                    y += yIncrement;
                }
            }
        }

        private void DrawPoint(Vector4 point, double size)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = size;
            ellipse.Height = size;
            ellipse.Stroke = Brushes.White;
            ellipse.Fill = Brushes.White;

            Canvas.SetLeft(ellipse, point.X - size / 2);
            Canvas.SetTop(ellipse, point.Y - size / 2);

            MainWindow.ModelCanvas.Children.Add(ellipse);
        }

    }
}
