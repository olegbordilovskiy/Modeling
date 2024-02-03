using AKG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace akg_lab1
{
    internal class Controller
    {
        public MainWindow MainWindow { get; set; }
        public MyModel Model { get; set; }
        public View View { get; set; }
        public Controller(MainWindow window, MyModel model, View view)
        {
            MainWindow = window;
            Model = model;
            View = view;
        }
        public void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.Up)
                {
                    Model.scaleX += 0.1f;
                    Model.scaleY += 0.1f;
                    Model.scaleZ += 0.1f;

                    Model.UpdateModel();
                    View.UpdateView();
                }

                if (e.Key == Key.Down)
                {
                    Model.scaleX -= 0.1f;
                    Model.scaleY -= 0.1f;
                    Model.scaleZ -= 0.1f;

                    Model.UpdateModel();
                    View.UpdateView();
                }
            }

            if (e.Key == Key.Right)
            {
                Model.translationX += 10.0f;
                Model.UpdateModel();
                View.UpdateView();
            }

            if (e.Key == Key.Left)
            {
                Model.translationX -= 10.0f;
                Model.UpdateModel();
                View.UpdateView();
            }

            if (e.Key == Key.Up)
            {
                Model.translationY += 5.0f;
                Model.UpdateModel();
                View.UpdateView();
            }

            if (e.Key == Key.Down)
            {
                Model.translationY -= 5.0f;
                Model.UpdateModel();
                View.UpdateView();
            }


        }
    }
}
