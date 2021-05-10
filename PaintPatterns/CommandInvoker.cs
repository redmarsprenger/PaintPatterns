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
        private Point InitPos;

        public void Resize(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, bool done)
        {
            var cmd = new Resize(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing, done);
            cmd.Execute();
            if (done)
            {
                ActionsUndo.Push(cmd);
                ActionsRedo.Clear();
            }
        }

        public void Move(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Point Diff, Canvas canvas, Shape shapeDrawing, bool first, bool done)
        {
            var cmd = new Move(selectedElement, getPosition, RelativePoint, InitPos, Diff, canvas, shapeDrawing, done);
            cmd.Execute();
            if (done)
            {
                ActionsUndo.Push(cmd);
                ActionsRedo.Clear();
            }
        }

        public void Draw(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, bool done)
        {
            var cmd = new Draw(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing, done);
            cmd.Execute();
            if (done)
            {
                ActionsUndo.Push(cmd);
                ActionsRedo.Clear();
            }
        }
        
        public void Undo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            if (ActionsUndo.Count != 0)
            {
                ICommand command = ActionsUndo.Pop();
                command.Undo();
                ActionsRedo.Push(command);
            }
        }
        
        public void Redo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing)
        {
            if (ActionsRedo.Count != 0)
            {
                ICommand command = ActionsRedo.Pop();
                command.Redo();
                ActionsUndo.Push(command);
            }
        }
    }
}
