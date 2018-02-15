using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WPF_KeyReact
{
    static class ImageLoader
    {
        /// <summary>
        /// vrátí načtený obrázek
        /// </summary>
        /// <param name="size"></param>
        /// <param name="path">cesta k souboru</param>
        /// <returns></returns>
        public static Image LoadImage(Size size, string path)
        {
            path = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\Images", path); // posune do složky aktuální aplikace a o složku výše
            Image loadImage = new Bitmap(path);
            Image image = new Bitmap(loadImage, size);
            return image;
        }

        /// <summary>
        /// zpracuje obrázek na pole barev
        /// </summary>
        /// <returns></returns>
        public static Color[,] Process(Image image)
        {
            Color[,] arrayOfValues = new Color[image.Width, image.Height];

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = (image as Bitmap).GetPixel(i, j);
                    arrayOfValues[i, j] = Color.FromArgb(pixel.R,pixel.G,pixel.B);
                }
            }

            return arrayOfValues;
        }

    }
}
