using System;

namespace HW2
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Execute()
        {
            _action();
        }
    }
}
