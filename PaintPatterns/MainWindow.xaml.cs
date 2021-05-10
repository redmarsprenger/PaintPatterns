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
using PaintPatterns.Command;

namespace PaintPatterns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CommandInvoker invoker;
        private string shape = "none";
        public Point InitialPosition;
        public Point SelectPos;
        private bool mouseButtonHeld;

        UIElement selectedElement = null;
        Shape selectedShape;

        Point firstPos;
        bool drawing = false;
        Shape shapeDrawing;

        List<String> UndoList;
        List<String> RedoList;
        
        private static List<Shape> Shapes = new List<Shape>();
        private static Point Diff;
        private static Point RelativePoint;
        
        public MainWindow()
        {
            InitializeComponent();
            NoneBtn.IsEnabled = false;
            invoker = new CommandInvoker {MainWindow = this};
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
            if (shape == "none")
            {
                selectedElement?.GetType().GetProperty("Stroke")?.SetValue(selectedElement, Brushes.Black);
                if (e.Source != Canvas)
                {
                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                        drawing = false;
                    if (Mouse.RightButton == MouseButtonState.Pressed)
                        drawing = true;
                    selectedShape = e.Source as Shape;
                    selectedElement = e.Source as UIElement;

                    Point relativePoint = selectedElement.TransformToAncestor(Canvas).Transform(new Point(0, 0));
                    Diff.X = InitialPosition.X - relativePoint.X;
                    Diff.Y = InitialPosition.Y - relativePoint.Y;

                    selectedElement.GetType().GetProperty("Stroke")?.SetValue(selectedElement, Brushes.Blue);
                    RelativePoint = selectedElement.TransformToAncestor(Canvas).Transform(new Point(0, 0));
                }
            }
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (shape != "none")
                {
                    selectedElement?.GetType().GetProperty("Stroke")?.SetValue(selectedElement, Brushes.Black);

                    selectedShape = null;
                    selectedElement = null;

                    if (drawing == false)
                    {
                        drawing = true;
                        if (shape == "ellipse")
                        {
                            Ellipse ellipse = new Ellipse()
                            {
                                Width = 0,
                                Height = 0,
                                Stroke = Brushes.Black,
                                StrokeThickness = 2,
                                Fill = (Brush)typeof(Brushes).GetProperties()[new Random().Next(typeof(Brushes).GetProperties().Length)].GetValue(null, null)
                            };

                            Canvas.Children.Add(ellipse);
                            ellipse.SetValue(Canvas.LeftProperty, InitialPosition.X);
                            ellipse.SetValue(Canvas.TopProperty, InitialPosition.Y);

                            shapeDrawing = ellipse;
                        }
                        else if (shape == "rectangle")
                        {
                            Rectangle rectangle = new Rectangle()
                            {
                                Width = 0,
                                Height = 0,
                                Stroke = Brushes.Black,
                                StrokeThickness = 2,
                                Fill = (Brush)typeof(Brushes).GetProperties()[new Random().Next(typeof(Brushes).GetProperties().Length)].GetValue(null, null)
                            };

                            Canvas.Children.Add(rectangle);
                            rectangle.SetValue(Canvas.LeftProperty, InitialPosition.X);
                            rectangle.SetValue(Canvas.TopProperty, InitialPosition.Y);

                            shapeDrawing = rectangle;
                        }
                    }
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (drawing)
                {
                    invoker.Draw(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Canvas, shapeDrawing);
                }
                else if (mouseButtonHeld && selectedElement != null)
                {
                    if (e.Source != Canvas)
                    {
                        invoker.Move(selectedElement, e.GetPosition(Canvas), Diff, InitialPosition, Canvas, shapeDrawing);
                    }
                }
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed && drawing)
            {
                if (shape == "none")
                {
                    invoker.Resize(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Canvas, shapeDrawing);
                }
            }
        }

        private void ChangeColor_Click(object sender, RoutedEventArgs e)
        {
//            if (shape == "none" && selectedShape != null)
//            {
//                selectedShape = new RedDecorator(selectedShape).ReturnShape();
//            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = false;
            firstPos = new Point();
            drawing = false;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift && e.Key == Key.Z)
                {
                    MessageBox.Show("Ctrl-Shift-Z Pressed");
                }
                else if (e.Key == Key.Z)
                {
                    MessageBox.Show("Ctrl-Z Pressed");
                }
            }
        }

//        public Ellipse ellipse(int x, int y, int width, int height, Canvas cv)
//        {
//            Ellipse ellipse = new Ellipse()
//            {
//                Width = width,
//                Height = height,
//                Stroke = Brushes.Black,
//                StrokeThickness = 2,
//                Fill = randColor()
//            };            
//
//            cv.Children.Add(ellipse);
//            ellipse.SetValue(Canvas.LeftProperty, (double)x);
//            ellipse.SetValue(Canvas.TopProperty, (double)y);
//
//            Shapes.Add(ellipse);
//            return ellipse;
//        }
//
//        public Rectangle rectangle(int x, int y, int width, int height, Canvas cv)
//        {
//            Rectangle rectangle = new Rectangle()
//            {
//                Width = width,
//                Height = height,
//                Stroke = Brushes.Black,
//                StrokeThickness = 2,
//                Fill = randColor()
//            };
//
//            cv.Children.Add(rectangle);
//            rectangle.SetValue(Canvas.LeftProperty, (double)x);
//            rectangle.SetValue(Canvas.TopProperty, (double)y);
//
//            Shapes.Add(rectangle);
//            return rectangle;
//        }

//        public void move(UIElement selectedElement, Point getPosition)
//        {
//            selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X - Diff.X);
//            selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y - Diff.Y);
//        }

//        public void select(UIElement selectedElement, Point getPosition, Point InitialPosition, Canvas canvas)
//        {
//        }
//
//        public void unselect(UIElement selectedElement)
//        {
//            selectedElement.GetType().GetProperty("Stroke").SetValue(selectedElement, Brushes.Black);
//        }

//        public Brush randColor()
//        {
//            return (Brush)typeof(Brushes).GetProperties()[new Random().Next(typeof(Brushes).GetProperties().Length)].GetValue(null, null);
//        }

//        public void resize(UIElement selectedElement, Point getPosition, Point initialPosition, Canvas canvas)
//        {
//            if (getPosition.X < RelativePoint.X && getPosition.Y > RelativePoint.Y)
//            {
//                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
//                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - getPosition.X);
//                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
//            }
//            else if (getPosition.X > RelativePoint.X && getPosition.Y < RelativePoint.Y)
//            {
//                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
//                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
//                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
//            }
//            else if (getPosition.X < RelativePoint.X && getPosition.Y < RelativePoint.Y)
//            {
//                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
//                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
//                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - (int)getPosition.X);
//                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
//            }
//            else 
//            if (getPosition.X > RelativePoint.X && getPosition.Y > RelativePoint.Y)
//            {
//                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
//                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
//            }
//        }
    }
    }
