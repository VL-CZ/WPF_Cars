using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_KeyReact
{
    class Car
    {
        FrameworkElement image;
        double height, width;
        MainWindow wnd;
        public KeyStuff Up, Down, Right, Left;
        List<Point> Corners;
        bool cornerCollision = false;
        /// <summary>
        /// úhel, o který se otáčí auto
        /// </summary>
        public static readonly double rotationAngle = 3;
        public static readonly double accelerationRate = 0.05;
        

        /// <summary>
        /// aktuální úhel
        /// </summary>
        private double angle = 0;
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
        private double speed = 0;      //pixels per move
        public double Speed {
            get => speed;
            set
            {
                if (value >= -3 && value <= 6)
                    speed = value;
            }
        }
        /// <summary>
        /// souřadnice levého, pravého horního rohu a středu auta
        /// </summary>
        public Point RightFrontCorner { get; private set; }
        public Point LeftFrontCorner { get; private set; }
        public Point Center { get; private set; }

        public bool InFinish { get; set; } = false;

        /// <summary>
        /// souřadnice v předchozím tahu (hodí se při nemožnosti udělat tah)
        /// </summary>
        public Point PreviousRightFrontCorner { get; private set; }
        public Point PreviousLeftFrontCorner { get; private set; }
        public Point PreviousCenter { get; private set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        public Car(MainWindow wnd, FrameworkElement car, UIElement ancestor, Key left, Key right, Key up, Key down)
        {   this.image = car;
            this.wnd = wnd;
            wnd.timer.Tick += Timer_Tick;

            RightFrontCorner = leftUpperCorner;
            LeftFrontCorner = new Point(leftUpperCorner.X, leftUpperCorner.Y + height);
            Center = new Point(leftUpperCorner.X + width / 2, leftUpperCorner.Y + height / 2);

            height = car.ActualHeight;
            width = car.ActualWidth;
            car.RenderTransformOrigin = new Point(0, 0);

            Point relativePoint = car.TransformToAncestor(ancestor).Transform(new Point(0.5, 0.5));
            Center = new Point(relativePoint.X + car.ActualWidth / 2, relativePoint.Y + car.ActualHeight / 2);

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
        /// otočí bod kolem Center
        /// </summary>
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
        public void Timer_Tick(object sender, EventArgs e)
        {
            if (Up.isDown)
                Speed += accelerationRate;
            else if (Down.isDown)
            {
                if (Speed > 0)
                {
                    Speed -= 2 * accelerationRate;                     //brakes
                }
                else if (Speed < 0)
                {
                    Speed -= accelerationRate;
                }
            }
            else
            {
                if (Speed > 0)
                {
                    Speed -= accelerationRate;
                }
                else if (Speed < 0)
                {
                    Speed += accelerationRate;
                }
            }
            if (Left.isDown)
            {
                Angle -= Car.rotationAngle;
            }
            if (Right.isDown)
            {
                Angle += Car.rotationAngle;
            }


            Move();
        }
        /// <summary>
        /// pohne autem
        /// </summary>
        public void Move()                                       
        {
            double piAngle = angle / 180 * Math.PI ;
            double topMargin = (-1) * Math.Sin(piAngle) * speed;
            double leftMargin = (-1) * Math.Cos(piAngle) * speed;

           
            Point NewCenter = new Point(Center.X + leftMargin / 2, Center.Y + topMargin / 2);
            
            if (cornerCollision)
            {
                Corners = new List<Point>
                {
                    new Point(NewCenter.X + width / 2, NewCenter.Y + height / 2),
                    new Point(NewCenter.X - width / 2, NewCenter.Y + height / 2),
                    new Point(NewCenter.X + width / 2, NewCenter.Y - height / 2),
                    new Point(NewCenter.X - width / 2, NewCenter.Y - height / 2),
                };
            }
            

            if ( cornerCollision? Corners.TrueForAll(corner => wnd.mapManager.PixelIsEmpty(corner)) : wnd.mapManager.PixelIsEmpty(NewCenter))  // checks if corners collide or if center collides - decides base on cornerCollision boolean
            {
                Center = NewCenter;
                image.Margin = new Thickness
                (
                    image.Margin.Left + leftMargin / 2,
                    image.Margin.Top + topMargin / 2,
                    0,
                    0
                );
            }
            else
            {
                Speed = 0;      //stops because of collision
            }
        }
    }
}
