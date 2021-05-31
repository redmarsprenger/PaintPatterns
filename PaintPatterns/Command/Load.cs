using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PaintPatterns.Composite;

namespace PaintPatterns.Command
{
    class Load : ICommand
    {
        private readonly CommandInvoker invoker;
        private Group glGroup;
        public Load()
        {
            this.invoker = CommandInvoker.GetInstance();
        }

        public void Execute()
        {
            //Clear canvas and composite
            invoker.MainWindow.Canvas.Children.Clear();
            invoker.composite = new CompositeShapes();

            string path = System.IO.Directory.GetCurrentDirectory() + "help.txt";
            string[] lines = File.ReadAllLines(path);
            List<Brush> colors = RandomColors(lines.Length);
            Stack<Shape> groupStack = new Stack<Shape>();
            int lineNumber = 0;

            //Loop trough all lines
            foreach (string line in lines)
            {
                //Split to get all items from line in array
                string[] split = line.Split(' ');
                int depth = 0;
                for (int i = 0; i < split.Length; i++)
                {
                    string c = split[i];
                    if (c != "")
                        break;
                    //4 spaces add depth
                    if (i % 4 == 0)
                        depth++;
                }

                //Check what shape or group, set values to shape and add it to canvas and composite
                switch (split[depth * 4])
                {
                    case "rectangle":
                        {
                            Rectangle rectangle = new Rectangle();

                            rectangle.Stroke = Brushes.Black;
                            rectangle.StrokeThickness = 2;
                            rectangle.Fill = colors[lineNumber];
                            rectangle.Uid = Guid.NewGuid().ToString("N");

                            rectangle.SetValue(Canvas.LeftProperty, (double)Convert.ToInt32(split[1 + depth * 4]));
                            rectangle.SetValue(Canvas.TopProperty, (double)Convert.ToInt32(split[2 + depth * 4]));
                            rectangle.Width = (double)Convert.ToInt32(split[3 + depth * 4]);
                            rectangle.Height = (double)Convert.ToInt32(split[4 + depth * 4]);

                            invoker.MainWindow.Canvas.Children.Add(rectangle);
                            invoker.composite.Add(rectangle);
                            groupStack.Push(rectangle);
                            break;
                        }
                    case "ellipse":
                        {
                            Ellipse ellipse = new Ellipse();

                            ellipse.Stroke = Brushes.Black;
                            ellipse.StrokeThickness = 2;
                            ellipse.Fill = (Brush)typeof(Brushes).GetProperties()[new Random().Next(typeof(Brushes).GetProperties().Length)].GetValue(null, null);
                            ellipse.Uid = Guid.NewGuid().ToString("N");

                            ellipse.SetValue(Canvas.LeftProperty, (double)Convert.ToInt32(split[1 + depth * 4]));
                            ellipse.SetValue(Canvas.TopProperty, (double)Convert.ToInt32(split[2 + depth * 4]));
                            ellipse.Width = (double)Convert.ToInt32(split[3 + depth * 4]);
                            ellipse.Height = (double)Convert.ToInt32(split[4 + depth * 4]);

                            invoker.MainWindow.Canvas.Children.Add(ellipse);
                            invoker.composite.Add(ellipse);
                            groupStack.Push(ellipse);
                            break;
                        }
                    case "group":
                        {
                            if (groupStack.Count != 0)
                            {
                                
                                invoker.Group(groupStack);
                                groupStack.Clear();
                            }
                            break;
                        }
                    default:
                        continue;
                }
                lineNumber++;
            }
            if (groupStack.Count != 0)
            {
                invoker.Group(groupStack);
                groupStack.Clear();
            }
        }

        private List<Brush> RandomColors(int amount)
        {
            List<Brush> colors = new List<Brush>();
            Random rnd = new Random();

            for (int i = 0; i < amount; i++)
            {
                int random = rnd.Next(typeof(Brushes).GetProperties().Length);
                var result = (Brush)typeof(Brushes).GetProperties()[random].GetValue(null, null);

                colors.Add(result);
            }

            return colors;
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public UIElement GetElement()
        {
            return null;
        }

        public Shape GetShape()
        {
            return null;
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
