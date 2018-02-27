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
    public class Car
    {
        #region Variable Declaration

        /// <summary>
        /// proměnné
        /// </summary>
        private FrameworkElement image;
        private double height, width;
        private GamePage gamePage;
        public KeyStuff Up, Down, Right, Left;
        public string Name { get; private set; }
        public int Points { get; private set; } = 0;
        private bool inFinish = false;

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

                image.RenderTransform = new RotateTransform(Angle);
            }
        }

        private double speed = 0;      //pixels per move
        public double Speed
        {
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

        /// <summary>
        /// je v cíli?
        /// </summary>
        public bool InFinish { get; set; } = false;

        #endregion

        /// <summary>
        /// konstruktor
        /// </summary>
        public Car(GamePage wnd, FrameworkElement car, UIElement ancestor, Key left, Key right, Key up, Key down, String name)
        {
            Name = name;
            this.image = car;
            this.gamePage = wnd;
            wnd.timer.Tick += Timer_Tick;
            wnd.timer.Start();

            height = car.ActualHeight;
            width = car.ActualWidth;
            car.RenderTransformOrigin = new Point(0.5, 0.5);

            Point relativePoint = car.TransformToAncestor(ancestor).Transform(new Point(0.5, 0.5));
            Center = new Point(relativePoint.X + car.ActualWidth / 2, relativePoint.Y + car.ActualHeight / 2);

            RightFrontCorner = new Point(Center.X - width / 2, Center.Y - height / 2);
            LeftFrontCorner = new Point(Center.X - width / 2, Center.Y + height / 2);

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
            double piAngle = angle / 180 * Math.PI;
            double topMargin = (-1) * Math.Sin(piAngle) * speed;
            double leftMargin = (-1) * Math.Cos(piAngle) * speed;

            Point newCenter = new Point(Center.X + leftMargin / 2, Center.Y + topMargin / 2);
            Point newRightFrontCorner = new Point(RightFrontCorner.X + leftMargin / 2, RightFrontCorner.Y + topMargin / 2);
            Point newLeftFrontCorner = new Point(LeftFrontCorner.X + leftMargin / 2, LeftFrontCorner.Y + topMargin / 2);

            List<Point> Corners = new List<Point>()
            {
                    newCenter, newLeftFrontCorner,newRightFrontCorner
            };

            if (Corners.TrueForAll(corner => gamePage.mapManager.PixelIsEmptyOrFinish(corner)))  // checks if corners collide or if center collides - decides base on cornerCollision boolean
            {
                Center = newCenter;
                LeftFrontCorner = newLeftFrontCorner;
                RightFrontCorner = newRightFrontCorner;

                image.Margin = new Thickness
                (
                    image.Margin.Left + leftMargin / 2,
                    image.Margin.Top + topMargin / 2,
                    image.Margin.Right - leftMargin / 2,
                    image.Margin.Bottom - topMargin / 2
                );
            }
            else
            {
                Speed = 0;      //stops because of collision
            }
            CheckIfFinish();
        }

        /// <summary>
        /// zkontroluje, jestli není v cíli (nepřičte body při zpětném průchodu)
        /// </summary>
        private void CheckIfFinish()
        {
            if (gamePage.mapManager.PixelIsEmptyOrFinish(Center, true) && LeftFrontCorner.X < Center.X)
            {
                if (!InFinish)
                {
                    Points++;
                    gamePage.Player1PointsTextBlock.Text = Points.ToString();

                    if (Points >= 5)
                        gamePage.ShowWinner(this);
                }
                InFinish = true;
            }
            else
                InFinish = false;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
