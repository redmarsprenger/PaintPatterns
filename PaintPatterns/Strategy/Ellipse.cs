namespace PaintPatterns.Strategy
{
    /// <summary>
    /// Ellipse Strategy
    /// </summary>
    class Ellipse : IStrategy
    {
        /// <summary>
        /// Execute Ellipse, Create new ellipse, set it as shapeDrawing and call CommandInvoker Draw
        /// </summary>
        public void Execute()
        {
            System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse();
            CommandInvoker.GetInstance().MainWindow.shapeDrawing = ellipse;
            CommandInvoker.GetInstance()
                .Draw(
                    CommandInvoker.GetInstance().MainWindow.selectedElement,
                    CommandInvoker.GetInstance().MainWindow.MousePos,
                    CommandInvoker.GetInstance().MainWindow.InitialPosition,
                    CommandInvoker.GetInstance().MainWindow.Canvas,
                    CommandInvoker.GetInstance().MainWindow.shapeDrawing,
                    ellipse,
                    false
                    );
        }
    }
}
