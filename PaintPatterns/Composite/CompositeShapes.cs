using System;
using System.Collections.Generic;
using System.IO;
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

        public void Write()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "help.txt";

            //if (!File.Exists(path))
            //{
                using (StreamWriter sw = File.CreateText(path))
                {
                    int tabs = 0;
                    ChildElements.Write(sw, tabs);
                }
            //}
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
                    //item.Shape.SetValue(Canvas.LeftProperty, element.GetValue(Canvas.LeftProperty));
                    //item.Shape.SetValue(Canvas.TopProperty, element.GetValue(Canvas.TopProperty));
                    //item.Shape.GetType().GetProperty("Width").SetValue(element, element.GetType().GetProperty("Width").GetValue(element));
                    //item.Shape.GetType().GetProperty("Height").SetValue(element, element.GetType().GetProperty("Height").GetValue(element));
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

        public void Write(StreamWriter sw, int tabs)
        {
            //write group stuff
            sw.WriteLine("group " + Parts.Count.ToString());

            tabs++; // increment tabs for the rest of shapes and a possible group
            foreach (Part item in Parts)
            {
                if (item.Group != null)
                {
                    item.Group.Write(sw, tabs);
                }
                else
                {
                    item.Write(sw, tabs);
                }
            }
        }

    }

    public class Part
    {
        public Shape Shape = null;
        public Group Group = null;

        public Part(Shape shape)
        {
            Shape = shape;
        }

        public Part(Group group)
        {
            Group = group;
        }

        public void Write(StreamWriter sw, int tabs)
        {
            for (int i = 0; i < tabs; i++)
            {
                sw.Write("    ");
            }
            if (typeof(Rectangle).Equals(Shape.GetType()))
            {
                sw.WriteLine("rectangle " + Shape.GetValue(Canvas.LeftProperty) + " " + Shape.GetValue(Canvas.TopProperty) + " " + Shape.Width + " " + Shape.Height);
            }
            else
            {
                sw.WriteLine("ellipse " + Shape.GetValue(Canvas.LeftProperty) + " " + Shape.GetValue(Canvas.TopProperty) + " " + Shape.Width + " " + Shape.Height);
            }
        }
    }
}
