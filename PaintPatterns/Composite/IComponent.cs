using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintPatterns.Visitor;

namespace PaintPatterns.Composite
{
    public interface IComponent
    {
        void WriteContent(StreamWriter sw, int tabs); 
        void Accept(IVisitor visitor);
        void Add(Group group);
    }
}
