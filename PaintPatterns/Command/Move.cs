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
        private Point _oldPos;
        private bool _moving;
        private Shape _shape;
        private UIElement _Element;


        private UIElement _selectedElement;
        private Point _getPosition;
        private Point _RelativePoint;
        private Point _initialPosition;
        private Canvas _canvas;
        private Shape _shapeDrawing;
        private Point _Diff;
        private bool done;

        private readonly Stack<UIElement> _positionsUndo = new Stack<UIElement>();
        private readonly Stack<UIElement> _positionsRedo = new Stack<UIElement>();

        public Move(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Point Diff, Canvas canvas, Shape shapeDrawing, bool done)
        {
            _selectedElement = selectedElement;
            _getPosition = getPosition;
            _RelativePoint = RelativePoint;
            _initialPosition = initialPosition;
            _canvas = canvas;
            _shapeDrawing = shapeDrawing;
            _Diff = Diff;
            this.done = done;
        }
        public void Execute()
        {
            if (done)
            {
                _oldPos.X = _RelativePoint.X;
                _oldPos.Y = _RelativePoint.Y;
                this._shape = _shapeDrawing;
                this._Element = _selectedElement;
            }

            _selectedElement.SetValue(Canvas.LeftProperty, (double)_getPosition.X - _Diff.X);
            _selectedElement.SetValue(Canvas.TopProperty, (double)_getPosition.Y - _Diff.Y);
        }

        public void Redo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            _selectedElement.SetValue(Canvas.LeftProperty, (double)_getPosition.X - _Diff.X);
            _selectedElement.SetValue(Canvas.TopProperty, (double)_getPosition.Y - _Diff.Y);
        }

        public void Undo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            _selectedElement.SetValue(Canvas.LeftProperty, (double)_oldPos.X);
            _selectedElement.SetValue(Canvas.TopProperty, (double)_oldPos.Y);
        }
    }
}
