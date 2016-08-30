using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

namespace Windows.Input.Commands.Base
{
    public abstract class ObservableCommandBase : ICommand
    {
        #region System.Windows.Input.ICommand Implementation

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        #endregion

        #region Observable CanExcuteChanged Pattern Implementation

        private readonly HashSet<string> observableProperties = new HashSet<string>();

        private INotifyPropertyChanged notifier;

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
            catch (ArgumentException)
            {
                throw new ArgumentException("Argument Exception: Your expression does not contain any properties ti be observed, You can use ordinary Command instead of Observed Command.");
            }
        }

        private string ExtractPropertyName(LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var memberExpression = (MemberExpression)expression.Body;
            if (memberExpression == null)
                throw new ArgumentException();

            var property = (PropertyInfo)memberExpression.Member;
            if (property == null)
                throw new ArgumentException();

            var getMethod = property.GetMethod;
            if (getMethod.IsStatic)
                throw new ArgumentException();

            return memberExpression.Member.Name;
        }

        protected virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
