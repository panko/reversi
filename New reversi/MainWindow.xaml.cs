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

namespace Reversi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        bool isAgainstAI = false;
        String p1 = "";
        String p2 = "";

        public MainWindow()
        {
            InitializeComponent();
      
        }
        
        private void Vs_cmp_clicked(object sender, RoutedEventArgs e)
        {
            isAgainstAI = true;
            playBTN.Visibility = Visibility.Visible;
        }

        private void Exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }     


        private void Vs_hm_clicked(object sender, RoutedEventArgs e)
        {
            isAgainstAI = false;           
            player1.Visibility = Visibility.Visible;
            player1_box.Visibility = Visibility.Visible;
            player2_box.Visibility = Visibility.Visible;
            player2.Visibility = Visibility.Visible;
            playBTN.Visibility = Visibility.Visible;
        }

        private void playBTN_Click(object sender, RoutedEventArgs e)
        {            
            p1 = player1_box.Text;
            p2 = player2_box.Text;

            if (isAgainstAI)
            {
                GameWindow_AI gv = new GameWindow_AI();
                gv.Show();

            }
            else if ((p1 != "") &&( p2 != ""))
            {
                GameWindow gv = new GameWindow();
                if ((p1.Split(' ').Length > 1))
                {
                    gv.setPlayer1(p1.Split(' ')[0], p1.Split(' ')[1]);
                    //Person person1 = new Person();
                    //person1.ID = SqliteDataAccess.LoadPeople().Count + 1;
                    //person1.firstName = p1.Split(' ')[0];
                    //person1.lastName = p1.Split(' ')[1];
                    //SqliteDataAccess.SavePerson(person1);
                }
                else
                {
                    gv.setPlayer1(p1.Split(' ')[0]," ");
                    //Person person1 = new Person();
                    //person1.ID = SqliteDataAccess.LoadPeople().Count + 1;
                    //person1.firstName = p1.Split(' ')[0];
                    //person1.lastName = " ";
                    //SqliteDataAccess.SavePerson(person1);

                }
                if (p2.Split(' ').Length > 1)
                {
                    gv.setPlayer2(p2.Split(' ')[0], p2.Split(' ')[1]);
                    //Person person2 = new Person();
                    //person2.ID = SqliteDataAccess.LoadPeople().Count + 1;
                    //person2.firstName = p2.Split(' ')[0];
                    //person2.lastName = p2.Split(' ')[1];
                    //SqliteDataAccess.SavePerson(person2);
                }
                else
                {
                    gv.setPlayer2(p2.Split(' ')[0], " ");
                    //Person person2 = new Person();
                    //person2.ID = SqliteDataAccess.LoadPeople().Count + 1;
                    //person2.firstName = p2.Split(' ')[0];
                    //person2.lastName = " ";
                    //SqliteDataAccess.SavePerson(person2);
                }
                gv.Show();  
            }

        }
        private void LeaderB(object sender, RoutedEventArgs e)
        {
            LeaderBoard LB = new LeaderBoard();
            LB.Show();
        }
    } 
}
