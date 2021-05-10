using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            SelectedShape.Fill = randColor();
        }

        public Brush randColor()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        public override Shape ReturnShape()
        {
            return SelectedShape;
        }
    }
}
