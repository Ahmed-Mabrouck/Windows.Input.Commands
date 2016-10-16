using System;
using System.Windows.Input;

namespace Windows.Input.Commands.Base
{
    /// <summary>
    /// Abstract implementation of ICommand interface.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// Call this method to raise CanExecuteChanged event so that binding clients update 
        /// its IsEnabled property value to the current return value of CanExecute method.
        /// [Explicit Call]
        /// </summary>
        protected virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
