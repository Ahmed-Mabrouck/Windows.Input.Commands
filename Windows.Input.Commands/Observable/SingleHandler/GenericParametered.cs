using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Observable.SingleHandler
{

    /// <summary>
    /// Single handler observable command with generic parameter.
    /// </summary>
    /// <typeparam name="T">Command parameter type.</typeparam>
    public sealed class ObservableCommand<T> : ObservableCommandBase
    {
        /// <summary>
        /// Execute <see cref="Action{T}"/> delegate.
        /// </summary>
        private readonly Action<T> execute;

        /// <summary>
        /// CanExecute <see cref="Func{T, bool}"/> delegate.
        /// </summary>
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="ObservableCommand{T}"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public ObservableCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ObservableCommand{T}"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public ObservableCommand(Action<T> execute, Expression<Func<T, bool>> canExecuteExpression)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="ObservableCommand{T}"/> can execute in its current state.
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
        /// Executes the <see cref="ObservableCommand{T}"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public override void Execute(object parameter)
        {
            execute((T)parameter);
        }
    }
}
