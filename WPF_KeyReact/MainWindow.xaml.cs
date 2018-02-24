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
using System.Windows.Threading;

namespace WPF_KeyReact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// auto
        /// </summary>
        private Car car;

        /// <summary>
        /// mapManager
        /// </summary>
        private MapManager mapManager;

        /// <summary>
        /// pomocné proměnné
        /// </summary>
        private bool upDown, downDown, leftDown, rightDown = false;

        /// <summary>
        /// reference na UserControl elementy
        /// </summary>
        public TextBlock Player1Points { get; set; }
        public TextBlock Player2Points { get; set; }

        /// <summary>
        /// kostruktor okna
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.KeyUp += new KeyEventHandler(MainWindow_KeyUp);
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(10)
            };
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        #region KeyUpDown Methods

        /// <summary>
        /// stisk tlačítka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    downDown = true;
                    break;
                case Key.Up:
                    upDown = true;
                    break;
                case Key.Left:
                    leftDown = true;
                    break;
                case Key.Right:
                    rightDown = true;
                    break;
            }
        }

        /// <summary>
        /// puštění tlačítkas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    downDown = false;
                    break;
                case Key.Up:
                    upDown = false;
                    break;
                case Key.Left:
                    leftDown = false;
                    break;
                case Key.Right:
                    rightDown = false;
                    break;
            }
        }

        #endregion

        /// <summary>
        /// reaguje na stisk klávesy, pohne autem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            Thickness margin = ButtonCar.Margin;

            if (upDown)
            {
                Tuple<double, double> margins = car.CountMargin();

                if (mapManager.PixelIsEmptyOrFinish(car.LeftFrontCorner) && mapManager.PixelIsEmptyOrFinish(car.RightFrontCorner))
                {
                    margin.Top += margins.Item1 / 2;
                    margin.Bottom -= margins.Item1 / 2;
                    margin.Left += margins.Item2 / 2;
                    margin.Right -= margins.Item2 / 2;
                }
                else
                    car.RestorePrevious();
            }
            if (leftDown)
                car.Angle -= Car.rotationAngle;
            if (rightDown)
                car.Angle += Car.rotationAngle;


            ButtonCar.Margin = margin;
            ButtonCar.RenderTransform = new RotateTransform(car.Angle);

            CheckIfFinish();
        }

        /// <summary>
        /// načte objekty
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GeneralTransform generalTransform1 = ButtonCar.TransformToAncestor(GridMain);
            Point point = generalTransform1.Transform(new Point(0, 0));
            car = new Car(point, ButtonCar.Height, ButtonCar.Width);
            mapManager = new MapManager(new System.Drawing.Size((int)(GridMain.ActualWidth), (int)(GridMain.ActualHeight)));

            Player1Points = HelpClass.Player1Points;
            Player2Points = HelpClass.Player2Points;
        }

        /// <summary>
        /// zkontroluje, jestli není v cíli (nepřičte body při zpětném průchodu)
        /// </summary>
        private void CheckIfFinish()
        {
            if ((mapManager.PixelIsEmptyOrFinish(car.LeftFrontCorner, true) || mapManager.PixelIsEmptyOrFinish(car.RightFrontCorner, true))
                && car.LeftFrontCorner.X<car.Center.X)
            {
                if (!car.InFinish)
                {
                    int points = int.Parse(Player1Points.Text) + 1;
                    Player1Points.Text = points.ToString();

                    if (points >= 5)
                    {
                        MessageBox.Show("Hráč 1 vyhrál");
                        this.Close();
                    }
                }
                car.InFinish = true;
            }
            else
                car.InFinish = false;
        }
    }
}
