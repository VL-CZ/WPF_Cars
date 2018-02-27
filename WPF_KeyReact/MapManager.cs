using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WPF_KeyReact
{
    public class MapManager
    {
        /// <summary>
        /// background
        /// </summary>
        public Bitmap Background { get; private set; }

        /// <summary>
        /// barva okolí dráhy
        /// </summary>
        private Color areaColor = Color.FromArgb(255, 255, 255);
        

        /// <summary>
        /// Constructor
        /// </summary>
        public MapManager(Size size)
        {
            Image tmp = new Bitmap("../../Images/background_track.png");

            Background = new Bitmap(tmp, size);
        }

        /// <summary>
        /// rozhodne, jestli je pixel volný
        /// </summary>
        public bool PixelIsEmpty(System.Windows.Point point)
        {
            int RoundX = (int)Math.Round(point.X);
            int RoundY = (int)Math.Round(point.Y);
            return Background.GetPixel(RoundX, RoundY) != areaColor; 
            
        }
    }
}
