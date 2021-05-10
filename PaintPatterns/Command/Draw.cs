using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    class Draw : ICommand
    {
        private Point oldPos;
        private UIElement selectedElement;
        private Point getPosition;
        private Point relativePoint;
        private Point initialPosition;
        private Canvas canvas;
        private Shape shapeDrawing;
        private bool done;

        public Draw(UIElement selectedElement, Point getPosition, Point relativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, bool done)
        {
            this.selectedElement = selectedElement;
            this.getPosition = getPosition;
            this.relativePoint = relativePoint;
            this.initialPosition = initialPosition;
            this.canvas = canvas;
            this.shapeDrawing = shapeDrawing;
            this.done = done;
        }

        public void Execute()
        {
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
    }
}
