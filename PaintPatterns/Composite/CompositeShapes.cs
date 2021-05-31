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
            PaintObjects.Add(shape);
        }

        /// <summary>
        /// Adds a group
        /// </summary>
        /// <param name="group"></param>
        public void Add(Group group)
        {
            var first = group.Parts.First();
            string firstKey = first.Key;
            List<string> removeList = new List<string>();

            //searches for the group that needs to be edited and changes found to true there
            Group found = PaintObjects.Find(firstKey);

            //adds everything that needs to be deleted to a list so a mutation error doesn't occur
            foreach (string key in group.Parts.Keys)
            {
                removeList.Add(key);
            }

            //go through list and remove everything
            foreach (string key in removeList)
            {
                PaintObjects.Remove(key);
            }
            removeList.Clear();

            //add all shapes to the found group
            PaintObjects.Add(group);
        }

        public Composite.Group Find(string uid)
        {
            foreach (string key in PaintObjects.Parts.Keys)
            {
                //Composite.Group group = null;
                PaintObjects.Parts.TryGetValue(key, out IComponent component);


                if (uid == key)
                {
                    return PaintObjects;
                }
                else
                {
                    if (typeof(Group).Equals(component.GetType()))
                    {
                        //PaintObjects.Parts.TryGetValue(key, out IComponent next);
                        Group sub = (Group)component;
                        return sub.Find(uid);
                    }
                    
                }
            }

            return null;
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
//            string path = System.IO.Directory.GetCurrentDirectory() + "help.txt";
//
//            //get all the data and write it out
//            using (StreamWriter sw = File.CreateText(path))
//            {
//                int tabs = 0;
//                //recursive call
//                PaintObjects.WriteContent(sw, tabs);
//            }
        }

        /// <summary>
        /// Loads content from file to canvas
        /// </summary>
        public void Load()
        {
            throw new NotImplementedException();
        }

        public Group GetPaintObjects()
        {
            return PaintObjects;
        }
    }
}
