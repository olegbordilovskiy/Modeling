using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;



namespace AKG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Parser parser = new Parser();
            parser.ParseFile("cat.obj");


            MyModel model = new(parser.Vertices, parser.Faces);

          

            //DrawPoints(transformedVertices);

        }
        private void DrawPoints(List<Vector4> points)
        {
            foreach (Vector4 point in points)
            {
                DrawEllipse(point.X, point.Y, 2, 2); 
            }
        }

        private void DrawEllipse(float x, float y, float width, float height)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = width;
            ellipse.Height = height;
            ellipse.Stroke = Brushes.White;
            ellipse.Fill = Brushes.White;

            // Устанавливаем координаты центра эллипса
            Canvas.SetLeft(ellipse, x - width / 2);
            Canvas.SetTop(ellipse, y - height / 2);

            // Добавляем эллипс на Canvas
            Model.Children.Add(ellipse);
        }

    }
}