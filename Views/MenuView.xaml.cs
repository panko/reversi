using System;
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

namespace Reversi.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MenuView : UserControl
    {
        MainWindow mw;
        bool isAgainstAI = false;
        string p1 = "";
        string p2 = "";

        public MenuView(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();      
        }

        /// <summary>
        /// Event handler for the button : "Against Computer".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vs_cmp_clicked(object sender, RoutedEventArgs e)
        {
            isAgainstAI = true;
            HideInputPlayer();
            playBTN.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Event handler for the button : "Exit".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }     
        

        /// <summary>
        /// Event handler for the button : "Player vs Player".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vs_hm_clicked(object sender, RoutedEventArgs e)
        {
            isAgainstAI = false;           
            player1.Visibility = Visibility.Visible;
            player1_box.Visibility = Visibility.Visible;
            player2_box.Visibility = Visibility.Visible;
            player2.Visibility = Visibility.Visible;
            playBTN.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Event handler for the button : "Play".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playBTN_Click(object sender, RoutedEventArgs e)
        {            
            p1 = player1_box.Text;
            p2 = player2_box.Text;

            if (isAgainstAI)
            {
                mw.DataContext = new GameView(mw, true, p1, p2);

            }
            else {
                if ((p1 != "") && (p2 != ""))
                {
                    HideInputPlayer();
                    GameView gv = new GameView(mw, false, p1, p2);
                    if ((p1.Split(' ').Length > 1))
                    {
                        gv.p1_firstname = p1.Split(' ')[0];
                        gv.p1_lastname = p1.Split(' ')[1];
                    }
                    else
                    {
                        gv.p1_firstname = p1;
                        gv.p1_lastname = " ";
                    }
                    if (p2.Split(' ').Length > 1)
                    {
                        gv.p2_firstname = p2.Split(' ')[0];
                        gv.p2_lastname = p2.Split(' ')[1];
                    }
                    else
                    {
                        gv.p2_firstname = p2;
                        gv.p2_lastname = " ";
                    }
                    mw.DataContext = gv;
                }
                else MessageBox.Show("You must type a name!");
            }
        }


        /// <summary>
        /// Hides the textboxes when user clicked elsewhere from tab "Human vs human".
        /// </summary>
        private void HideInputPlayer()
        {
            player1.Visibility = Visibility.Hidden;
            player1_box.Visibility = Visibility.Hidden;
            player2_box.Visibility = Visibility.Hidden;
            player2.Visibility = Visibility.Hidden;
            playBTN.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Event handler for the button : "Leaderboard".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeaderB(object sender, RoutedEventArgs e)
        {
            mw.DataContext = new LeaderboardView(mw);
        }
    } 
}
