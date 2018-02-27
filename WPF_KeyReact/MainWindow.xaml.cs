using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
        public MapManager mapManager;
        internal ConcurrentBag<KeyStuff> Controls = new ConcurrentBag<KeyStuff>();          //concurrent bag je neco jako list, ale je multi thread safe = da se s nim pracovat z vice vlaken aniz by to hazelo errory. Pouzivam ho zde kvuli asynchroni operaci v methode ModifyKeyState

        public DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(15) };

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
            
            this.KeyDown += (obj, args) => ModifyKeyState(args.Key, true);
            this.KeyUp += (obj, args) => ModifyKeyState(args.Key, false);
            
        }
        /// <summary>
        /// find key in controls and changes its state
        /// </summary>
        private void ModifyKeyState(Key key, bool b)
        {
            Task.Run(() => 
            {
                foreach (var control in Controls)
                {
                    if (control.key == key)
                        control.isDown = b;
                }
            });
             
        }

        /// <summary>
        /// načte objekty
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            car = new Car(this, ButtonCar, GridMain, Key.Left, Key.Right, Key.Up, Key.Down,"Player 1");
            mapManager = new MapManager(new System.Drawing.Size((int)(GridMain.ActualWidth), (int)(GridMain.ActualHeight)));

            Player1Points = HelpClass.Player1Points;
            Player2Points = HelpClass.Player2Points;
        }

        /// <summary>
        /// TODO - lepší zobrazení než MessageBox, př Page/ UserControl
        /// </summary>
        /// <param name="winner"></param>
        public void ShowWinner(Car winner)
        {
            MessageBox.Show("Vyhrál hráč: " + winner);
            this.Close();
        }
        
    }
}
