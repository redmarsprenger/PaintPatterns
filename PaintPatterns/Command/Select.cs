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
    class Select
    {
        public void Execute(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, bool done)
        {
            Point Diff = new Point();
            Point relativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
            Diff.X = getPosition.X - relativePoint.X;
            Diff.Y = getPosition.Y - relativePoint.Y;

            selectedElement.GetType().GetProperty("Stroke")?.SetValue(selectedElement, Brushes.Blue);
            RelativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
        }

        public void Redo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            Point Diff = new Point();
            Point relativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
            Diff.X = getPosition.X - relativePoint.X;
            Diff.Y = getPosition.Y - relativePoint.Y;

            selectedElement.GetType().GetProperty("Stroke")?.SetValue(selectedElement, Brushes.Blue);
            RelativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
        }

        public void Undo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            Point Diff = new Point();
            Point relativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
            Diff.X = getPosition.X - relativePoint.X;
            Diff.Y = getPosition.Y - relativePoint.Y;

            selectedElement.GetType().GetProperty("Stroke")?.SetValue(selectedElement, Brushes.Blue);
            RelativePoint = selectedElement.TransformToAncestor(canvas).Transform(new Point(0, 0));
        }
    }
}
