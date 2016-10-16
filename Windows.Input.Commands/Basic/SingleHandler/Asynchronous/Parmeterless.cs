using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Basic.SingleHandler.Asynchronous
{

    /// <summary>
    /// Single handler asynchronous parameterless command.
    /// </summary>
    public sealed class AsyncParameterlessBasicCommand : CommandBase
    {
        /// <summary>
        /// Execute <see cref="Action{Task}"/> delegate.
        /// </summary>
        private readonly Func<Task> execute;

        /// <summary>
        /// CanExecute <see cref="Func{bool}"/> delegate.
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="AsyncParameterlessBasicCommand"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public AsyncParameterlessBasicCommand(Func<Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncParameterlessBasicCommand"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecute">Execution status delegate.</param>
        public AsyncParameterlessBasicCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncParameterlessBasicCommand"/> can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        /// <summary>
        /// Executes the <see cref="AsyncParameterlessBasicCommand"/> on the current command target.
        /// </summary>
        public async override void Execute(object parameter)
        {
            await execute();
        }
    }
}
