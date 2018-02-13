using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_KeyReact
{
    class Car
    {
        /// <summary>
        /// úhel, o který se otáčí auto
        /// </summary>
        public static readonly double rotationAngle = 30;

        /// <summary>
        /// počet pixelů o které se pohne při pohybu
        /// </summary>
        public static readonly double pixelsPerMove = 20;

        /// <summary>
        /// aktuální úhel
        /// </summary>
        private double angle;
        public double Angle
        {
            get => angle;
            set
            {
                double newAngle = value;
                double myAngle = newAngle;
                myAngle = angle < myAngle ? rotationAngle : -rotationAngle;

                newAngle = newAngle % 360;
                angle = newAngle < 0 ? newAngle + 360 : newAngle;

                myAngle *= (Math.PI / 180);
                LeftFrontCorner = CountCoordinatesAfterRotation(myAngle, LeftFrontCorner);
                RightFrontCorner = CountCoordinatesAfterRotation(myAngle, RightFrontCorner);
            }
        }

        /// <summary>
        /// souřadnice levého, pravého horního rohu a středu auta
        /// </summary>
        public Point RightFrontCorner { get; private set; }
        public Point LeftFrontCorner { get; private set; }
        public Point Center { get; private set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        public Car(Point leftUpperCorner, double height, double width)
        {
            angle = 0;
            RightFrontCorner = leftUpperCorner;
            LeftFrontCorner = new Point(leftUpperCorner.X, leftUpperCorner.Y + height);
            Center = new Point(leftUpperCorner.X + width / 2, leftUpperCorner.Y + height / 2);

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

            RightFrontCorner = new Point(RightFrontCorner.X + leftMargin / 2, RightFrontCorner.Y + topMargin / 2);
            LeftFrontCorner = new Point(LeftFrontCorner.X + leftMargin / 2, LeftFrontCorner.Y + topMargin / 2);
            Center = new Point(Center.X + leftMargin / 2, Center.Y + topMargin / 2);

            return new Tuple<double, double>(topMargin, leftMargin);
        }

        /// <summary>
        /// otočí bod kolem Center
        /// </summary>
        /// <param name="angle">úhel</param>
        /// <param name="point">bod</param>
        /// <returns>souřadnice otočeného bodu</returns>
        private Point CountCoordinatesAfterRotation(double angle, Point point)
        {
            point = new Point(point.X - Center.X, point.Y - Center.Y);

            double x = point.X * Math.Cos(angle) - point.Y * Math.Sin(angle);
            double y = point.X * Math.Sin(angle) + point.Y * Math.Cos(angle);

            return new Point(x + Center.X, y + Center.Y);
        }
    }
}
