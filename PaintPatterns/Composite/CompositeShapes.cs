using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PaintPatterns.Composite
{
    class CompositeShapes
    {
        private List<Group> ChildElements = new List<Group>();

        public CompositeShapes()
        {
            ChildElements.Add(new Group());
        }
        public void Add(Shape shape)
        {
            ChildElements.First<Group>().Add(new Part(shape));
        }

        public void Add(Group group)
        {
            ChildElements.Add(group);
        }
    }
    class Group
    {
        List<Part> Parts;

        public void Add(Part part)
        {
            Parts.Add(part);
        }
        public void AddGroup(Group group)
        {
            Parts.Add( new Part(group));
        }

    }

    class Part
    {
        private Shape Shape = null;
        private Group Group = null;

        public Part(Shape shape)
        {
            Shape = shape;
        }

        public Part(Group group)
        {
            Group = group;
        }
    }
}
