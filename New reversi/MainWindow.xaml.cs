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

        public MainWindow()
        {
            InitializeComponent();

        }
        
        private void Vs_cmp_clicked(object sender, RoutedEventArgs e)
        {
            GameWindow gv = new GameWindow();
            gv.Show();
            this.Close();

        }

        private void Exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       private void Vs_hm_clicked(object sender, RoutedEventArgs e)
        {
            GameWindow gv = new GameWindow();
            gv.Show();
            this.Close();
        }
    } 
}
