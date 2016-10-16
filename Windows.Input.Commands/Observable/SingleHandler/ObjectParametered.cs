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
    /// Single handler observable command with object parameter.
    /// </summary>
    public sealed class ObservableCommand : ObservableCommandBase
    {
        /// <summary>
        /// Execute <see cref="Action{object}"/> delegate.
        /// </summary>
        private readonly Action<object> execute;

        /// <summary>
        /// CanExecute <see cref="Func{object, bool}"/> delegate.
        /// </summary>
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="ObservableCommand"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public ObservableCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ObservableCommand"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public ObservableCommand(Action<object> execute, Expression<Func<object, bool>> canExecuteExpression)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="ObservableCommand"/> can execute in its current state.
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
        /// Executes the <see cref="ObservableCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public override void Execute(object parameter)
        {
            execute(parameter);
        }
    }

}
