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
        public Color[,] BackgroundPixel { get; private set; }
        /// <summary>
        /// barva okolí dráhy
        /// </summary>
        private Color areaColor;
        private Color finishColor;
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
            pathToFile = "background_track.png";
            areaColor = Color.FromArgb(255, 255, 255);
            finishColor = Color.FromArgb(236, 28, 36);
            Background = ImageLoader.LoadImage(size, pathToFile);
            BackgroundPixel = ImageLoader.Process(Background);
        }

        /// <summary>
        /// rozhodne, jestli je pixel volný / jestli je to cíl
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool PixelIsEmptyOrFinish(System.Windows.Point point, bool finish = false) // pokud není zadán druhý paramter, tak je automaticky false
        {
            int RoundX = (int)Math.Round(point.X);
            int RoundY = (int)Math.Round(point.Y);
            if (finish)
                return BackgroundPixel[RoundX, RoundY] == finishColor;
            else
                return BackgroundPixel[RoundX, RoundY] != areaColor;
        }

    }
}
