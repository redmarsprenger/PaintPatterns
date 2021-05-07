using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintPatterns
{
    public abstract class ColorDecorator
    {
        public Shape SelectedShape;
    }

    public class RedDecorator : ColorDecorator
    {
        private new Shape SelectedShape;

        public RedDecorator(Shape selectedShape)
        {
            SelectedShape = selectedShape;
            SelectedShape.Stroke = Brushes.Red;
        }

        public Shape ReturnShape()
        {
            return SelectedShape;
        }
    }
}
