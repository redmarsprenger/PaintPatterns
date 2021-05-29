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
    class Resize : ICommand
    {
        private UIElement selectedElement;
        private Point getPosition;
        private Point RelativePoint;
        private Point initialPosition;
        private Canvas canvas;
        private Shape shapeDrawing;
        private bool done;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedElement"></param>
        /// <param name="getPosition"></param>
        /// <param name="RelativePoint"></param>
        /// <param name="initialPosition"></param>
        /// <param name="canvas"></param>
        /// <param name="shapeDrawing"></param>
        /// <param name="done"></param>
        public Resize(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, bool done)
        {
            this.selectedElement = selectedElement;
            this.getPosition = getPosition;
            this.RelativePoint = RelativePoint;
            this.initialPosition = initialPosition;
            this.canvas = canvas;
            this.shapeDrawing = shapeDrawing;
            this.done = done;
        }

        /// <summary>
        /// Execute Resize set selectedElement Width and Height
        /// </summary>
        public void Execute()
        {
            //Can't have '-' height or Width so change position if wanting to draw to minus axes.
            if (getPosition.X < RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
            else if (getPosition.X > RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else if (getPosition.X < RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - (int)getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else
            if (getPosition.X > RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
        }

        /// <summary>
        /// Set selectedElement back to newest Width and Height, can use same values as Execute
        /// </summary>
        public void Redo()
        {
            if (selectedElement == null)
            {
                selectedElement = shapeDrawing;
            }
            //Can't have '-' height or Width so change position if wanting to draw to minus axes.
            if (getPosition.X < RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
            else if (getPosition.X > RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else if (getPosition.X < RelativePoint.X && getPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                selectedElement.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - (int)getPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)getPosition.Y);
            }
            else
            if (getPosition.X > RelativePoint.X && getPosition.Y > RelativePoint.Y)
            {
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)getPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)getPosition.Y - (int)RelativePoint.Y);
            }
        }

        /// <summary>
        /// Set element back to previous Width and Height by using initialPosition instead of getPosition
        /// </summary>
        public void Undo()
        {
            //If for some reason no element is selected get the shapeDrawing
            if (selectedElement == null)
            {
                selectedElement = shapeDrawing;
            }

            //Can't have '-' height or Width so change position if wanting to draw to minus axes.
            if (initialPosition.X < RelativePoint.X && initialPosition.Y > RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)initialPosition.X);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - initialPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)initialPosition.Y - (int)RelativePoint.Y);
            }
            else if (initialPosition.X > RelativePoint.X && initialPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.TopProperty, (double)initialPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)initialPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)initialPosition.Y);
            }
            else if (initialPosition.X < RelativePoint.X && initialPosition.Y < RelativePoint.Y)
            {
                selectedElement.SetValue(Canvas.LeftProperty, (double)initialPosition.X);
                selectedElement.SetValue(Canvas.TopProperty, (double)initialPosition.Y);
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)RelativePoint.X - (int)initialPosition.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)RelativePoint.Y - (int)initialPosition.Y);
            }
            else
            if (initialPosition.X > RelativePoint.X && initialPosition.Y > RelativePoint.Y)
            {
                selectedElement.GetType().GetProperty("Width").SetValue(selectedElement, (int)initialPosition.X - (int)RelativePoint.X);
                selectedElement.GetType().GetProperty("Height").SetValue(selectedElement, (int)initialPosition.Y - (int)RelativePoint.Y);
            }
        }

        /// <summary>
        /// return selectedElement
        /// </summary>
        /// <returns>selectedElement</returns>
        public UIElement GetElement()
        {
            return selectedElement;
        }

        /// <summary>
        /// return shapeDrawing
        /// </summary>
        /// <returns>shapeDrawing</returns>
        public Shape GetShape()
        {
            return shapeDrawing;
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
