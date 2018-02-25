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
    class MapManager
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
            Background = new Bitmap(new Bitmap("../../Images/background_track.png"), size);
        }

        /// <summary>
        /// rozhodne, jestli je pixel volný
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool PixelIsEmpty(System.Windows.Point point)
        {
            int RoundX = (int)Math.Round(point.X);
            int RoundY = (int)Math.Round(point.Y);
            return Background.GetPixel(RoundX, RoundY) != areaColor; 
        }
    }
}
