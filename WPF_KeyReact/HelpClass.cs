using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_KeyReact
{
    static class HelpClass
    {
        /// <summary>
        /// předává kontrolky z UserControl na MainWindow
        /// </summary>
        public static TextBlock Player1Points { get; set; }
        public static TextBlock Player2Points { get; set; }
        public static TextBlock Time { get; set; }
    }
}
