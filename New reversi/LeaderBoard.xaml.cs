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
using System.Windows.Shapes;

namespace Reversi
{
    /// <summary>
    /// Interaction logic for LeaderBoard.xaml
    /// </summary>
    public partial class LeaderBoard : Window
    {

        List<Person> list = new List<Person>();
        public LeaderBoard()
        {
            InitializeComponent();
            LoadPeopleList();
            listPoeple();
        }

        /// <summary>
        /// Load the people from the database to a list.
        /// </summary>
        private void LoadPeopleList()
        {
            list = SqliteDataAccess.LoadPeople();
            list = list.OrderByDescending(Person => Person.bestScore).ToList();
            
        }

        /// <summary>
        /// Fill up the leaderboard with the people in the database.
        /// </summary>
        private void listPoeple()
        {
            int k = 0;
            int i = 1;
            TextBlock[,] blocklist = new TextBlock[8, 3];
            
            foreach (Person item in list)
            {
                blocklist[i, k] = new TextBlock();
                blocklist[i, k].Text = item.FullName;
                Grid.SetRow(blocklist[i, k], i);
                Grid.SetColumn(blocklist[i, k], k);
                blocklist[i, k].FontSize = 28;
                grid.Children.Add(blocklist[i, k]);
                k++;
                blocklist[i, k] = new TextBlock();
                blocklist[i, k].Text = item.time;
                Grid.SetRow(blocklist[i, k], i);
                Grid.SetColumn(blocklist[i, k], k);
                blocklist[i, k].FontSize = 28;
                grid.Children.Add(blocklist[i, k]);
                k++;
                blocklist[i, k] = new TextBlock();
                blocklist[i, k].Text = item.bestScore.ToString();
                Grid.SetRow(blocklist[i, k], i);
                Grid.SetColumn(blocklist[i, k], k);
                blocklist[i, k].FontSize = 28;
                grid.Children.Add(blocklist[i, k]);
                k = 0;
                i++;
                if (i > 7) break;
            }

        }
    }
}
