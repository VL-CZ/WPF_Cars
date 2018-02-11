using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_KeyReact
{
    class Transformer
    {
        /// <summary>
        /// úhel, o který se otáčí tlačítko
        /// </summary>
        public static readonly double rotationAngle = 30;

        /// <summary>
        /// počet pixelů o které se pohne při pohybu
        /// </summary>
        public static readonly double pixelsPerMove = 20;

        /// <summary>
        /// úhel
        /// </summary>
        private double angle;
        public double Angle
        {
            get => angle;
            set
            {
                double newAngle = value % 360;
                angle = newAngle < 0 ? newAngle + 360 : newAngle;
            }
        }

        /// <summary>
        /// konstruktor
        /// </summary>
        public Transformer()
        {
            angle = 0;
        }

        /// <summary>
        /// spočítá margin
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public Tuple<double, double> CountMargin()
        {
            double myAngle = angle * (Math.PI / 180);
            double topMargin = (-1) * Math.Sin(myAngle) * pixelsPerMove;
            double leftMargin = (-1) * Math.Cos(myAngle) * pixelsPerMove;

            return new Tuple<double, double>(topMargin, leftMargin);
        }
    }
}
