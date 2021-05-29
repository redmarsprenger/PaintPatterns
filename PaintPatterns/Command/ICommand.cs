using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    /// <summary>
    /// ICommand interface
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
        UIElement GetElement();
        Shape GetShape();
        Composite.Group GetGroup();
    }
}

