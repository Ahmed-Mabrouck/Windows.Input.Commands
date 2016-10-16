using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Observable.BlendedHandlers
{

    /// <summary>
    /// Blended handlers observable command with generic parameter.
    /// </summary>
    /// <typeparam name="T">Command parameter type.</typeparam>
    public sealed class BlendedObservableCommand<T> : ObservableCommandBase
    {
        /// <summary>
        /// Execute <see cref="Action{T}[]"/> delegates.
        /// </summary>
        private readonly Action<T>[] executionCollection;

        /// <summary>
        /// CanExecute <see cref="Func{T, bool}"/> delegate.
        /// </summary>
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="BlendedObservableCommand{T}"/> object that can always execute.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        public BlendedObservableCommand(params Action<T>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new <see cref="BlendedObservableCommand{T}"/> object.
        /// </summary>
        /// <param name="canExecuteExpression">Execution delegates.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public BlendedObservableCommand(Expression<Func<T, bool>> canExecuteExpression, params Action<T>[] executionCollection)
        {
            if (this.executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="BlendedObservableCommand{T}"/> can execute in its current state.
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
        /// Executes the <see cref="BlendedObservableCommand{T}"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public override void Execute(object parameter)
        {
            foreach (var execute in executionCollection)
                execute((T)parameter);
        }
    }
}
