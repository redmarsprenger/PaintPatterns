using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PaintPatterns.Command;
using PaintPatterns.Composite;

namespace PaintPatterns
{
    class CommandInvoker
    {
        private static readonly CommandInvoker Instance = new CommandInvoker();
        private readonly Stack<Command.ICommand> ActionsUndo = new Stack<Command.ICommand>();
        private readonly Stack<Command.ICommand> ActionsRedo = new Stack<Command.ICommand>();
        public MainWindow MainWindow;

        public void Resize(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, bool done, CompositeShapes composite)
        {
            var cmd = new Resize(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing, done, composite);
            cmd.Execute();
            if (done)
            {
                ActionsUndo.Push(cmd);
                ActionsRedo.Clear();
                composite.Update(selectedElement);
            }
        }

        public void Move(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Point Diff, Canvas canvas, Shape shapeDrawing, bool done, CompositeShapes composite)
        {
            var cmd = new Move(selectedElement, getPosition, RelativePoint, initialPosition, Diff, canvas, shapeDrawing, done);
            cmd.Execute();
            if (done)
            {
                ActionsUndo.Push(cmd);
                ActionsRedo.Clear();
                composite.Update(selectedElement);
            }
        }

        public void Draw(UIElement selectedElement, Point getPosition, Point initialPosition, Canvas canvas, Shape shapeDrawing, Shape shape)
        {
            var cmd = new Draw(selectedElement, getPosition, initialPosition, canvas, shapeDrawing, shape);
            cmd.Execute();
        }
        
        public void Undo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, CompositeShapes composite)
        { 
            if (ActionsUndo.Count != 0)
            {
                ICommand command = ActionsUndo.Pop();
                command.Undo();
                ActionsRedo.Push(command);

                string uid = command.GetElement().Uid;
                bool entry = false;

                foreach (var item in ActionsUndo)
                {
                    if (item.GetShape().Uid == uid)
                    {
                        composite.Update(command.GetShape());
                        entry = true;
                    }
                }
                if (entry == false)
                {
                    composite.Remove(uid);
                }
            }
            
        }
        
        public void Redo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, CompositeShapes composite)
        {
            if (ActionsRedo.Count != 0)
            {
                ICommand command = ActionsRedo.Pop();
                command.Redo();
                ActionsUndo.Push(command);
                composite.Update(selectedElement);
            }
        }
        public static CommandInvoker GetInstance()
        {
            return Instance;
        }
    }

    internal class Drawww
    {
        public Drawww(int round, int i, Shape shape)
        {
            throw new NotImplementedException();
        }
    }
}
