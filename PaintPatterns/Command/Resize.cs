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
        }

        public Resize(Rectangle selectedShape)
        {
        }

        public void Execute()
        {
            
        }

        public void Redo()
        {
        }

        public void Undo()
        {
        }
    }
}
