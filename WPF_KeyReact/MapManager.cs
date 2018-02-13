using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPF_KeyReact
{
    class MapManager
    {
        public Image Background { get; private set; }
        private string pathToFile;

        public MapManager()
        {
            Background = ImageLoader.LoadImage(new Size(60, 60), "background.png");
        }
    }
}
