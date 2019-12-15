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
using Reversi.Models;
using Reversi.ViewModels;
using Console = System.Diagnostics.Debug;

namespace Reversi.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        MainWindow mw;
        GameViewModel gvm = new GameViewModel();

        public GameView(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
            render();
        }
        private void render()
        {
            renderLines();
            renderDisks();
        }

        private void renderDisks()
        {
            ;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var disk = new Ellipse();
                    Console.WriteLine(disk.Stroke);
                    switch (gvm.Board.Disks[i][j])
                    {
                        case Board.Disk.Empty:
                            disk.Fill = System.Windows.Media.Brushes.Transparent;
                            break;
                        case Board.Disk.White:
                            disk.Fill = System.Windows.Media.Brushes.White;
                            break;
                        case Board.Disk.Black:
                            disk.Fill = System.Windows.Media.Brushes.Black;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                    Canvas.SetLeft(disk, i * 50);
                    Canvas.SetTop(disk, j * 50);
                    disk.Height = 50;
                    disk.Width = 50;
                    disk.IsMouseDirectlyOverChanged += Disk_IsMouseDirectlyOverChanged;
                    disk.MouseLeftButtonDown += Disk_MouseLeftButtonDown;

                    canvas.Children.Add(disk);
                }
            }
        }

        private void Disk_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Disk_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Ellipse sent = (Ellipse)sender;
            //var isItHovered = Boolean.parseBoolean();
            if (e.NewValue.Equals(true))
            {
                sent.Stroke = System.Windows.Media.Brushes.DarkSlateGray;
            }
            else
            {
                sent.Stroke = System.Windows.Media.Brushes.Transparent;
            }
        }

        private void renderLines()
        {
            for (int i = 0; i <= 400; i += 50)
            {
                var line = new Line();
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.X1 = i;
                line.Y1 = 0;
                line.X2 = i;
                line.Y2 = 400;
                canvas.Children.Add(line);
            }
            for (int i = 0; i <= 400; i += 50)
            {
                var line = new Line();
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.X1 = 0;
                line.Y1 = i;
                line.X2 = 400;
                line.Y2 = i;
                canvas.Children.Add(line);
            }
        }
    }
}
