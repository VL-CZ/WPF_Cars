using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_KeyReact
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TextBlock Player1Points { get; set; }
        public static TextBlock Player2Points { get; set; }
        public static TextBlock Time { get; set; }
    }
}
