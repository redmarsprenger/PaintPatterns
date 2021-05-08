using System;
using System.Collections;
using System.Collections.Generic;
using PaintPatterns.Command;

namespace PaintPatterns
{
    class CommandInvoker
    {
        private static readonly CommandInvoker Instance = new CommandInvoker();
        private readonly Stack<Command.Actions> ActionsUndo = new Stack<Command.Actions>();
        private readonly Stack<Command.Actions> ActionsRedo = new Stack<Command.Actions>();
        public MainWindow MainWindow;

        public void Resize()
        {
            var cmd = new Resize();
            cmd.Execute();
            ActionsUndo.Push(cmd);
        }

        public void Move()
        {
            var cmd = new Move();
            cmd.Execute();
            ActionsUndo.Push(cmd);
        }

        public void Draw()
        {
            var cmd = new Draw();
            cmd.Execute();
            ActionsUndo.Push(cmd);
        }

        public void Undo()
        {
            var Action = ActionsRedo.Peek();
            Action.Undo();
            ActionsRedo.Push(Action);
        }

        public void Redo()
        {
            var Action = ActionsRedo.Peek();
            Action.Redo();
            ActionsUndo.Push(Action);
        }
        public static CommandInvoker GetInstance()
        {
            return Instance;
        }
    }
}
