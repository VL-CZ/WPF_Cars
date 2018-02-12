using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_KeyReact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// transformer
        /// </summary>
        private Transformer transformer;

        /// <summary>
        /// kostruktor okna
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        /// <summary>
        /// reaguje na stisk klávesy, pohne tlačítkem 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Thickness margin = ButtonCar.Margin;

            GeneralTransform generalTransform1 = ButtonCar.TransformToAncestor(GridMain);
            Point point = generalTransform1.Transform(new Point(0, 0));

            if (transformer == null)
                transformer = new Transformer(point, ButtonCar.Height, ButtonCar.Width);

            switch (e.Key)
            {
                case Key.Up:
                    Tuple<double, double> margins = transformer.CountMargin();

                    if (IsInside(margin.Top + margins.Item1, margin.Left + margins.Item2))
                    {
                        margin.Top += margins.Item1 / 2;
                        margin.Bottom -= margins.Item1 / 2;
                        margin.Left += margins.Item2 / 2;
                        margin.Right -= margins.Item2 / 2;
                    }
                    break;
                case Key.Left:
                    transformer.Angle -= Transformer.rotationAngle;
                    break;
                case Key.Down:
                    break;
                case Key.Right:
                    transformer.Angle += Transformer.rotationAngle;
                    break;
                default:
                    return;
            }

            ButtonCar.Margin = margin;
            ButtonCar.RenderTransform = new RotateTransform(transformer.Angle);

        }

        /// <summary>
        /// zkotroluje, jestli nevyjde ven z okna
        /// </summary>
        /// <param name="topMargin"></param>
        /// <param name="leftMargin"></param>
        /// <returns></returns>
        private bool IsInside(double topMargin, double leftMargin)
        {
            return true;
        }

    }
}
