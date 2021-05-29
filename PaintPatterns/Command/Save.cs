using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PaintPatterns.Composite;
using PaintPatterns.Visitor;

namespace PaintPatterns.Command
{
    class Save : ICommand
    {
        private readonly CommandInvoker invoker;

        public Save()
        {
            this.invoker = CommandInvoker.GetInstance();
        }

        public void Execute()
        {
            //Do visitor shit
            //new SaveVisitor();

            string path = System.IO.Directory.GetCurrentDirectory() + "help.txt";

            //get all the data and write it out
            using (StreamWriter sw = File.CreateText(path))
            {
                invoker.composite.GetPaintObjects().Accept(new SaveVisitor(sw));
            }
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public UIElement GetElement()
        {
            return null;
        }

        public Shape GetShape()
        {
            return null;
        }

        public Stack<Shape> GetStack()
        {
            throw new NotImplementedException();
        }

        public Composite.Group GetGroup()
        {
            throw new NotImplementedException();
        }
    }
}
