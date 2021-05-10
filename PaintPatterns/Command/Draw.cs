using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    class Draw : ICommand
    {
        private UIElement selectedElement;
        private Point getPosition;
        private Point initialPosition;
        private Canvas canvas;
        private Shape shapeDrawing;
        private Shape newShape;

        public Draw(UIElement selectedElement, Point getPosition, Point initialPosition, Canvas canvas, Shape shapeDrawing, Shape shape)
        {
            this.selectedElement = selectedElement;
            this.getPosition = getPosition;
            this.initialPosition = initialPosition;
            this.canvas = canvas;
            this.shapeDrawing = shapeDrawing;
            this.newShape = shape;
        }

        public void Execute()
        {
            if (newShape != null)
            {
                newShape.Width = 20;
                newShape.Height = 20;
                newShape.Stroke = Brushes.Black;
                newShape.StrokeThickness = 2;
                newShape.Fill = (Brush)typeof(Brushes).GetProperties()[new Random().Next(typeof(Brushes).GetProperties().Length)].GetValue(null, null);

                canvas.Children.Add(newShape);
                newShape.SetValue(Canvas.LeftProperty, initialPosition.X);
                newShape.SetValue(Canvas.TopProperty, initialPosition.Y);

                shapeDrawing = newShape;
            }

            if (getPosition.X < initialPosition.X && getPosition.Y > initialPosition.Y)
            {
                shapeDrawing.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                shapeDrawing.Width = (int)initialPosition.X - (int)getPosition.X;
                shapeDrawing.Width = (int)initialPosition.X - (int)getPosition.X;
                shapeDrawing.Height = (int)getPosition.Y - (int)initialPosition.Y;
            }
            else if (getPosition.X > initialPosition.X && getPosition.Y < initialPosition.Y)
            {
                shapeDrawing.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                shapeDrawing.Width = (int)getPosition.X - (int)initialPosition.X;
                shapeDrawing.Height = (int)initialPosition.Y - (int)getPosition.Y;
            }
            else if (getPosition.X < initialPosition.X && getPosition.Y < initialPosition.Y)
            {
                shapeDrawing.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                shapeDrawing.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                shapeDrawing.Width = (int)initialPosition.X - (int)getPosition.X;
                shapeDrawing.Height = (int)initialPosition.Y - (int)getPosition.Y;
            }
            else if (getPosition.X > initialPosition.X && getPosition.Y > initialPosition.Y)
            {
                shapeDrawing.Width = (int)getPosition.X - (int)initialPosition.X;
                shapeDrawing.Height = (int)getPosition.Y - (int)initialPosition.Y;
            }
        }

        public void Redo()
        {
            canvas.Children.Add(shapeDrawing);
        }

        public void Undo()
        {
            canvas.Children.Remove(shapeDrawing);
        }

        public UIElement GetElement()
        {
            return selectedElement;
        }

        public Shape GetShape()
        {
            return shapeDrawing;
        }
    }
}
