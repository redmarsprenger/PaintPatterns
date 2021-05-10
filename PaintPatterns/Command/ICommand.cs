using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing);
        void Redo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing);
    }
}

