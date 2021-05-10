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
        public void Execute(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
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

        }

        public void Undo()
        {

        }
    }
}
