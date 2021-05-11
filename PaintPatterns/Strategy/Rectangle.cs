using System.Windows.Shapes;

namespace PaintPatterns.Strategy
{
    /// <summary>
    /// Rectangle Strategy
    /// </summary>
    class Rectangle : IStrategy
    {
        /// <summary>
        /// Execute Rectangle, Create new rectangle, set it as shapeDrawing and call CommandInvoker Draw
        /// </summary>
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
                    rectangle,
                    false
                    );
        }
    }
}
