using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamAR.Core
{
    public abstract class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Invokes provided delegate, then raises PropertyChanged event.
        /// </summary>
        protected void OnPropertyChanged(Action action, [CallerMemberName] string name = null)
        {
            action();
            OnPropertyChanged(name);
        }

        /// <summary>
        /// Assigns value to the parameter, then raises PropertyChanged event.
        /// </summary>
        protected void OnPropertyChanged<T>(ref T parameter, T value, [CallerMemberName] string name = null)
        {
            parameter = value;
            OnPropertyChanged(name);
        }
    }
}
