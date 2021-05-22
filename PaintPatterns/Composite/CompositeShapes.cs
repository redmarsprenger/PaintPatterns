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
        private Group PaintObjects = new Group();

        /// <summary>
        /// initialise CompositeShapes
        /// </summary>
        public CompositeShapes()
        {
            PaintObjects = new Group();
        }

        /// <summary>
        /// adds a shape
        /// </summary>
        /// <param name="shape"></param>
        public void Add(Shape shape)
        {
            PaintObjects.Add(new Figure(shape));
        }

        /// <summary>
        /// Adds a group
        /// </summary>
        /// <param name="group"></param>
        public void Add(Group group)
        {
            PaintObjects.Add(group);
        }

        /// <summary>
        /// Updates the cloned shape using a UIElement
        /// </summary>
        /// <param name="element"></param>
        public void Update(UIElement element)
        {
            PaintObjects.Update(element);
        }

        /// <summary>
        /// Updates the cloned shape using a shape
        /// </summary>
        /// <param name="shape"></param>
        public void Update(Shape shape)
        {
            PaintObjects.Update(shape);
        }

        /// <summary>
        /// Removes the cloned entry using a id
        /// </summary>
        /// <param name="uid"></param>
        public void Remove(string uid)
        {
            PaintObjects.Remove(uid);
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
                //recursive call
                PaintObjects.WriteContent(sw, tabs);
            }
        }


        /// <summary>
        /// Loads content from file to canvas
        /// </summary>
        public void Load()
        {
            throw new NotImplementedException();
        }
    }

    public interface IComponent
    {
        //void GetContent();
        //void UpdateContent();
        //int Definer();
        void WriteContent(StreamWriter sw, int tabs);
    }

    public class Figure : IComponent
    {
        private Shape figure;
        public Figure(Shape shape)
        {
            figure = shape;
        }
        public Shape GetFigure()
        {
            return figure;
        }

        public void UpdateContent(Shape shape)
        {
            figure = shape;
        }

        public void WriteContent(StreamWriter sw, int tabs)
        {
            for (int i = 0; i < tabs; i++)
            {
                sw.Write("    ");
            }
            if (typeof(Rectangle).Equals(figure.GetType()))
            {
                sw.WriteLine("rectangle " + figure.GetValue(Canvas.LeftProperty) + " " + figure.GetValue(Canvas.TopProperty) + " " + figure.Width + " " + figure.Height);
            }
            else
            {
                sw.WriteLine("ellipse " + figure.GetValue(Canvas.LeftProperty) + " " + figure.GetValue(Canvas.TopProperty) + " " + figure.Width + " " + figure.Height);
            }
        }
    }

    public class Group:IComponent
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

        //public void Update()
        //{
        //    Add()
        //}

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
                    //else if (item.Shape == null)
                    //{
                    //    Remove(uid);
                    //}
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
            //write group stuff
            sw.WriteLine("group " + Parts.Count.ToString());

            tabs++; // increment tabs for the rest of shapes and a possible group
            foreach (IComponent item in Parts)
            {
                //if (typeof(Figure).Equals(item.GetType()))
                //{
                    item.WriteContent(sw, tabs);
                //}
                //else if (typeof(Group).Equals(item.GetType()))
                //{
                    //item.WriteContent(sw, tabs);
                //}
            }
        }
    }

}
