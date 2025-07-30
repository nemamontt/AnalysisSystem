using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PragmaticAnalyzer.Assistant
{
    public class ObservedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}