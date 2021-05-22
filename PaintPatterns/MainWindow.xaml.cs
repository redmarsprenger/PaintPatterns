using PaintPatterns.Composite;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintPatterns.Strategy;

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
        private bool mouseButtonHeld;

        public UIElement selectedElement = null;
        Shape selectedShape;
        private readonly Stack<Shape> selectedShapes = new Stack<Shape>();
        IStrategy strategy;

        Point firstPos;
        bool drawing = false;
        bool moving = false;
        public Shape shapeDrawing;

        private static Point Diff;
        private static Point RelativePoint;

        /// <summary>
        /// MainWindow Constructor
        /// InitializeComponent
        /// Disable NoneBtn
        /// Set composite and invoker
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NoneBtn.IsEnabled = false;

            invoker = CommandInvoker.GetInstance();
            invoker.MainWindow = this;
        }

        /// <summary>
        /// On NoneBtn_Click disable NoneBtn enable other buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoneBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "none";
            NoneBtn.IsEnabled = false;
            RectangleBtn.IsEnabled = true;
            EllipseBtn.IsEnabled = true;
        }

        /// <summary>
        /// On EllipseBtn_Click disable EllipseBtn enable other buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "rectangle";
            NoneBtn.IsEnabled = true;
            RectangleBtn.IsEnabled = false;
            EllipseBtn.IsEnabled = true;
            //Set strategy to Rectangle
            strategy = new Strategy.Rectangle();
        }

        /// <summary>
        /// On EllipseBtn_Click disable EllipseBtn enable other buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            shape = "ellipse";
            NoneBtn.IsEnabled = true;
            RectangleBtn.IsEnabled = true;
            EllipseBtn.IsEnabled = false;
            //Set strategy to Ellipse
            strategy = new Strategy.Ellipse();
        }

        /// <summary>
        /// if shape none set selectedShape, selectedElement and RelativePoint
        ///
        /// if Mouse.LeftButton and shape != none execute strategy and add composite.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseButtonHeld = true;
            InitialPosition = e.GetPosition(Canvas);
            if (shape == "none")
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)
                {
                    if (selectedShapes != null)
                    {
                        while (selectedShapes?.Count > 0)
                        {
                            selectedShapes.Pop().Stroke = Brushes.Black;
                        }
                        selectedShapes?.Clear();
                    }
                }
                if (!Equals(e.Source, Canvas))
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
                        selectedShapes?.Push(selectedShape);
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
                        //composite.Add(shapeDrawing);
                    }
                }
            }
        }

        /// <summary>
        /// Calls corresponding invoker depending on mousebutton and if drawing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            MousePos = e.GetPosition(Canvas);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (drawing)
                {
                    invoker.Draw(selectedElement, e.GetPosition(Canvas), InitialPosition, Canvas, shapeDrawing, null, false);
                }
                if (mouseButtonHeld && selectedElement != null && moving)
                {
                    invoker.Move(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Diff, Canvas, shapeDrawing, false);
                }
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed && drawing)
            {
                if (shape == "none")
                {
                    invoker.Resize(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Canvas, shapeDrawing, false);
                }
            }
        }

        /// <summary>
        /// Do last invoker call with done=true
        /// Reset mouseButtonHeld, firstPos, drawing, moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (drawing)
                {
                    invoker.Draw(selectedElement, e.GetPosition(Canvas), InitialPosition, Canvas, shapeDrawing, null, true);
                }
                if (mouseButtonHeld && selectedElement != null && moving)
                {
                    invoker.Move(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Diff, Canvas, shapeDrawing, true);
                }
            }
            if (e.ChangedButton == MouseButton.Right && drawing)
            {
                if (shape == "none")
                {
                    invoker.Resize(selectedElement, e.GetPosition(Canvas), RelativePoint, InitialPosition, Canvas, shapeDrawing, true);
                }
            }

            mouseButtonHeld = false;
            firstPos = new Point();
            drawing = false;
            moving = false;
        }

        /// <summary>
        /// CTRL+SHIFT+Z -> invoker.Redo
        /// CTRL+Z -> invoker.Undo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift && e.Key == Key.Z)
                {
                    invoker.Redo(selectedElement, firstPos, RelativePoint, InitialPosition, Canvas, shapeDrawing);
                }
                else if (e.Key == Key.Z)
                {
                    invoker.Undo(selectedElement, firstPos, RelativePoint, InitialPosition, Canvas, shapeDrawing);
                }
            }
        }

        /// <summary>
        /// On ChangeColor_Click Composite Write
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            invoker.Save();
        }

        private void LoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            invoker.Load();
            //            composite.Load();
        }
    }
}
