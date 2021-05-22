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
        public List<IComponent> Parts;

        /// <summary>
        /// initialises a Group object
        /// </summary>
        public Group()
        {
            Parts = new List<IComponent>();
        }

        /// <summary>
        /// Adds a part to Parts
        /// </summary>
        /// <param name="part"></param>
        public void Add(Figure part)
        {
            Parts.Add(part);
        }

        /// <summary>
        /// Adds a Group as a part to Parts
        /// </summary>
        /// <param name="group"></param>
        public void Add(Group group)
        {
            Parts.Add(new Group());
        }

        /// <summary>
        /// Updates the clone using the updated UIElement
        /// </summary>
        /// <param name="element"></param>
        public void Update(UIElement element)
        {
            foreach (IComponent item in Parts)
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
            foreach (IComponent item in Parts)
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
            IComponent part = null;
            foreach (IComponent item in Parts)
            {
                if (typeof(Figure).Equals(item.GetType()))
                {
                    Figure figure = (Figure)item;
                    if (figure.GetFigure().Uid == uid)
                    {
                        //so the list doesn't crash because of a removed entry
                        part = item;
                    }
                }
                if (typeof(Group).Equals(item.GetType()))
                {
                    Group group = (Group)item;
                    group.Remove(uid);
                }
            }
            Parts.Remove(part);
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

        public List<IComponent> GetParts()
        {
            return Parts;
        }
    }
}
