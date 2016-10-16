using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Basic.SingleHandler.Asynchronous
{

    /// <summary>
    /// Single handler asynchronous command with generic parameter
    /// </summary>
    /// <typeparam name="T">Command parameter type.</typeparam>
    public sealed class AsyncBasicCommand<T> : CommandBase
    {
        /// <summary>
        /// Execute <see cref="Func{T, Task}"/> delegate.
        /// </summary>
        private readonly Func<T, Task> execute;

        /// <summary>
        /// CanExecute <see cref="Func{T, bool}"/> delegate.
        /// </summary>
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="AsyncBasicCommand{T}"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public AsyncBasicCommand(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncBasicCommand{T}"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecute">Execution status delegate.</param>
        public AsyncBasicCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncBasicCommand{T}"/> can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute((T)parameter);
        }

        /// <summary>
        /// Executes the <see cref="AsyncBasicCommand{T}"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public async override void Execute(object parameter)
        {
            await execute((T)parameter);
        }
    }
}
