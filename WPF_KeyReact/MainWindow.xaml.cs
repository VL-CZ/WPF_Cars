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

        /// <summary>
        /// kostruktor okna
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            StartPage sp = new StartPage();
            ContentFrame.NavigationService.Navigate(sp);
        }
    }
}
