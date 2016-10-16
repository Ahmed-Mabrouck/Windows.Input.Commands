using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Observable.BlendedHandlers.Asynchronous
{

    /// <summary>
    /// Blended handlers asynchronous observable command with generic parameter.
    /// </summary>
    /// <typeparam name="T">Command parameter type.</typeparam>
    public sealed class AsyncBlendedObservableCommand<T> : ObservableCommandBase
    {
        /// <summary>
        /// Execute <see cref="Func{T, Task}[]"/> delegates.
        /// </summary>
        private readonly Func<T, Task>[] executionCollection;

        /// <summary>
        /// CanExecute <see cref="Func{T, bool}"/> delegate.
        /// </summary>
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="AsyncBlendedObservableCommand{T}"/> object that can always execute.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        public AsyncBlendedObservableCommand(params Func<T, Task>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncBlendedObservableCommand{T}"/> object.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public AsyncBlendedObservableCommand(Expression<Func<T, bool>> canExecuteExpression, params Func<T, Task>[] executionCollection)
        {
            if (this.executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncBlendedObservableCommand{T}"/> can execute in its current state.
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
        /// Executes the <see cref="AsyncBlendedObservableCommand{T}"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public async override void Execute(object parameter)
        {
            var executionTasks = new List<Task>();
            foreach (var execute in executionCollection)
                executionTasks.Add(execute((T)parameter));
            await Task.WhenAll(executionTasks);
        }
    }

}
