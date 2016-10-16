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
    /// Blended handlers asynchronous observable parameterless command.
    /// </summary>
    public sealed class AsyncParameterlessBlendedObservableCommand : ObservableCommandBase
    {
        /// <summary>
        /// Execute <see cref="Action{Task}[]"/> delegates.
        /// </summary>
        private readonly Func<Task>[] executionCollection;

        /// <summary>
        /// CanExecute <see cref="Func{bool}"/> delegate.
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="AsyncParameterlessBlendedObservableCommand"/> object that can always execute.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        public AsyncParameterlessBlendedObservableCommand(params Func<Task>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncParameterlessBlendedObservableCommand"/> object.
        /// </summary>
        /// <param name="canExecuteExpression">Execution delegates.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public AsyncParameterlessBlendedObservableCommand(Expression<Func<bool>> canExecuteExpression, params Func<Task>[] executionCollection)
        {
            if (this.executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncParameterlessBlendedObservableCommand"/> can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        /// <summary>
        /// Executes the <see cref="AsyncParameterlessBlendedObservableCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public async override void Execute(object parameter)
        {
            var executionTasks = new List<Task>();
            foreach (var execute in executionCollection)
                executionTasks.Add(execute());
            await Task.WhenAll(executionTasks);
        }
    }

}
