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
using System.Windows.Media.Imaging;

namespace AKG
{
    internal class View
    {
        MyModel Model { get; set; }
        MainWindow MainWindow { get; set; }
        //WriteableBitmap bitmap = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgr32, null);
        private long Time { get; set; }

        public View(MyModel model, MainWindow window)
        {
            Model = model;
            MainWindow = window;
        }

        public void UpdateView()
        {
            MainWindow.ModelCanvas.Children.Clear();
            Start();
            DrawDDAGrid();
            DrawParameters();
        }


        private void DrawDDAGrid()
        {
            List<Vector4> DDAVertices = new List<Vector4>();

            int height = (int)MainWindow.Height;
            int width = (int)MainWindow.Width;

            var pixelData = new byte[width * height * 4]; // 4 байта на каждый пиксель (BGRA)

            Parallel.For(0, Model.SourceFaces.Count, i =>
            {
                int[] face = Model.SourceFaces[i];
                for (int j = 0; j < face.Length - 1; j++)
                {
                    Rasterization(Model.Vertices[face[j]], Model.Vertices[face[j + 1]]);
                }
                Rasterization(Model.Vertices[face[face.Length - 1]], Model.Vertices[face[0]]);
            });

            WriteableBitmap bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            Int32Rect rect = new Int32Rect(0, 0, width, height);
            bitmap.WritePixels(rect, pixelData, width * 4, 0);

            Image image = new Image();
            image.Source = bitmap;
            MainWindow.ModelCanvas.Children.Add(image);

            void Rasterization(Vector4 point1, Vector4 point2)
            {
                float dx = point2.X - point1.X;
                float dy = point2.Y - point1.Y;

                int steps = Math.Abs((int)(dx > dy ? dx : dy));

                float xIncrement = dx / steps;
                float yIncrement = dy / steps;

                float x = point1.X;
                float y = point1.Y;

                for (int i = 0; i < steps; i++)
                {
                    int index = ((int)y * width + (int)x) * 4;

                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        lock (pixelData)
                        {
                            pixelData[index + 0] = 255; 
                            pixelData[index + 1] = 255; 
                            pixelData[index + 2] = 255; 
                            pixelData[index + 3] = 255; 
                        }
                    }

                    x += xIncrement;
                    y += yIncrement;
                }
            }
        }



        private void DrawParameters()
        {
            MainWindow.Delay.Text = $"Delay: {TimeSpan.FromTicks(Stop()).TotalMilliseconds} ms";
            MainWindow.Vertices.Text = $"Vertices: {Model.Vertices.Count}";
            MainWindow.Faces.Text = $"Faces: {Model.SourceFaces.Count}";
            
        }

        public void Start()
        {
            Time = DateTime.Now.Ticks;
        }

        public long Stop()
        {
            long endTimeTicks = DateTime.Now.Ticks;
            long elapsedTicks = endTimeTicks - Time;
            return elapsedTicks;
        }
    }
}
