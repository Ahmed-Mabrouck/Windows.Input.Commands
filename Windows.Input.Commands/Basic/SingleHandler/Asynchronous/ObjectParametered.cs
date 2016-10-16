using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Basic.SingleHandler.Asynchronous
{

    /// <summary>
    /// Single handler asynchronous command with object parameter
    /// </summary>
    public sealed class AsyncBasicCommand : CommandBase
    {
        /// <summary>
        /// Execute <see cref="Func{object, Task}"/> delegate.
        /// </summary>
        private readonly Func<object, Task> execute;

        /// <summary>
        /// CanExecute <see cref="Func{object, Task{bool}}"/> delegate.
        /// </summary>
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="AsyncBasicCommand"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public AsyncBasicCommand(Func<object, Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncBasicCommand"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecute">Execution status delegate.</param>
        public AsyncBasicCommand(Func<object, Task> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncBasicCommand"/> can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        /// <summary>
        /// Executes the <see cref="AsyncBasicCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public async override void Execute(object parameter)
        {
            await execute(parameter);
        }
    }
}
