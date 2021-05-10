using PaintPatterns.Composite;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintPatterns.StrategyPattern;

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
        public Point MousePos;
        public Point SelectPos;
        private bool mouseButtonHeld;

        public UIElement selectedElement = null;
        Shape selectedShape;
        IStrategy strategy;

        Point firstPos;
        bool drawing = false;
        bool moving = false;
        public Shape shapeDrawing;

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

            composite = new CompositeShapes();
            invoker = CommandInvoker.GetInstance();
            invoker.MainWindow = this;
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
            strategy = new StrategyPattern.Rectangle();
            NoneBtn.IsEnabled = true;
            RectangleBtn.IsEnabled = false;
            EllipseBtn.IsEnabled = true;
        }

        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "ellipse";
            strategy = new StrategyPattern.Ellipse();
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

                        if (shape != "ellipse" && shape != "rectangle") return;
                        strategy.Execute();
                        composite.Add(shapeDrawing);
                    }
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            MousePos = e.GetPosition(Canvas);
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
                    invoker.Draw(selectedElement, e.GetPosition(Canvas), InitialPosition, Canvas, shapeDrawing, null);
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
