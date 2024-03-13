using AKG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AKG
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
            switch (e.Key)
            {
                case Key.Up:

                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        Model.rotationXAngleRad += 0.10f;
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.Shift)
                    {
                        Model.scaleX += 0.05f;
                        Model.scaleY += 0.05f;
                        Model.scaleZ += 0.05f;
                    }
                    else
                    {
                        Model.translationY += 0.1f;
                    }
                    break;

                case Key.Down:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        Model.rotationXAngleRad -= 0.10f;
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.Shift)
                    {
                        Model.scaleX -= 0.05f;
                        Model.scaleY -= 0.05f;
                        Model.scaleZ -= 0.05f;
                    }
                    else
                    {
                        Model.translationY -= 0.1f;
                    }
                    break;

                case Key.Right:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        Model.rotationYAngleRad += 0.10f;
                    }
                    else
                    {
                        Model.translationX += 0.1f;
                    }
                    break;

                case Key.Left:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        Model.rotationYAngleRad -= 0.10f;
                    }
                    else
                    {
                        Model.translationX -= 0.1f;
                    }
                    break;
            }

            Model.UpdateModel();
            View.UpdateView();
        }
    }
}
