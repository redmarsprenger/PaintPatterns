using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using PaintPatterns.Visitor;

namespace PaintPatterns.Composite
{
    public class Group : IComponent
    {
        /// <summary>
        /// list of parts
        /// </summary>
        public Dictionary<string, IComponent> Parts;

        public bool found;

        /// <summary>
        /// initialises a Group object
        /// </summary>
        public Group()
        {
            Parts = new Dictionary<string, IComponent>();
            found = false;
        }

        public Group(Stack<Shape> shapes)
        {
            Parts = new Dictionary<string, IComponent>();
            foreach (Shape shape in shapes)
            {
                string key = shape.Uid;
                Figure figure = new Figure(shape);
                Parts.Add(key, figure);
            }
        }

        /// <summary>
        /// Adds a part to Parts
        /// </summary>
        /// <param name="part"></param>
        public void Add(Shape shape)
        {
            Figure figure = new Figure(shape);
            Parts.Add(shape.Uid, figure);
        }

        /// <summary>
        /// Adds a Group as a part to Parts
        /// </summary>
        /// <param name="group"></param>
        public void Add(Group group)
        {
            if (found == true)
            {
                Parts.Add(Parts.Count.ToString(), group);
                found = false;
            }
            else
            {
                foreach (IComponent component in Parts.Values)
                {
                    if (typeof(Group).Equals(component.GetType()))
                    {
                        component.Add(group);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the clone using the updated UIElement
        /// </summary>
        /// <param name="element"></param>
        public void Update(UIElement element)
        {
            foreach (IComponent item in Parts.Values)
            {
                if (typeof(Figure).Equals(item.GetType()))
                {
                    //if (element != null)
                    Figure figure = (Figure)item;

                    if (figure.GetFigure().Uid == element.Uid)
                    {
                        figure.UpdateContent((Shape)element);
                        //item.Shape.SetValue(Canvas.LeftProperty, element.GetValue(Canvas.LeftProperty));
                        //item.Shape.SetValue(Canvas.TopProperty, element.GetValue(Canvas.TopProperty));
                        //item.Shape.GetType().GetProperty("Width").SetValue(element, element.GetType().GetProperty("Width").GetValue(element));
                        //item.Shape.GetType().GetProperty("Height").SetValue(element, element.GetType().GetProperty("Height").GetValue(element));
                    }
                    //}
                }
                else if (typeof(Group).Equals(item.GetType()))
                {
                    Group group = (Group)item;
                    group.Update(element);
                }
            }
        }

        /// <summary>
        /// Updates the clonse using the updated Shape
        /// </summary>
        /// <param name="shape"></param>
        public void Update(Shape element)
        {
            foreach (IComponent item in Parts.Values)
            {
                if (typeof(Figure).Equals(item.GetType()))
                {
                    //if (element != null)
                    Figure figure = (Figure)item;

                    if (figure.GetFigure().Uid == element.Uid)
                    {
                        figure.UpdateContent(element);
                        //item.Shape.SetValue(Canvas.LeftProperty, element.GetValue(Canvas.LeftProperty));
                        //item.Shape.SetValue(Canvas.TopProperty, element.GetValue(Canvas.TopProperty));
                        //item.Shape.GetType().GetProperty("Width").SetValue(element, element.GetType().GetProperty("Width").GetValue(element));
                        //item.Shape.GetType().GetProperty("Height").SetValue(element, element.GetType().GetProperty("Height").GetValue(element));
                    }
                    //else if (item.Shape == null)
                    //{
                    //    Update(element);
                    //}
                    //}
                }
                else if (typeof(Group).Equals(item.GetType()))
                {
                    Group group = (Group)item;
                    group.Update(element);
                }
            }
        }

        /// <summary>
        /// Removes a entry using a id
        /// </summary>
        /// <param name="uid"></param>
        public void Remove(string uid)
        {
            string num = "";
            foreach (string key in Parts.Keys)
            {
                IComponent item = null;
                Parts.TryGetValue(key, out item);

                if (typeof(Figure).Equals(item.GetType()))
                {
                    Figure figure = (Figure)item;
                    if (figure.GetFigure().Uid == uid)
                    {
                        //so the list doesn't crash because of a removed entry
                        num = key;
                    }
                }
                if (typeof(Group).Equals(item.GetType()))
                {
                    Group group = (Group)item;
                    group.Parts.Remove(uid);
                    //item = group;
                }
            }

            if (num != "")
            {
                Parts.Remove(num);
            }
        }

        /// <summary>
        /// Writes the group data out to a file
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="tabs"></param>
        public void WriteContent(StreamWriter sw, int tabs)
        {
//            //write group stuff
//            sw.WriteLine("group " + Parts.Count.ToString());
//
//            tabs++; // increment tabs for the rest of shapes and a possible group
//            foreach (IComponent item in Parts)
//            {
//                item.WriteContent(sw, tabs);
//            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitGroup(this);
        }

        public Dictionary<string, IComponent> GetParts()
        {
            return Parts;
        }

        public Group Find(string uid)
        {
            Group group = null;
            foreach (string key in Parts.Keys)
            {
                //IComponent component = null;
                Parts.TryGetValue(key, out IComponent component);
                if (typeof(Group).Equals(component.GetType()))
                {
                    //can't use Find from component but putting it in another variable works
                    Group thing = (Group)component;
                    group = thing.Find(uid);

                    //if parts is count 0 it is a newly made group so it is the group that we want
                    if (group.Parts.Count == 0)
                    {
                        //so we return component because that is the whole not edited group
                        return (Group)component;
                    }
                }
                if (typeof(Figure).Equals(component.GetType()))
                {
                    if (uid == key)
                    {
                        //set found to true so when we need to add a the group we just search for the "found" group and put it in there
                        found = true;
                        Group newGroup = new Group();
                        //return a new group as a identifier of what we need
                        return newGroup;
                    }
                }
            }
            return group;
        }
    }
}
