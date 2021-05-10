using PaintPatterns.Composite;
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
        bool moving = false;
        Shape shapeDrawing;

        CompositeShapes composite;

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

            composite = new CompositeShapes();
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
                    if (!moving)
                    {
                        moving = true;
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
                                Width = 20,
                                Height = 20,
                                Stroke = Brushes.Black,
                                StrokeThickness = 2,
                                Fill = (Brush)typeof(Brushes).GetProperties()[new Random().Next(typeof(Brushes).GetProperties().Length)].GetValue(null, null)
                            };

                            ellipse.Uid = Guid.NewGuid().ToString("N");

                            Canvas.Children.Add(ellipse);
                            ellipse.SetValue(Canvas.LeftProperty, InitialPosition.X);
                            ellipse.SetValue(Canvas.TopProperty, InitialPosition.Y);

                            shapeDrawing = ellipse;
                        }
                        else if (shape == "rectangle")
                        {
                            Rectangle rectangle = new Rectangle()
                            {
                                Width = 20,
                                Height = 20,
                                Stroke = Brushes.Black,
                                StrokeThickness = 2,
                                Fill = (Brush)typeof(Brushes).GetProperties()[new Random().Next(typeof(Brushes).GetProperties().Length)].GetValue(null, null)
                            };


                            rectangle.Uid = Guid.NewGuid().ToString("N");

                            Canvas.Children.Add(rectangle);
                            rectangle.SetValue(Canvas.LeftProperty, InitialPosition.X);
                            rectangle.SetValue(Canvas.TopProperty, InitialPosition.Y);

                            rectangle.SetCurrentValue(Canvas.LeftProperty, InitialPosition.X);
                            rectangle.SetCurrentValue(Canvas.TopProperty, InitialPosition.Y);

                            shapeDrawing = rectangle;
                        }
                        //composite.Add(shapeDrawing);
                    }
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
//            if (Mouse.LeftButton == MouseButtonState.Pressed)
//            {
//                if (drawing)
//                {
//                    invoker.Draw(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Canvas, shapeDrawing, false);
//                }
//                else if (mouseButtonHeld && selectedElement != null)
//                {
//                    if (e.Source != Canvas)
//                    {
//                        invoker.Move(selectedElement, e.GetPosition(Canvas), Diff, InitialPosition, Canvas, shapeDrawing, false, false);
//                    }
//                }
//            }
//            else if (Mouse.RightButton == MouseButtonState.Pressed && drawing)
//            {
//                if (shape == "none")
//                {
//                    invoker.Resize(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Canvas, shapeDrawing, false);
//                }
//            }
        }

        private void ChangeColor_Click(object sender, RoutedEventArgs e)
        {
            //            if (shape == "none" && selectedShape != null)
            //            {
            //                selectedShape = new RedDecorator(selectedShape).ReturnShape();
            //            }
            composite.Write();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (drawing)
                {
                    invoker.Draw(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Canvas, shapeDrawing, true, composite);
                }
                if (mouseButtonHeld && selectedElement != null && moving)
                {
                    invoker.Move(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Diff, Canvas, shapeDrawing, true, composite);
                }
            }
            if (e.ChangedButton == MouseButton.Right && drawing)
            {
                if (shape == "none")
                {
                    invoker.Resize(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Canvas, shapeDrawing, true, composite);
                }
            }

            mouseButtonHeld = false;
            firstPos = new Point();
            drawing = false;
            moving = false;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift && e.Key == Key.Z)
                {
                    invoker.Redo(selectedElement, firstPos, RelativePoint, InitialPosition, Canvas, shapeDrawing, composite);
                }
                else if (e.Key == Key.Z)
                {
                    invoker.Undo(selectedElement, firstPos, RelativePoint, InitialPosition, Canvas, shapeDrawing, composite);
                }
            }
        }
    }
}
