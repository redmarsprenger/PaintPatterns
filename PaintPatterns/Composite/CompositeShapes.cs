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
            ////go through the group parts which are always shapes convert them to figures add to a group an insert in the first selected shape spot uid/dictionary id
            //foreach (IComponent item in group.Parts.Values)
            //{
            //    //go through the given group values and search for the ones in compositeShapes then remove those and insert the group in the highest id of the selected shapes
            //    if (typeof(Figure).Equals(item.GetType()))
            //    {
            //        //Figure figure = item;
            //        PaintObjects.Add((Figure)item);

            //    }
            //}
            foreach (string key in group.Parts.Keys)
            {
                PaintObjects.Remove(key);
            }

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
