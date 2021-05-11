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
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedElement"></param>
        /// <param name="getPosition"></param>
        /// <param name="RelativePoint"></param>
        /// <param name="initialPosition"></param>
        /// <param name="Diff"></param>
        /// <param name="canvas"></param>
        /// <param name="shapeDrawing"></param>
        /// <param name="done"></param>
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

        /// <summary>
        /// Set new position for selectedElement
        /// </summary>
        public void Execute()
        {
            selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X - Diff.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y - Diff.Y);
        }

        /// <summary>
        /// Set selectedElement back to newest positions, can use same values as Execute
        /// </summary>
        public void Redo()
        {
            selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X - Diff.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y - Diff.Y);
        }

        /// <summary>
        /// Set element back to previous point stored in RelativePoint
        /// </summary>
        public void Undo()
        {
            selectedElement.SetValue(Canvas.LeftProperty, (double)RelativePoint.X);
            selectedElement.SetValue(Canvas.TopProperty, (double)RelativePoint.Y);
        }

        /// <summary>
        /// return selectedElement
        /// </summary>
        /// <returns></returns>
        public UIElement GetElement()
        {
            return selectedElement;
        }

        /// <summary>
        /// return shapeDrawing
        /// </summary>
        /// <returns></returns>
        public Shape GetShape()
        {
            return shapeDrawing;
        }
    }
}
