using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Reversi
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        GameEngine gameEngine = new GameEngine();
        DispatcherTimer tB = new DispatcherTimer();
        DispatcherTimer tW = new DispatcherTimer();
        Stopwatch sw_B = new Stopwatch();
        Stopwatch sw_W = new Stopwatch();
        String currentTime;

        public GameWindow()
        {
            InitializeComponent();
            updateBoard();
            text_black.Text = "Black Ellapsed time";
            text_white.Text = "White Ellapsed time";
            tB.Interval = TimeSpan.FromMilliseconds(1);
            tW.Interval = TimeSpan.FromMilliseconds(1);
            tB.Tick += timer_Tick_B;
            tW.Tick += timer_Tick_W;
            tB.Start();
            tW.Start();
            sw_B.Start();
        }

        void timer_Tick_W(object sender, EventArgs e)
        {
            TimeSpan ts = sw_W.Elapsed;
            currentTime = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            timer_W.Text = currentTime;
        }
        void timer_Tick_B(object sender, EventArgs e)
        {
            TimeSpan ts = sw_B.Elapsed;
            currentTime = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            timer_B.Text = currentTime;
        }


        private void CellClicker(object sender, RoutedEventArgs e)
        {
            
            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (gameEngine.PlaceTile(row, col))
            {
                if (!gameEngine.blackTurn)
                {
                    sw_B.Stop();
                    sw_W.Start();
                }
                else
                {
                    sw_W.Stop();
                    sw_B.Start();
                }
            }
            updateBoard();            
        }

        private void updateBoard()
        {
            grid.Children.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    if (gameEngine.getBoard().matrix[i, j] is Tile)
                    {
                        Ellipse el1 = new Ellipse();
                        el1.Fill = new SolidColorBrush(getColor(gameEngine.getBoard().matrix[i, j].isBlack));
                        Grid.SetRow(el1, i);
                        Grid.SetColumn(el1, j);
                        grid.Children.Add(el1);
                    }
                    else
                    {
                        Button btn = new Button();
                        Grid.SetRow(btn, i);
                        Grid.SetColumn(btn, j);
                        btn.Opacity = 0;
                        btn.Click += CellClicker;
                        grid.Children.Add(btn);
                    }
                    if (gameEngine.blackTurn)
                    {
                        turn.Text = "Black turn";
                    }
                    else
                    {
                        turn.Text = "White turn";
                    }
                    score.Text = string.Format("Score:\nBlack: {0}\nWhite: {1}", gameEngine.blackScore, gameEngine.whiteScore);


                }
            }
            if (!gameEngine.hasPossibleMove(gameEngine.blackTurn))
            {
                if (gameEngine.blackScore > gameEngine.whiteScore)
                {
                    MessageBox.Show("Black won! Game will now reset.");
                }
                else
                {
                    MessageBox.Show("White won! Game will now reset.");
                }
                gameEngine.resetGame();
                updateBoard();
            }
        }

        private Color getColor(bool a)
        {
            return a ? Colors.Black : Colors.White;
        }

    }
}

