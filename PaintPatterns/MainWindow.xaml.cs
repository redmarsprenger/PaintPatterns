using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public Point SelectPos;
        private bool mouseButtonHeld;

        UIElement selectedElement = null;
        Shape selectedShape;

        Point firstPos;
        bool drawing = false;

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
                draw.select(selectedElement, e.GetPosition(Canvas), InitialPosition, Canvas);
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
                }
                else if(shape == "rectangle")
                {
                    draw.rectangle((int)InitialPosition.X, (int)InitialPosition.Y, 50, 30, Canvas);

                    if (drawing == false)
                    {
                        firstPos = InitialPosition;
                        drawing = true;
                    }
                    else
                    {
                        if (InitialPosition.X < firstPos.X && InitialPosition.Y > firstPos.Y)
                        {
                            draw.ellipse((int)InitialPosition.X, (int)firstPos.Y, (int)firstPos.X - (int)InitialPosition.X, (int)InitialPosition.Y - (int)firstPos.Y, Canvas);
                        }
                        else if (InitialPosition.X > firstPos.X && InitialPosition.Y < firstPos.Y)
                        {
                            draw.ellipse((int)firstPos.X, (int)InitialPosition.Y, (int)InitialPosition.X - (int)firstPos.X, (int)firstPos.Y - (int)InitialPosition.Y, Canvas);
                        }
                        else if (InitialPosition.X < firstPos.X && InitialPosition.Y < firstPos.Y)
                        {
                            draw.ellipse((int)InitialPosition.X, (int)InitialPosition.Y, (int)firstPos.X - (int)InitialPosition.X, (int)firstPos.Y - (int)InitialPosition.Y, Canvas);
                        }
                        else
                        {
                            draw.ellipse((int)firstPos.X, (int)firstPos.Y, (int)InitialPosition.X - (int)firstPos.X, (int)InitialPosition.Y - (int)firstPos.Y, Canvas);
                        }
                        firstPos = new Point();
                        drawing = false;
                    }
                }
                else if(shape == "rectangle")
                {
                    if (drawing == false)
                    {
                        firstPos = InitialPosition;
                        drawing = true;
                    }
                    else
                    {
                        if (InitialPosition.X < firstPos.X && InitialPosition.Y > firstPos.Y)
                        {
                            draw.rectangle((int)InitialPosition.X, (int)firstPos.Y, (int)firstPos.X - (int)InitialPosition.X, (int)InitialPosition.Y - (int)firstPos.Y, Canvas);
                        }
                        else if (InitialPosition.X > firstPos.X && InitialPosition.Y < firstPos.Y)
                        {
                            draw.rectangle((int)firstPos.X, (int)InitialPosition.Y, (int)InitialPosition.X - (int)firstPos.X, (int)firstPos.Y - (int)InitialPosition.Y, Canvas);
                        }
                        else if (InitialPosition.X < firstPos.X && InitialPosition.Y < firstPos.Y)
                        {
                            draw.rectangle((int)InitialPosition.X, (int)InitialPosition.Y, (int)firstPos.X - (int)InitialPosition.X, (int)firstPos.Y - (int)InitialPosition.Y, Canvas);
                        }
                        else
                        {
                            draw.rectangle((int)firstPos.X, (int)firstPos.Y, (int)InitialPosition.X - (int)firstPos.X, (int)InitialPosition.Y - (int)firstPos.Y, Canvas);
                        }
                        firstPos = new Point();
                        drawing = false;
                    }
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
        private static Point Diff;
        public static void ellipse(int x, int y, int width, int height, Canvas cv)
        {
            Ellipse ellipse = new Ellipse()
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = randColor()
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
                Fill = randColor()
            };

            cv.Children.Add(rectangle);
            rectangle.SetValue(Canvas.LeftProperty, (double)x);
            rectangle.SetValue(Canvas.TopProperty, (double)y);

            Shapes.Add(rectangle);
        }

        public static void move(UIElement selectedElement, Point getPosition)
        {
            selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X - Diff.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y - Diff.Y);
        }

        public static void select(UIElement selectedElement, Point getPosition, Point InitialPosition, Canvas canvas)
        {
            Point relativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
            Diff.X = InitialPosition.X - relativePoint.X;
            Diff.Y = InitialPosition.Y - relativePoint.Y;

            selectedElement.GetType().GetProperty("Stroke").SetValue(selectedElement, Brushes.Blue);
        }

        public static void unselect(UIElement selectedElement)
        {
            selectedElement.GetType().GetProperty("Stroke").SetValue(selectedElement, Brushes.Black);
        }

        public static Brush randColor()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

    }
}
