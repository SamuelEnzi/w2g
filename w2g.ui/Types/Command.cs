using System;
using System.Windows.Input;

namespace w2g.ui.Types
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Func<bool> canExecuteMethod { get; set; }
        private Action targetMethod { get; set; }
        private bool lastCommandState = true;

        public Command(Action method, Func<bool> canExecute)
        {
            this.targetMethod = method;
            this.canExecuteMethod = canExecute;
            this.lastCommandState = canExecute();
        }

        public bool CanExecute(object parameter) =>
            canExecuteMethod();

        public void Execute(object parameter)
        {
            targetMethod?.Invoke();
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            var state = canExecuteMethod();
            if (state != lastCommandState)
            {
                lastCommandState = state;
                CanExecuteChanged?.Invoke(this, null);
            }
        }
    }
}
