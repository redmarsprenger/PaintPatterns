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
