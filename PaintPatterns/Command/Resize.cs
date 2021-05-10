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
    class Resize : ICommand
    {
        private Point oldPos;
        private UIElement selectedElement;
        private Point getPosition;
        private Point RelativePoint;
        private Point initialPosition;
        private Canvas canvas;
        private Shape shapeDrawing;
        private bool done;

        public Resize(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, bool done)
        {
            this.selectedElement = selectedElement;
            this.getPosition = getPosition;
            this.RelativePoint = RelativePoint;
            this.initialPosition = initialPosition;
            this.canvas = canvas;
            this.shapeDrawing = shapeDrawing;
            this.done = done;
        }

        public void Execute()
        {
            if (selectedElement == null)
            {
                selectedElement = shapeDrawing;
            }
            if (getPosition.X < RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
            else if (getPosition.X > RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else if (getPosition.X < RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - (int)getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else
            if (getPosition.X > RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
        }

        public void Redo()
        {
            if (selectedElement == null)
            {
                selectedElement = shapeDrawing;
            }
            if (getPosition.X < RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
            else if (getPosition.X > RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else if (getPosition.X < RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - (int)getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else
            if (getPosition.X > RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
        }

        public void Undo()
        {
            if (selectedElement == null)
            {
                selectedElement = shapeDrawing;
            }
            if (getPosition.X < RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
            else if (getPosition.X > RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else if (getPosition.X < RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - (int)getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else
            if (getPosition.X > RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
        }
    }
}
