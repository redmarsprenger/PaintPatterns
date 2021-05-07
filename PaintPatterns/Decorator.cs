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

        public abstract Shape ReturnShape();
    }

    public class RedDecorator : ColorDecorator
    {
        public RedDecorator(Shape selectedShape)
        {
            SelectedShape = selectedShape;
            SelectedShape.Fill = draw.randColor();
        }

        public override Shape ReturnShape()
        {
            return SelectedShape;
        }
    }
}
