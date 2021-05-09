using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    class Resize : Actions
    {
        int x, y, width, height;
        string shape;
        public Resize(Ellipse selectedShape)
        {
            width = (int)selectedShape.Width;
            height = (int)selectedShape.Height;
            x = (int)selectedShape.GetValue(Canvas.LeftProperty);
            y = (int)selectedShape.GetValue(Canvas.TopProperty);
            shape = "ellipse";
        }

        public Resize(Rectangle selectedShape)
        {
            width = (int)selectedShape.Width;
            height = (int)selectedShape.Height;
            x = (int)selectedShape.GetValue(Canvas.LeftProperty);
            y = (int)selectedShape.GetValue(Canvas.TopProperty);
            shape = "rectangle";
        }

        public void Execute()
        {
            
        }

        public void Redo()
        {
            //add the undo entry to redo and remove from undo
        }

        public void Undo()
        {
            //write "shape + x + y + width + height"
        }
    }
}
