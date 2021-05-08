namespace PaintPatterns.Command
{
    public interface Actions
    {
        void Execute();
        void Redo();
        void Undo();
    }
}