﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.Observable.SingleHandler.Asynchronous
{

    /// <summary>
    /// Single handler asynchronous observable command with object parameter.
    /// </summary>
    public sealed class AsyncObservableCommand : ObservableCommandBase
    {

        /// <summary>
        /// Execute <see cref="Func{object, Task}"/> delegate.
        /// </summary>
        private readonly Func<object, Task> execute;

        /// <summary>
        /// CanExecute <see cref="Func{object, bool}"/> delegate.
        /// </summary>
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Creates a new <see cref="AsyncObservableCommand"/> object that can always execute.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        public AsyncObservableCommand(Func<object, Task> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AsyncObservableCommand"/> object.
        /// </summary>
        /// <param name="execute">Execution delegate.</param>
        /// <param name="canExecuteExpression">Execution status expression.</param>
        public AsyncObservableCommand(Func<object, Task> execute, Expression<Func<object, bool>> canExecuteExpression)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            canExecute = canExecuteExpression.Compile();
            ObserveCanExecuteChanged(canExecuteExpression);
        }

        /// <summary>
        /// Determines whether this <see cref="AsyncObservableCommand"/> can execute in its current state.
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
        /// Executes the <see cref="AsyncObservableCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public async override void Execute(object parameter)
        {
            await execute(parameter);
        }
    }

}
