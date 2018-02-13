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
        /// <summary>
        /// obrázek a obrázek na pixely
        /// </summary>
        public Image Background { get; private set; }
        public RGBColor[,] BackgroundPixel { get; private set; }
        /// <summary>
        /// barva dráhy
        /// </summary>
        public RGBColor trackColor;
        /// <summary>
        /// cesta k souboru
        /// </summary>
        private string pathToFile;

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="size"></param>
        public MapManager(Size size)
        {
            pathToFile = "background.png";
            trackColor = new RGBColor(0, 0, 0);
            Background = ImageLoader.LoadImage(size, pathToFile);
            BackgroundPixel = ImageLoader.Process(Background);
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
            return BackgroundPixel[RoundX, RoundY] == trackColor;
        }
    }
}
