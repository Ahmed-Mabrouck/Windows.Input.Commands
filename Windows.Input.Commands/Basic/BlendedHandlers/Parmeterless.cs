using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Basic.BlendedHandlers
{

    /// <summary>
    /// Blended handlers parameterless command.
    /// </summary>
    public sealed class ParameterlessBlendedBasicCommand : CommandBase
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
        /// Creates a new <see cref="ParameterlessBlendedBasicCommand"/> object that can always execute.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        public ParameterlessBlendedBasicCommand(params Action[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ParameterlessBlendedCommand"/> object.
        /// </summary>
        /// <param name="executionCollection">Execution delegates.</param>
        /// <param name="canExecute">Execution status delegate.</param>
        public ParameterlessBlendedBasicCommand(Func<bool> canExecute, params Action[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="ParameterlessBlendedBasicCommand"/> can execute in its current state.
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
        /// Executes the <see cref="ParameterlessBlendedBasicCommand"/> on the current command target.
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
