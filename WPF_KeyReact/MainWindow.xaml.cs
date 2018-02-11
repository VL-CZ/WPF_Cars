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
        private Transformer transformer;

        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            transformer = new Transformer();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Thickness margin = ButtonCar.Margin;

            switch (e.Key)
            {
                case Key.Up:
                    Tuple<double, double> margins = transformer.CountMargin();
                    margin.Top += margins.Item1;
                    margin.Left += margins.Item2;
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
    }
}
