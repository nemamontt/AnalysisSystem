using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.Core
{
    public class ObservedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
