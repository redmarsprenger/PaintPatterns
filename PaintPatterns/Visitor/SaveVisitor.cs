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
            string line = "";
            foreach (var figure in group.Parts)
            {
                childCount = 0;
                var subGroup = figure.Value as Group;
                if (subGroup == null) continue;


                foreach (IComponent tempChild in subGroup.GetParts().Values)
                {
                    if (tempChild is Figure)
                    {
                        if (childCount == 0 && tabs > 0)
                        {
                            tabs--;
                        }
                        childCount++;
                    }
                }

                for (int i = 0; i < tabs; i++)
                {
                    indent += "    ";
                }

                line = indent + "group" + " " + childCount;
                lines.Add(line);
                tabs++;

                foreach (var fig in subGroup.Parts)
                {
                    fig.Value.Accept(this);
                }
            }

            foreach (string s in lines)
            {
                sw.WriteLine(s);
            }

        }
    }
}