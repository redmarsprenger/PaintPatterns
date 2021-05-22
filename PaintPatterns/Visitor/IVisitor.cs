using System.Windows.Controls;
using PaintPatterns.Composite;

namespace PaintPatterns.Visitor
{
    public interface IVisitor
    {
        void VisitFigure(Figure figure);
        void VisitGroup(Group group);
    }
}