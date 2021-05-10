﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    class Move : ICommand
    {
        public void Execute(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X - RelativePoint.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y - RelativePoint.Y);
        }

        public void Redo()
        {

        }

        public void Undo()
        {

        }
    }
}
