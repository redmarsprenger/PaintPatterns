using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintPatterns.Composite;

namespace PaintPatterns.Command
{
    class Load : ICommand
    {
        private readonly CommandInvoker invoker;
        public Load()
        {
            this.invoker = CommandInvoker.GetInstance();
        }

        public void Execute()
        {
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
    }
}
