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
using akg_lab1;



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
            model.UpdateModel();

            View view = new View(model, this);
            view.UpdateView();

            Controller controller = new Controller(this, model, view);

            //PreviewKeyDown += MainWindow_PreviewKeyDown;
            PreviewKeyDown += controller.HandleKeyDown;
        }
    }
}