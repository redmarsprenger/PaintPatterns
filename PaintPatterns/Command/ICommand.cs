using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
        UIElement GetElement();
        Shape GetShape();
    }
}

