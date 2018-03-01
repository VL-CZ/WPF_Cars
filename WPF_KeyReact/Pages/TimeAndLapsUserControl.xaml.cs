using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for TimeAndLapsUserControl.xaml
    /// </summary>
    public partial class TimeAndLapsUserControl : UserControl
    {
        public TimeAndLapsUserControl()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            HelpClass.Player1Points = TextBlockPlayer1;
            HelpClass.Player2Points = TextBlockPlayer2;
            HelpClass.Time = TextBlockTime;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int seconds = int.Parse(TextBlockTime.Text) + 1;
            TextBlockTime.Text = seconds.ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
