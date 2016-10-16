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
    /// Blended handlers observable parameterless command.
    /// </summary>
    public sealed class ParameterlessBlendedObservableCommand : ObservableCommandBase
    {
        /// <summary>
        /// Execute <see cref="Action[]"/> delegates.
        /// </summary>
        private readonly Action[] executionCollection;

        /// <summary>
        /// CanExecute <see cref="Func{bool}"/> delegate.
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="ParameterlessBlendedObservableCommand"/> object that can always execute.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        public ParameterlessBlendedObservableCommand(params Action[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ParameterlessBlendedObservableCommand"/> object.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public ParameterlessBlendedObservableCommand(Expression<Func<bool>> canExecuteExpression, params Action[] executionCollection)
        {
            if (this.executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="ParameterlessBlendedObservableCommand"/> can execute in its current state.
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
        /// Executes the <see cref="ParameterlessBlendedObservableCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public override void Execute(object parameter)
        {
            foreach (var execute in executionCollection)
                execute();
        }
    }
}
