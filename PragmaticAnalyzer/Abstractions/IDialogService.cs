namespace PragmaticAnalyzer.Abstractions
{
    public interface IDialogService
    {
        string? OpenFileDialog(string filter);
        string? SaveFileDialog(string defaultFileName, string filter);
    }
}
