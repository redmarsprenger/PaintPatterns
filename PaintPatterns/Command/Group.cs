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
        //private readonly CompositeShapes composite;
        private readonly Stack<Shape> shapes;
        private readonly Composite.Group group;

        public Group(Stack<Shape> shapes)
        {
            this.shapes = shapes;
            group = new Composite.Group(this.shapes);
        }

        public void Execute()
        {
            //composite.Add(shapes);
            //this.group = new Group(shapes);
        }

        public UIElement GetElement()
        {
            return null;
        }

        public Shape GetShape()
        {
            return null;
        }

        public Composite.Group GetGroup()
        {
            return group;
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        Composite.Group ICommand.GetGroup()
        {
            return group;
        }
    }
}
