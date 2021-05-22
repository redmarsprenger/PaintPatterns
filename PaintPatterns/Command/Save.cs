using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PaintPatterns.Composite;

namespace PaintPatterns.Command
{
    class Save
    {
        private readonly CompositeShapes composite;
        public Save(CompositeShapes composite)
        {
            this.composite = composite;
        }

        public void Execute()
        {
            //Do visitor shit
            composite.Write();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public UIElement GetElement()
        {
            return null;
        }

        public Shape GetShape()
        {
            return null;
        }
    }
}
