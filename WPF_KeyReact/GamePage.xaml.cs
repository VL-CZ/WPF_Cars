﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_KeyReact
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private Car player1, player2;
        public MapManager mapManager;
        internal ConcurrentBag<KeyStuff> Controls = new ConcurrentBag<KeyStuff>();          //concurrent bag je neco jako list, ale je multi thread safe = da se s nim pracovat z vice vlaken aniz by to hazelo errory. Pouzivam ho zde kvuli asynchroni operaci v methode ModifyKeyState

        public DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(15) };

        /// <summary>
        /// reference na UserControl elementy
        /// </summary>
        public TextBlock Player1PointsTextBlock { get; set; }
        public TextBlock Player2PointsTextBlock { get; set; }
        public TextBlock TimeTextBlock { get; set; }

        /// <summary>
        /// kostruktor okna
        /// </summary>
        public GamePage()
        {
            InitializeComponent();

        }
        /// <summary>
        /// find key in controls and changes its state
        /// </summary>
        private void ModifyKeyState(Key key, bool b)
        {
            Task.Run(() =>
            {
                foreach (var control in Controls)
                {
                    if (control.key == key)
                        control.isDown = b;
                }
            });

        }

        /// <summary>
        /// načte objekty
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            player1 = new Car(this, ButtonCar, GridMain, Key.Left, Key.Right, Key.Up, Key.Down, "Player 1");
            player2 = new Car(this, ButtonCar2, GridMain, Key.A, Key.D, Key.W, Key.S, "Player 2");
            mapManager = new MapManager(new System.Drawing.Size((int)(GridMain.ActualWidth), (int)(GridMain.ActualHeight)));

            Player1PointsTextBlock = HelpClass.Player1Points;
            Player2PointsTextBlock = HelpClass.Player2Points;
            TimeTextBlock = HelpClass.Time;

            Window window = Window.GetWindow(this);
            window.KeyDown += (obj, args) => ModifyKeyState(args.Key, true);
            window.KeyUp += (obj, args) => ModifyKeyState(args.Key, false);
        }

        /// <summary>
        /// přepne na StatisticsPage 
        /// </summary>
        /// <param name="winner"></param>
        public void ShowWinner(Car winner)
        {
            StatisticsPage statisticsPage = new StatisticsPage(int.Parse(TimeTextBlock.Text), winner, player1, player2); // místo null přijde 2. auto

            timer.Tick -= player1.Timer_Tick; // je potřeba, jinak při nové hře někdy padá
            timer.Tick -= player2.Timer_Tick;

            this.NavigationService.Navigate(statisticsPage);
        }

    }
}
