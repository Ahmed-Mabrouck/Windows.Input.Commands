using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Observable.SingleHandler.Asynchronous
{

    /// <summary>
    /// Single handler asynchronous observable parameterless command.
    /// </summary>
    public sealed class AsyncParameterlessObservableCommand : ObservableCommandBase
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
        /// Creates a new <see cref="AsyncParameterlessObservableCommand"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public AsyncParameterlessObservableCommand(Func<Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncParameterlessObservableCommand"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public AsyncParameterlessObservableCommand(Func<Task> execute, Expression<Func<bool>> canExecuteExpression)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncParameterlessObservableCommand"/> can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        /// <summary>
        /// Executes the <see cref="AsyncParameterlessObservableCommand"/> on the current command target.
        /// </summary>
        public async override void Execute(object parameter)
        {
            await execute();
        }
    }

}
