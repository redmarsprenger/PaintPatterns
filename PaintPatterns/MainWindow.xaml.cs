using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
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
        private string shape;
        public Point InitialPosition;
        private bool mouseButtonHeld;

        UIElement selectedElement = null;
        Shape selectedShape;

        bool circle;

        public MainWindow()
        {
            InitializeComponent();
            NoneBtn.IsEnabled = false;
        }

        private void NoneBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "none";
            NoneBtn.IsEnabled = false;
            RectangleBtn.IsEnabled = true;
            EllipseBtn.IsEnabled = true;
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "rectangle";
            NoneBtn.IsEnabled = true;
            RectangleBtn.IsEnabled = false;
            EllipseBtn.IsEnabled = true;
        }

        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "ellipse";
            NoneBtn.IsEnabled = true;
            RectangleBtn.IsEnabled = true;
            EllipseBtn.IsEnabled = false;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = true;
            InitialPosition = e.GetPosition(Canvas);
            if (e.Source != Canvas)
            {
                if (selectedElement != null)
                    draw.unselect(selectedElement);
                selectedShape = e.Source as Shape;
                selectedElement = e.Source as UIElement;
                draw.select(selectedElement);
            }
            else
            {
                if(selectedElement != null)
                    draw.unselect(selectedElement);
                
                selectedShape = null;
                selectedElement = null;

                if (shape == "ellipse")
                {
                    draw.ellipse((int)InitialPosition.X, (int)InitialPosition.Y, 50, 30, Canvas);
                    circle = true;
                }
                else if(shape == "rectangle")
                {
                    draw.rectangle((int)InitialPosition.X, (int)InitialPosition.Y, 50, 30, Canvas);
                    circle = false;
                }
            }

        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseButtonHeld && selectedElement != null)
            {
                Point position = Mouse.GetPosition(Canvas);
                draw.move(selectedElement, e.GetPosition(Canvas));
            }
        }
    }


    class draw
    {
        private static List<Shape> Shapes = new List<Shape>();
        public static void ellipse(int x, int y, int width, int height, Canvas cv)
        {
            Ellipse ellipse = new Ellipse()
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.Red
            };

            cv.Children.Add(ellipse);

            ellipse.SetValue(Canvas.LeftProperty, (double)x);
            ellipse.SetValue(Canvas.TopProperty, (double)y);

            Shapes.Add(ellipse);
        }
        public static void rectangle(int x, int y, int width, int height, Canvas cv)
        {
            Rectangle rectangle = new Rectangle()
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.Red
            };

            cv.Children.Add(rectangle);
            rectangle.SetValue(Canvas.LeftProperty, (double)x);
            rectangle.SetValue(Canvas.TopProperty, (double)y);

            Shapes.Add(rectangle);
        }

        public static void move(UIElement selectedElement, Point getPosition)
        {
            selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
        }

        public static void select(UIElement selectedElement)
        {
            selectedElement.GetType().GetProperty("Stroke").SetValue(selectedElement, Brushes.Blue);
        }
        public static void unselect(UIElement selectedElement)
        {
            selectedElement.GetType().GetProperty("Stroke").SetValue(selectedElement, Brushes.Black);
        }
    }
}
