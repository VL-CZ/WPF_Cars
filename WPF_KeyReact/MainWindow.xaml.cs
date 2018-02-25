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
    
    public partial class MainWindow : Window
    {
        
        private Car car;
        private MapManager mapManager;
        internal List<KeyStuff> Controls = new List<KeyStuff>();
        private bool upDown, downDown, leftDown, rightDown = false;

        DispatcherTimer MoveTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(10) };
        DispatcherTimer ReadInputTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(50)};

        public MainWindow()
        {
            InitializeComponent();
            car = new Car(this, ButtonCar, GridMain, Key.Left, Key.Right, Key.Up, Key.Down);
            mapManager = new MapManager(new System.Drawing.Size((int)(GridMain.ActualWidth), (int)(GridMain.ActualHeight)));

            this.KeyDown += (obj, args) => ModifyKeyState(args.Key, true);
            this.KeyUp += (obj, args) => ModifyKeyState(args.Key, false);
            
        }
        /// <summary>
        /// find key in controls and changes its state
        /// </summary>
        private void ModifyKeyState(Key key, bool b)
        {
            foreach (var control in Controls)
            {
                if (control.key == key)
                    control.isDown = b;
            }
        }

        
        /// <summary>
        /// načte nové objekty
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveTimer.Start();
            ReadInputTimer.Start();
        }
    }
}
