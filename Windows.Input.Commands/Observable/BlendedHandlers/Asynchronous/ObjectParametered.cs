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
    /// Blended handlers asynchronous observable command with object parameter.
    /// </summary>
    public sealed class AsyncBlendedObservableCommand : ObservableCommandBase
    {
        /// <summary>
        /// Execute <see cref="Func{object, Task}[]"/> delegates.
        /// </summary>
        private readonly Func<object, Task>[] executionCollection;

        /// <summary>
        /// CanExecute <see cref="Func{object, bool}"/> delegate.
        /// </summary>
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="AsyncBlendedObservableCommand"/> object that can always execute.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        public AsyncBlendedObservableCommand(params Func<object, Task>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncBlendedObservableCommand"/> object.
        /// </summary>
        /// <param name="canExecuteExpression">Execution delegates.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public AsyncBlendedObservableCommand(Expression<Func<object, bool>> canExecuteExpression, params Func<object, Task>[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncBlendedObservableCommand"/> can execute in its current state.
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
        /// Executes the <see cref="AsyncBlendedObservableCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public async override void Execute(object parameter)
        {
            var executionTasks = new List<Task>();
            foreach (var execute in executionCollection)
                executionTasks.Add(execute(parameter));
            await Task.WhenAll(executionTasks);
        }
    }

}
