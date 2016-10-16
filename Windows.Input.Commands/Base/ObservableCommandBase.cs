using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

namespace Windows.Input.Commands.Base
{
    /// <summary>
    /// Abstract implementation for ICommand interface with observable CanExcuteChanged pattern.
    /// </summary>
    public abstract class ObservableCommandBase : ICommand
    {
        #region System.Windows.Input.ICommand Implementation

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.
        /// </param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// Raises CanExecuteChanged event so that binding clients update 
        /// its IsEnabled property value to the current return value of CanExecute method.
        /// [Implicit Call]
        /// </summary>
        protected virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Observable CanExcuteChanged Pattern Implementation

        /// <summary>
        /// Contains the properties that affects CanExcuteChanged.
        /// </summary>
        private readonly HashSet<string> observableProperties = new HashSet<string>();

        /// <summary>
        /// A wrapper for peoperty INotifiyPropertyChanged implementation.
        /// </summary>
        private INotifyPropertyChanged notifier;

        /// <summary>
        /// Extracts the a property from expression.
        /// </summary>
        /// <param name="expression">Expression containing the propert.</param>
        /// <returns>Property name.</returns>
        private string ExtractPropertyName(LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("canExecuteExpression", "The expression cannot be null.");

            var expressionBody = (MemberExpression)expression.Body;
            if (expressionBody == null)
                throw new ArgumentException("canExecuteExpression", "The body of the expression cannot be null.");

            var expressionProperty = (PropertyInfo)expressionBody.Member;
            if (expressionProperty == null)
                throw new ArgumentException("canExecuteExpression", "The return property of the expression cannot be null.");

            var expressionPropertyGetMethod = expressionProperty.GetMethod;
            if (expressionPropertyGetMethod.IsStatic)
                throw new ArgumentException("canExecuteExpression", "The return property of the expression cannot be static.");

            return expressionBody.Member.Name;
        }

        /// <summary>
        /// Updates CanExcuteChanged implicitly.
        /// [Don't Override Untill You Really Know Well What You Are Doing]
        /// </summary>
        /// <param name="canExecuteExpression"></param>
        protected virtual void ObserveCanExecuteChanged(LambdaExpression canExecuteExpression)
        {
            try
            {
                var property = ExtractPropertyName(canExecuteExpression);

                if (!observableProperties.Contains(property))
                    observableProperties.Add(property);

                var memberExpression = (MemberExpression)canExecuteExpression.Body;

                if (memberExpression == null)
                    return;

                if (notifier == null)
                {
                    var constantExpression = (ConstantExpression)memberExpression.Expression;
                    if (constantExpression != null)
                    {
                        notifier = (INotifyPropertyChanged)constantExpression.Value;
                        if (notifier != null)
                        {
                            notifier.PropertyChanged += (s, a) =>
                            {
                                RaiseCanExecuteChanged();
                            };
                        }
                    }
                }
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Argument Exception: Your expression does not contain any properties to be observed, You can use Command instead of ObservableCommand. See innerExpception for more details.", e);
            }
        }

        #endregion
    }
}
