using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Input.Commands.Base;

namespace Windows.Input.Commands.BlendedCallbacks
{
    public sealed class BlendedCommand : CommandBase
    {
        private readonly Action<object>[] executionCollection;
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="executionCollection">The execution logic.</param>
        public BlendedCommand(params Action<object>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="executionCollection">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public BlendedCommand(Func<object, bool> canExecute, params Action<object>[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("execute");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="Command"/> can execute in its current state.
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
        /// Executes the <see cref="Command"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public override void Execute(object parameter)
        {
            foreach (var execute in executionCollection)
                execute(parameter);
        }
    }
    public sealed class AsyncBlendedCommand : CommandBase
    {
        private readonly Func<object, Task>[] executionCollection;
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="executionCollection">The execution logic.</param>
        public AsyncBlendedCommand(params Func<object, Task>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="executionCollection">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public AsyncBlendedCommand(Func<object, bool> canExecute, params Func<object, Task>[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("execute");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="Command"/> can execute in its current state.
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
        /// Executes the <see cref="Command"/> on the current command target.
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

    public sealed class BlendedCommand<T> : CommandBase
    {
        private readonly Action<T>[] executionCollection;
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public BlendedCommand(params Action<T>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public BlendedCommand(Func<T, bool> canExecute, params Action<T>[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("execute");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="Command"/> can execute in its current state.
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
        /// Executes the <see cref="Command"/> on the current command target.
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
    public sealed class AsyncBlendedCommand<T> : CommandBase
    {
        private readonly Func<T, Task>[] executionCollection;
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public AsyncBlendedCommand(params Func<T, Task>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public AsyncBlendedCommand(Func<T, bool> canExecute, params Func<T, Task>[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("execute");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="Command"/> can execute in its current state.
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
        /// Executes the <see cref="Command"/> on the current command target.
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

    public sealed class ParameterlessBlendedCommand : CommandBase
    {
        private readonly Action[] executionCollection;
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="executionCollection">The execution logic.</param>
        public ParameterlessBlendedCommand(params Action[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="executionCollection">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public ParameterlessBlendedCommand(Func<bool> canExecute, params Action[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="Command"/> can execute in its current state.
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
        /// Executes the <see cref="Command"/> on the current command target.
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
    public sealed class AsyncParameterlessBlendedCommand : CommandBase
    {
        private readonly Func<Task>[] executionCollection;
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="executionCollection">The execution logic.</param>
        public AsyncParameterlessBlendedCommand(params Func<Task>[] executionCollection)
            : this(null, executionCollection)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="executionCollection">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public AsyncParameterlessBlendedCommand(Func<bool> canExecute, params Func<Task>[] executionCollection)
        {
            if (executionCollection == null)
                throw new ArgumentNullException("executionCollection");
            this.executionCollection = executionCollection;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether this <see cref="Command"/> can execute in its current state.
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
        /// Executes the <see cref="Command"/> on the current command target.
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
