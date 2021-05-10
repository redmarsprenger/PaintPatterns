using System;
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
        private Point oldPos;
        private bool moving;
        private Shape shape;
        private UIElement element;
        private UIElement selectedElement;
        private Point getPosition;
        private Point RelativePoint;
        private Point initialPosition;
        private Canvas canvas;
        private Shape shapeDrawing;
        private Point Diff;
        private bool done;

        private readonly Stack<UIElement> _positionsUndo = new Stack<UIElement>();
        private readonly Stack<UIElement> _positionsRedo = new Stack<UIElement>();

        public Move(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Point Diff, Canvas canvas, Shape shapeDrawing, bool done)
        {
            this.selectedElement = selectedElement;
            this.getPosition = getPosition;
            this.RelativePoint = RelativePoint;
            this.initialPosition = initialPosition;
            this.canvas = canvas;
            this.Diff = Diff;
            this.shapeDrawing = shapeDrawing;
            this.done = done;
        }
        public void Execute()
        {
            if (done)
            {
                oldPos.X = RelativePoint.X;
                oldPos.Y = RelativePoint.Y;
                this.shape = shapeDrawing;
                this.element = selectedElement;
            }

            selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X - Diff.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y - Diff.Y);
        }

        public void Redo()
        {
            selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X - Diff.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y - Diff.Y);
        }

        public void Undo()
        {
            selectedElement.SetValue(Canvas.LeftProperty, (double)oldPos.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)oldPos.Y);
        }
    }
}
