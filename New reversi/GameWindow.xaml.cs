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
        List<Person> list = new List<Person>();
        GameEngine gameEngine = new GameEngine();
        DispatcherTimer tB = new DispatcherTimer();
        DispatcherTimer tW = new DispatcherTimer();
        Stopwatch sw_B = new Stopwatch();
        Stopwatch sw_W = new Stopwatch();
        String currentTime;
        bool _isFinished = false;

        string p1_firstname;
        string p1_lastname;
        string p2_firstname;
        string p2_lastname;

        public GameWindow()
        {
            InitializeComponent();
            if(!_isFinished) updateBoard();
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


        public void setPlayer1(string fn, string ln)
        {
            this.p1_firstname = fn;
            this.p1_lastname = ln;

        }
        public void setPlayer2(string fn, string ln)
        {
            this.p2_firstname = fn;
            this.p2_lastname = ln;

        }
        /// <summary>
        /// Calculates the ellapsed time for black and white player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the interaction with the matrix cells.
        /// Starts and stops the current player's stopwatch.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Updates the UI from the board.matrix, if the match has ended it adds the two player to the database.
        /// </summary>
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
                sw_B.Stop();
                sw_W.Stop();
                _isFinished = true;  
            }
            if (_isFinished)
            {
                TimeSpan ts_b = sw_B.Elapsed;
                TimeSpan ts_w = sw_W.Elapsed;

                Person player1 = new Person();
                Person player2 = new Person();
                player1.firstName = p1_firstname; player1.lastName = p1_lastname; player1.time = String.Format("{0:00}:{1:00}:{2:00}",
            ts_b.Minutes, ts_b.Seconds, ts_b.Milliseconds / 10); player1.bestScore = gameEngine.blackScore;
                player2.firstName = p2_firstname; player2.lastName = p2_lastname; player2.time = String.Format("{0:00}:{1:00}:{2:00}",
            ts_w.Minutes, ts_w.Seconds, ts_w.Milliseconds / 10); player2.bestScore = gameEngine.whiteScore;
                SqliteDataAccess.SavePerson(player1);
                SqliteDataAccess.SavePerson(player2);
                this.Close();
            }
        }

        private Color getColor(bool a)
        {
            return a ? Colors.Black : Colors.White;
        }

    }
}

