using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Basic.BlendedHandlers.Asynchronous
{

    /// <summary>
    /// Blended handlers asynchronous command with generic parameter
    /// </summary>
    /// <typeparam name="T">Command parameter type.</typeparam>
    public sealed class AsyncBlendedBasicCommand<T> : CommandBase
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
        /// Creates a new <see cref="AsyncBlendedBasicCommand{T}"/> object that can always execute.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        public AsyncBlendedBasicCommand(params Func<T, Task>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncBlendedBasicCommand{T}"/> object.
        /// </summary>
        /// <param name="executionCollection">Execution delegate.</param>
        /// <param name="canExecute">Execution status delegate..</param>
        public AsyncBlendedBasicCommand(Func<T, bool> canExecute, params Func<T, Task>[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncBlendedBasicCommand{T}"/> can execute in its current state.
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
        /// Executes the <see cref="AsyncBlendedBasicCommand{T}"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public override void Execute(object parameter)
        {
            var executionTasks = new List<Task>();
            foreach (var execute in executionCollection)
                executionTasks.Add(execute((T)parameter));
            Task.WhenAll(executionTasks);
        }
    }
}
