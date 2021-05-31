using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using PaintPatterns.Visitor;

namespace PaintPatterns.Composite
{
    public class Figure : IComponent
    {
        private Shape figure;
        public Figure(Shape shape)
        {
            figure = shape;
        }
        public Shape GetFigure()
        {
            return figure;
        }

        public void UpdateContent(Shape shape)
        {
            figure = shape;
        }

        public void WriteContent(StreamWriter sw, int tabs)
        {
//            for (int i = 0; i < tabs; i++)
//            {
//                sw.Write("    ");
//            }
//            if (typeof(Rectangle).Equals(figure.GetType()))
//            {
//                sw.WriteLine("rectangle " + figure.GetValue(Canvas.LeftProperty) + " " + figure.GetValue(Canvas.TopProperty) + " " + figure.Width + " " + figure.Height);
//            }
//            else
//            {
//                sw.WriteLine("ellipse " + figure.GetValue(Canvas.LeftProperty) + " " + figure.GetValue(Canvas.TopProperty) + " " + figure.Width + " " + figure.Height);
//            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitFigure(this);
        }

        public void Add(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
