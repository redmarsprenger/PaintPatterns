using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintPatterns.Command
{
    /// <summary>
    /// 
    /// </summary>
    class Draw : ICommand
    {
        private readonly UIElement selectedElement;
        private Point getPosition;
        private Point initialPosition;
        private Canvas canvas;
        private Shape shapeDrawing;
        private Shape newShape;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedElement"></param>
        /// <param name="getPosition"></param>
        /// <param name="initialPosition"></param>
        /// <param name="canvas"></param>
        /// <param name="shapeDrawing"></param>
        /// <param name="shape"></param>
        public Draw(UIElement selectedElement, Point getPosition, Point initialPosition, Canvas canvas, Shape shapeDrawing, Shape shape)
        {
            this.selectedElement = selectedElement;
            this.getPosition = getPosition;
            this.initialPosition = initialPosition;
            this.canvas = canvas;
            this.shapeDrawing = shapeDrawing;
            this.newShape = shape;
        }

        /// <summary>
        /// Create new shape and set proper fields
        /// </summary>
        public void Execute()
        {
            // if newShape != null set the base values for a new shape
            if (newShape != null)
            {
                newShape.Width = 20;
                newShape.Height = 20;
                newShape.Stroke = Brushes.Black;
                newShape.StrokeThickness = 2;
                //Random color
                newShape.Fill = (Brush)typeof(Brushes).GetProperties()[new Random().Next(typeof(Brushes).GetProperties().Length)].GetValue(null, null);

                //add identifier to shape
                newShape.Uid = Guid.NewGuid().ToString("N");

                canvas.Children.Add(newShape);
                newShape.SetValue(Canvas.LeftProperty, initialPosition.X);
                newShape.SetValue(Canvas.TopProperty, initialPosition.Y);

                shapeDrawing = newShape;
            }

            //Can't have '-' height or Width so change position if wanting to draw to minus axes.
            if (getPosition.X < initialPosition.X && getPosition.Y > initialPosition.Y)
            {
                shapeDrawing.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                shapeDrawing.Width = (int)initialPosition.X - (int)getPosition.X;
                shapeDrawing.Width = (int)initialPosition.X - (int)getPosition.X;
                shapeDrawing.Height = (int)getPosition.Y - (int)initialPosition.Y;
            }
            else if (getPosition.X > initialPosition.X && getPosition.Y < initialPosition.Y)
            {
                shapeDrawing.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                shapeDrawing.Width = (int)getPosition.X - (int)initialPosition.X;
                shapeDrawing.Height = (int)initialPosition.Y - (int)getPosition.Y;
            }
            else if (getPosition.X < initialPosition.X && getPosition.Y < initialPosition.Y)
            {
                shapeDrawing.SetValue(Canvas.LeftProperty, (double)getPosition.X);
                shapeDrawing.SetValue(Canvas.TopProperty, (double)getPosition.Y);
                shapeDrawing.Width = (int)initialPosition.X - (int)getPosition.X;
                shapeDrawing.Height = (int)initialPosition.Y - (int)getPosition.Y;
            }
            else if (getPosition.X > initialPosition.X && getPosition.Y > initialPosition.Y)
            {
                shapeDrawing.Width = (int)getPosition.X - (int)initialPosition.X;
                shapeDrawing.Height = (int)getPosition.Y - (int)initialPosition.Y;
            }
        }

        /// <summary>
        /// Add shapeDrawing back into canvas
        /// </summary>
        public void Redo()
        {
            canvas.Children.Add(shapeDrawing);
        }

        /// <summary>
        /// Remove shapeDrawing from canvas
        /// </summary>
        public void Undo()
        {
            canvas.Children.Remove(shapeDrawing);
        }

        /// <summary>
        /// return selectedElement
        /// </summary>
        /// <returns>selectedElement</returns>
        public UIElement GetElement()
        {
            return shapeDrawing;
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
