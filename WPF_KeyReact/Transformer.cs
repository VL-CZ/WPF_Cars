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
        public static readonly int rotationAngle = 30;

        /// <summary>
        /// úhel
        /// </summary>
        private int angle;
        public int Angle
        {
            get => angle;
            set
            {
                int newAngle = value % 360;
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
            double topMargin = (-1) * Math.Sin(myAngle);
            double leftMargin = (-1) * Math.Cos(myAngle);

            return new Tuple<double, double>(topMargin, leftMargin);
        }
    }
}
