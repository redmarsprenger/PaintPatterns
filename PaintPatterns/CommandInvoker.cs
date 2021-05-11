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
    /// <summary>
    /// CommandInvoker class
    /// </summary>
    class CommandInvoker
    {
        private static readonly CommandInvoker Instance = new CommandInvoker();
        private readonly Stack<Command.ICommand> ActionsUndo = new Stack<Command.ICommand>();
        private readonly Stack<Command.ICommand> ActionsRedo = new Stack<Command.ICommand>();
        public MainWindow MainWindow;

        /// <summary>
        /// Executes Resize command. If done true push to ActionsUndo.
        /// </summary>
        /// <param name="selectedElement"></param>
        /// <param name="getPosition"></param>
        /// <param name="RelativePoint"></param>
        /// <param name="initialPosition"></param>
        /// <param name="canvas"></param>
        /// <param name="shapeDrawing"></param>
        /// <param name="done"></param>
        /// <param name="composite"></param>
        public void Resize(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, bool done, CompositeShapes composite)
        {
            var cmd = new Resize(selectedElement, getPosition, RelativePoint, initialPosition, canvas, shapeDrawing, done, composite);
            cmd.Execute();
            if (done)
            {
                ActionsUndo.Push(cmd);
                ActionsRedo.Clear();
                //updates the Composite cloned element
                composite.Update(selectedElement);
            }
        }

        /// <summary>
        /// Executes Move command. If done true push to ActionsUndo.
        /// </summary>
        /// <param name="selectedElement"></param>
        /// <param name="getPosition"></param>
        /// <param name="RelativePoint"></param>
        /// <param name="initialPosition"></param>
        /// <param name="Diff"></param>
        /// <param name="canvas"></param>
        /// <param name="shapeDrawing"></param>
        /// <param name="done"></param>
        /// <param name="composite"></param>
        public void Move(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Point Diff, Canvas canvas, Shape shapeDrawing, bool done, CompositeShapes composite)
        {
            var cmd = new Move(selectedElement, getPosition, RelativePoint, initialPosition, Diff, canvas, shapeDrawing, done);
            cmd.Execute();
            if (done)
            {
                ActionsUndo.Push(cmd);
                ActionsRedo.Clear();
                //updates the Composite cloned element
                composite.Update(selectedElement);
            }
        }

        /// <summary>
        /// Executes Draw command. If done true push to ActionsUndo.
        /// </summary>
        /// <param name="selectedElement"></param>
        /// <param name="getPosition"></param>
        /// <param name="initialPosition"></param>
        /// <param name="canvas"></param>
        /// <param name="shapeDrawing"></param>
        /// <param name="shape"></param>
        /// <param name="done"></param>
        public void Draw(UIElement selectedElement, Point getPosition, Point initialPosition, Canvas canvas, Shape shapeDrawing, Shape shape, bool done)
        {
            var cmd = new Draw(selectedElement, getPosition, initialPosition, canvas, shapeDrawing, shape);
            cmd.Execute();
            if (done)
            {
                ActionsUndo.Push(cmd);
                ActionsRedo.Clear();
            }
        }

        /// <summary>
        /// Undo, pops from ActionsUndo calls Undo command.
        /// </summary>
        /// <param name="selectedElement"></param>
        /// <param name="getPosition"></param>
        /// <param name="RelativePoint"></param>
        /// <param name="initialPosition"></param>
        /// <param name="canvas"></param>
        /// <param name="shapeDrawing"></param>
        /// <param name="composite"></param>
        public void Undo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, CompositeShapes composite)
        {
            //Check if ActionsUndo isn't empty
            if (ActionsUndo.Count != 0)
            {
                ICommand command = ActionsUndo.Pop();
                command.Undo();
                ActionsRedo.Push(command);

                //Element is null if undo is Draw.
                if (command.GetElement() != null)
                {
                    string uid = command.GetElement().Uid;
                    bool entry = false;

                    //go through actions to see if there are other entries or if it is the last one
                    foreach (var item in ActionsUndo)
                    {
                        if (item.GetShape().Uid == uid)
                        {
                            //updates the Composite cloned element
                            composite.Update(command.GetShape());
                            entry = true;
                        }
                    }
                    if (entry == false)//if no entry is left remove it
                    {
                        //removes the Composite cloned element
                        composite.Remove(uid);
                    }
                }
            }
            
        }

        /// <summary>
        /// Redo, pops from ActionsRedo calls Redo command.
        /// </summary>
        /// <param name="selectedElement"></param>
        /// <param name="getPosition"></param>
        /// <param name="RelativePoint"></param>
        /// <param name="initialPosition"></param>
        /// <param name="canvas"></param>
        /// <param name="shapeDrawing"></param>
        /// <param name="composite"></param>
        public void Redo(UIElement selectedElement, Point getPosition, Point RelativePoint, Point initialPosition, Canvas canvas, Shape shapeDrawing, CompositeShapes composite)
        {
            //Check if ActionsRedo isn't empty
            if (ActionsRedo.Count != 0)
            {
                ICommand command = ActionsRedo.Pop();
                command.Redo();
                ActionsUndo.Push(command);
                //updates the Composite cloned element
                composite.Update(selectedElement);
            }
        }

        /// <summary>
        /// Returns CommandInvoker Instance
        /// </summary>
        /// <returns>Instance</returns>
        public static CommandInvoker GetInstance()
        {
            return Instance;
        }
    }
}
