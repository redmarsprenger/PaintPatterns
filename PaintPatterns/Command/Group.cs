using PaintPatterns.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    class Group : ICommand
    {
        private readonly CompositeShapes composite;

        public Group(CompositeShapes composite)
        {
            this.composite = composite;
        }

        public void Execute()
        {
            composite.add
        }

        public UIElement GetElement()
        {
            return null;
        }

        public Shape GetShape()
        {
            return null;
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
