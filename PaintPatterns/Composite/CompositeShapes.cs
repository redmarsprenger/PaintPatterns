using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintPatterns.Composite
{
    public class CompositeShapes
    {
        private Group ChildElements = new Group();

        public CompositeShapes()
        {
            ChildElements = new Group();
        }
        public void Add(Shape shape)
        {
            ChildElements.Add(new Part(shape));
        }

        public void Add(Group group)
        {
            ChildElements.Add(new Part(group));
        }

        public void Update(UIElement element)
        {
            ChildElements.Update(element);
        }

        public void Update(Shape shape)
        {
            ChildElements.Update(shape);
        }

        public void Remove(string uid)
        {
            ChildElements.Remove(uid);
        }
    }

    public class Group
    {
        public List<Part> Parts;
        public Group()
        {
            Parts = new List<Part>();
        }

        public void Add(Part part)
        {
            Parts.Add(part);
        }
        public void Add(Group group)
        {
            Parts.Add(new Part(group));
        }

        public void Update(UIElement element)
        {
            foreach (Part item in Parts)
            {
                if (item.Shape.Uid == element.Uid)
                {
                    //item.Shape = (Shape)element;
                    item.Shape.SetValue(Canvas.LeftProperty, element.GetValue(Canvas.LeftProperty));
                    item.Shape.SetValue(Canvas.TopProperty, element.GetValue(Canvas.TopProperty));
                    item.Shape.GetType().GetProperty("Width").SetValue(element, element.GetType().GetProperty("Width").GetValue(element));
                    item.Shape.GetType().GetProperty("Height").SetValue(element, element.GetType().GetProperty("Height").GetValue(element));
                }
                else if (item.Shape == null)
                {
                    Update(element);
                }
            }
        }

        public void Update(Shape shape)
        {
            foreach (Part item in Parts)
            {
                if (item.Shape.Uid == shape.Uid)
                {
                    item.Shape = shape;
                }
                else if (item.Shape == null)
                {
                    Update(shape);
                }
            }
        }

        public void Remove(string uid)
        {
            foreach (Part item in Parts)
            {
                if (item.Shape.Uid == uid)
                {
                    Parts.Remove(item);
                }
                else if (item.Shape == null)
                {
                    Remove(uid);
                }
            }
        }

    }

    public class Part
    {
        public Shape Shape = null;
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
