using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PaintPatterns.Composite;

namespace PaintPatterns.Visitor
{
    public class SaveVisitor : IVisitor
    {
        private readonly List<string> lines = new List<string>();
        private readonly Group root;
        private StreamWriter sw;
        private int tabs;

        public SaveVisitor(StreamWriter sw)
        {
            this.sw = sw;
        }

        public void VisitFigure(Figure figure)
        {
            string indent = "";
            for (int i = 0; i < tabs; i++)
            {
                indent += "    ";
            }

            string fig = typeof(Rectangle) == figure.GetFigure().GetType() ? "rectangle " : "ellipse ";
            fig += figure.GetFigure().GetValue(Canvas.LeftProperty) + " " + figure.GetFigure().GetValue(Canvas.TopProperty) + " " + figure.GetFigure().Width + " " + figure.GetFigure().Height;

            string line = indent + fig;
            lines.Add(line);
        }

        public void VisitGroup(Group group)
        {
            string indent = "";
            int childCount = 0;

            foreach (IComponent tempChild in group.GetParts().Values)
            {
                if (tempChild is Figure)
                {
                    childCount++;
                }
            }

            for (int i = 0; i < tabs; i++)
            {
                indent += "    ";
            }

            string line = indent + "group" + " " + childCount;
            lines.Add(line);
            tabs++;

            foreach (IComponent figure in group.GetParts().Values)
            {
                figure.Accept(this);
            }

            foreach (string s in lines)
                sw.WriteLine(s);
            sw.Close();
        }
    }
}