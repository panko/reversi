using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        MainWindow mw;
        public MenuView(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void vspc_Checked(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tb1.Text))
            {
                play.IsEnabled = true;
            }
            sp1.Visibility = Visibility.Visible;

        }

        private void vshuman_Checked(object sender, RoutedEventArgs e)
        {
            sp1.Visibility = Visibility.Visible;
            sp2.Visibility = Visibility.Visible;
            play.IsEnabled = false;
        }

        private void vshuman_Unchecked(object sender, RoutedEventArgs e)
        {
            sp2.Visibility = Visibility.Hidden;
            tb2.Clear();
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vshuman.IsChecked.Value)
            {
                if (String.IsNullOrWhiteSpace(tb1.Text) || String.IsNullOrWhiteSpace(tb2.Text))
                {
                    play.IsEnabled = false;
                }
                else
                {
                    play.IsEnabled = true;
                }
            } else {
                if (String.IsNullOrWhiteSpace(tb1.Text))
                {
                    play.IsEnabled = false;
                }
                else
                {
                    play.IsEnabled = true;
                }
            }

        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (vspc.IsChecked.Value)
            {
                mw.DataContext = new GameView(mw);
            }
        }

        private void leaderboard_Click(object sender, RoutedEventArgs e)
        {
            mw.DataContext = new LeaderboardView(mw);

        }
    }
}
