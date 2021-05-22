using System.Windows.Controls;
using PaintPatterns.Composite;

namespace PaintPatterns.Visitor
{
    public interface IVisitor
    {
        void Execute();
    }
}