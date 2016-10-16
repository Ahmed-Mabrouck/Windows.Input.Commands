using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Basic.BlendedHandlers.Asynchronous
{

    /// <summary>
    /// Blended handlers asynchronous command with object parameter
    /// </summary>
    public sealed class AsyncBlendedBasicCommand : CommandBase
    {
        /// <summary>
        /// Execute <see cref="Func{object, Task}[]"/> delegates.
        /// </summary>
        private readonly Func<object, Task>[] executionCollection;

        /// <summary>
        /// CanExecute <see cref="Func{object, Task{bool}}"/> delegate.
        /// </summary>
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="AsyncBlendedBasicCommand"/> object that can always execute.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        public AsyncBlendedBasicCommand(params Func<object, Task>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncBlendedBasicCommand"/> object.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        /// <param name="canExecute">Execution status delegate.</param>
        public AsyncBlendedBasicCommand(Func<object, bool> canExecute, params Func<object, Task>[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncBlendedBasicCommand"/> can execute in its current state.
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
        /// Executes the <see cref="AsyncBlendedBasicCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public override void Execute(object parameter)
        {
            var executionTasks = new List<Task>();
            foreach (var execute in executionCollection)
                executionTasks.Add(execute(parameter));
            Task.WhenAll(executionTasks);
        }
    }

}
