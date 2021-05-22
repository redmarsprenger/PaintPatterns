using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PaintPatterns.Composite;

namespace PaintPatterns.Visitor
{
    public class SaveVisitor : IVisitor
    {
        private UIElement element;
        private Shape shape;
        string path = System.IO.Directory.GetCurrentDirectory() + "save.txt";
        public SaveVisitor(UIElement element, Shape shape)
        {
            this.element = element;
            this.shape = shape;
        }

        public void Execute()
        {

        }
    }
}