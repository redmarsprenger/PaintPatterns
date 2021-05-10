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

        /// <summary>
        /// initialise CompositeShapes
        /// </summary>
        public CompositeShapes()
        {
            ChildElements = new Group();
        }
        /// <summary>
        /// adds a shape
        /// </summary>
        /// <param name="shape"></param>
        public void Add(Shape shape)
        {
            ChildElements.Add(new Part(shape));
        }

        /// <summary>
        /// Adds a group
        /// </summary>
        /// <param name="group"></param>
        public void Add(Group group)
        {
            ChildElements.Add(new Part(group));
        }

        /// <summary>
        /// Updates the cloned shape using a UIElement
        /// </summary>
        /// <param name="element"></param>
        public void Update(UIElement element)
        {
            ChildElements.Update(element);
        }

        /// <summary>
        /// Updates the cloned shape using a shape
        /// </summary>
        /// <param name="shape"></param>
        public void Update(Shape shape)
        {
            ChildElements.Update(shape);
        }

        /// <summary>
        /// Removes the cloned entry using a id
        /// </summary>
        /// <param name="uid"></param>
        public void Remove(string uid)
        {
            ChildElements.Remove(uid);
        }

        /// <summary>
        /// writes the CompositeShapes to a file
        /// </summary>
        public void Write()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "help.txt";

            //get all the data and write it out
            using (StreamWriter sw = File.CreateText(path))
            {
                int tabs = 0;
                ChildElements.Write(sw, tabs);
            }
        }
    }

    public class Group
    {
        /// <summary>
        /// list of parts
        /// </summary>
        public List<Part> Parts;
        
        /// <summary>
        /// initialises a Group object
        /// </summary>
        public Group()
        {
            Parts = new List<Part>();
        }

        /// <summary>
        /// Adds a part to Parts
        /// </summary>
        /// <param name="part"></param>
        public void Add(Part part)
        {
            Parts.Add(part);
        }

        /// <summary>
        /// Adds a Group as a part to Parts
        /// </summary>
        /// <param name="group"></param>
        public void Add(Group group)
        {
            Parts.Add(new Part(group));
        }

        /// <summary>
        /// Updates the clone using the updated UIElement
        /// </summary>
        /// <param name="element"></param>
        public void Update(UIElement element)
        {
            foreach (Part item in Parts)
            {
                if (item.Shape.Uid == element.Uid)
                {
                    item.Shape = (Shape)element;
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

        /// <summary>
        /// Updates the clonse using the updated Shape
        /// </summary>
        /// <param name="shape"></param>
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

        /// <summary>
        /// Removes a entry using a id
        /// </summary>
        /// <param name="uid"></param>
        public void Remove(string uid)
        {
            Part part = null;
            foreach (Part item in Parts)
            {
                if (item.Shape.Uid == uid)
                {
                    //so the list doesn't crash because of a removed entry
                    part = item;
                }
                else if (item.Shape == null)
                {
                    Remove(uid);
                }
            }
            Parts.Remove(part);
        }

        /// <summary>
        /// Writes the group data out to a file
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="tabs"></param>
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
        /// <summary>
        /// These are either a Shape or a Group never both
        /// </summary>
        public Shape Shape = null;
        public Group Group = null;

        /// <summary>
        /// Creates a Part using a Shape
        /// </summary>
        /// <param name="shape"></param>
        public Part(Shape shape)
        {
            Shape = shape;
        }

        /// <summary>
        /// Creates a Part using a Group
        /// </summary>
        /// <param name="group"></param>
        public Part(Group group)
        {
            Group = group;
        }

        /// <summary>
        /// Writes the Part data to a file
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="tabs"></param>
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
