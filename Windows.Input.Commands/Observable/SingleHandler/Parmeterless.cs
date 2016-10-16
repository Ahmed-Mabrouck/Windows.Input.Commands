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
    /// Single handler observable parameterless command.
    /// </summary>
    public sealed class ParameterlessObservableCommand : ObservableCommandBase
    {
        /// <summary>
        /// Execute <see cref="Action"/> delegate.
        /// </summary>
        private readonly Action execute;

        /// <summary>
        /// CanExecute <see cref="Func{bool}"/> delegate.
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="ParameterlessObservableCommand"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public ParameterlessObservableCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ParameterlessObservableCommand"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public ParameterlessObservableCommand(Action execute, Expression<Func<bool>> canExecuteExpression)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="ParameterlessObservableCommand"/> can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        /// <summary>
        /// Executes the <see cref="ParameterlessObservableCommand"/> on the current command target.
        /// </summary>
        public override void Execute(object parameter)
        {
            execute();
        }
    }
}
