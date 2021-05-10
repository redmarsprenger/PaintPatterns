using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PaintPatterns.Command;

namespace PaintPatterns
{
    class CommandInvoker
    {
        private static readonly CommandInvoker Instance = new CommandInvoker();
        private readonly Stack<Command.ICommand> ActionsUndo = new Stack<Command.ICommand>();
        private readonly Stack<Command.ICommand> ActionsRedo = new Stack<Command.ICommand>();
        public MainWindow MainWindow;
        private Point Diff;

        public void Resize(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            var cmd = new Resize();
            cmd.Execute(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing);
            ActionsUndo.Push(cmd);
        }

        public void Move(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            var cmd = new Move();
            cmd.Execute(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing);
            ActionsUndo.Push(cmd);
        }

        public void Draw(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            var cmd = new Draw();
            cmd.Execute(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing);
            ActionsUndo.Push(cmd);
        }

        public void Select(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            var cmd = new Select();
            cmd.Execute(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing);
            ActionsUndo.Push(cmd);
        }

        public void Unselect(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            var cmd = new Unselect();
            cmd.Execute(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing);
            ActionsUndo.Push(cmd);
        }
        
        //        public void Undo()
        //        {
        //            var Action = ActionsRedo.Peek();
        //            Action.Undo();
        //            ActionsRedo.Push(Action);
        //        }
        //
        //        public void Redo()
        //        {
        //            var Action = ActionsRedo.Peek();
        //            Action.Redo();
        //            ActionsUndo.Push(Action);
        //        }
        //        public static CommandInvoker GetInstance()
        //        {
        //            return Instance;
        //        }
    }
}
