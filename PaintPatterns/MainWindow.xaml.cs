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

namespace PaintPatterns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Ellipse ellipse = new Ellipse();

            Line line = new Line();
            line.Stroke = System.Windows.Media.Brushes.Blue;
            line.X1 = 25;
            line.X2 = 100;
            line.Y1 = 30;
            line.Y2 = 111;
            line.StrokeThickness = 2;

            //Canvas.Children.Add(line);

            ellipse.Width = 50;
            ellipse.Height = 50;
            ellipse.Margin = new Thickness(25,30,0,0);
            ellipse.Width = line.X2 - line.X1;
            ellipse.Height = line.Y2 - line.Y1;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = System.Windows.Media.Brushes.Black;

            Canvas.Children.Add(ellipse);
            Canvas.Children.Add(line);
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TriangleBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CircleBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
