using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_KeyReact
{
    class Car
    {
       FrameworkElement image;
       double height, width;
        MainWindow wnd;
       public KeyStuff Up, Down, Right, Left;

        /// <summary>
        /// úhel, o který se otáčí auto
        /// </summary>
        public static readonly double rotationAngle = 3;

        

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
        private int speed = 0;      //pixels per move
        public int Speed {
            get => speed;
            set
            {
                if (value >= 0 && value <= 5)
                    speed = value;
            }
        }
        /// <summary>
        /// souřadnice levého, pravého horního rohu a středu auta
        /// </summary>
        public Point Center { get; private set; }
    
        /// <summary>
        /// souřadnice v předchozím tahu (hodí se při nemožnosti udělat tah)
        /// </summary>
        public Point PreviousCenter { get; private set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        public Car(MainWindow wnd, Image car, UIElement ancestor, Key left, Key right, Key up, Key down)
        {
            angle = 0;
            height = car.ActualHeight;
            width = car.ActualWidth;
            car.RenderTransformOrigin = new Point(0.5, 0.5);
            Center = car.TransformToAncestor(ancestor).Transform(new Point(0.5, 0.5));
            this.image = car;

            //setting up controls
            Left = new KeyStuff(left);
            Right = new KeyStuff(right);
            Up = new KeyStuff(up);
            Down = new KeyStuff(down);
            wnd.Controls.Add(Left);
            wnd.Controls.Add(Right);
            wnd.Controls.Add(Up);
            wnd.Controls.Add(Down);
        }
        
        /// <summary>
        /// spočítá margin
        /// </summary>
        public Tuple<double, double> CountMargin()
        {
            double myAngle = angle * (Math.PI / 180);
            double topMargin = (-1) * Math.Sin(myAngle) * speed;
            double leftMargin = (-1) * Math.Cos(myAngle) * speed;
            
            
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
        /// <summary>
        /// reaguje na stisk klávesy, pohne autem
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (upDown)
                car.Speed++;
            if (downDown)
                car.Speed--;
            if (leftDown)
            {
                car.Angle -= Car.rotationAngle;
                ButtonCar.RenderTransform = new RotateTransform(car.Angle);
            }

            if (rightDown)
            {
                car.Angle += Car.rotationAngle;
                ButtonCar.RenderTransform = new RotateTransform(car.Angle);
            }
            Move();
        }
        public void Move()
        {
            Thickness margin = ButtonCar.Margin;

            Tuple<double, double> margins = car.CountMargin();

            if (mapManager.PixelIsEmpty(car.LeftFrontCorner) && mapManager.PixelIsEmpty(car.RightFrontCorner))
            {
                margin.Top += margins.Item1 / 2;
                margin.Bottom -= margins.Item1 / 2;
                margin.Left += margins.Item2 / 2;
                margin.Right -= margins.Item2 / 2;
            }
            else
                car.RestorePrevious();
            ButtonCar.Margin = margin;
        }
    }
}
