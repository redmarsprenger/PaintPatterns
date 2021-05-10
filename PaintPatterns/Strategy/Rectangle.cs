using System.Windows.Shapes;

namespace PaintPatterns.StrategyPattern
{
    class Rectangle : IStrategy
    {
        public void Execute()
        {
            System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();
            CommandInvoker.GetInstance().MainWindow.shapeDrawing = rectangle;
            CommandInvoker.GetInstance()
                .Draw(
                    CommandInvoker.GetInstance().MainWindow.selectedElement,
                    CommandInvoker.GetInstance().MainWindow.MousePos,
                    CommandInvoker.GetInstance().MainWindow.InitialPosition,
                    CommandInvoker.GetInstance().MainWindow.Canvas,
                    CommandInvoker.GetInstance().MainWindow.shapeDrawing,
                    rectangle
                    );
        }
    }
}
