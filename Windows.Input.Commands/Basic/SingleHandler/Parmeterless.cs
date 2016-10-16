using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Basic.SingleHandler
{
    /// <summary>
    /// Single handler parameterless command.
    /// </summary>

    public sealed class ParameterlessBasicCommand : CommandBase
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
        /// Creates a new <see cref="ParameterlessBasicCommand"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public ParameterlessBasicCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ParameterlessBasicCommand"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecute">Execution status delegate.</param>
        public ParameterlessBasicCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="ParameterlessBasicCommand"/> can execute in its current state.
        /// </summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        /// <summary>
        /// Executes the <see cref="ParameterlessBasicCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public override void Execute(object parameter)
        {
            execute();
        }
    }
}
