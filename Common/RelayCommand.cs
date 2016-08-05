using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication.Common
{
    class RelayCommand : ICommand
    {
        private Action<Object> execute;
        private Predicate<Object> canExecute;

        public RelayCommand(Action<Object> execute, Predicate<Object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<Object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (this.canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(Object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public void Execute(Object parameter)
        {
            this.execute(parameter);
        }

        public void Destroy()
        {
            this.execute = param => { return; };
            this.canExecute = param => false;
        }

        private static bool DefaultCanExecute(Object parameter)
        {
            return true;
        }
    }
}
