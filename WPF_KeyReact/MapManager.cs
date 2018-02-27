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
        private Color areaColor;
        private Color finishColor;
        /// <summary>
        /// cesta k souboru
        /// </summary>
        private string pathToFile;

        /// <summary>
        /// Constructor
        /// </summary>
        public MapManager(Size size)
        {
            pathToFile = "background_track.png";
            areaColor = Color.FromArgb(255, 255, 255);
            finishColor = Color.FromArgb(236, 28, 36);
            Background = ImageLoader.LoadImage(size, pathToFile);
            BackgroundPixel = ImageLoader.Process(Background);
            Image tmp = new Bitmap("../../Images/background_track.png");


            Background = new Bitmap(tmp, size);
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
            return Background.GetPixel(RoundX, RoundY) != areaColor; 
            
        }

    }
}
