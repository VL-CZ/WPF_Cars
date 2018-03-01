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
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public StatisticsPage(int time, Car winner, Car p1, Car p2)
        {
            InitializeComponent();

            TimeTextBlock.Text = time.ToString();
            P1TextBlock.Text = p1.Points.ToString();
            // P2TextBlock.Text = p2.Points.ToString();

            WinnerTextBlock.Text = p1 == winner ? p1.ToString() : p2.ToString();
        }

        private void NewGameButon_Click(object sender, RoutedEventArgs e)
        {
            GamePage gamePage = new GamePage();
            this.NavigationService.Navigate(gamePage);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}
