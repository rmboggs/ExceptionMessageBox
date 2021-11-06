using System;
using System.Windows.Input;

namespace ExceptionMessageBox.Wpf
{
    /// <summary>
    /// Basic <see cref="ICommand"/> object used for mvvm purposes.
    /// </summary>
    internal class ExceptionMessageBoxCommand : ICommand
    {
        #region Private Instance Fields

        private readonly Action _exe = () => { };
        private readonly Func<bool> _canExec = () => false;

        #endregion 

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessageBoxCommand{T}"/>
        /// class with the specified execution method.
        /// </summary>
        /// <param name="execute">
        /// The delegate method to call during execution.
        /// </param>
        public ExceptionMessageBoxCommand(Action execute) : this(execute, () => true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessageBoxCommand{T}"/>
        /// class with the specified execution methods.
        /// </summary>
        /// <param name="execute">
        /// The delegate method to call during execution.
        /// </param>
        /// <param name="canExecute">
        /// The delegate method called to determin whether or not this instance
        /// should execute.
        /// </param>
        public ExceptionMessageBoxCommand(Action execute, Func<bool> canExecute)
        {
            _exe = execute;
            _canExec = canExecute;
        }

        #endregion

        #region Public Instance Events

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Public Instance Methods

        /// <summary>
        /// Checks to see if the command can be executed.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the command is able to execute;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool CanExecute() => _canExec();

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute() => _exe();

        #endregion

        #region Private Instance Methods

        /// <inheritdoc/>
        bool ICommand.CanExecute(object? parameter) => CanExecute();

        /// <inheritdoc/>
        void ICommand.Execute(object? parameter) => Execute();

        #endregion
    }

    /// <summary>
    /// Basic <see cref="ICommand"/> object used for mvvm purposes.
    /// </summary>
    internal class ExceptionMessageBoxCommand<T> : ICommand
        where T: class
    {
        #region Private Instance Fields

        private readonly Action<T?> _exe = (a) => { };
        private readonly Func<T?, bool> _canExec = (a) => false;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessageBoxCommand{T}"/>
        /// class with the specified execution method.
        /// </summary>
        /// <param name="execute">
        /// The delegate method to call during execution.
        /// </param>
        public ExceptionMessageBoxCommand(Action<T?> execute) : this(execute, e => true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessageBoxCommand{T}"/>
        /// class with the specified execution methods.
        /// </summary>
        /// <param name="execute">
        /// The delegate method to call during execution.
        /// </param>
        /// <param name="canExecute">
        /// The delegate method called to determin whether or not this instance
        /// should execute.
        /// </param>
        public ExceptionMessageBoxCommand(Action<T?> execute, Func<T?, bool> canExecute)
        {
            _exe = execute;
            _canExec = canExecute;
        }

        #endregion

        #region Public Instance Events

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        /// <summary>
        /// Determines whether or not the current command can execute in its
        /// current state.
        /// </summary>
        /// <param name="parameter">
        /// The command parameter
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this command can be executed; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public bool CanExecute(T? parameter)
        {
            return _canExec == null || _canExec(parameter);
        }

        /// <summary>
        /// The method called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// The command parameter
        /// </param>
        public void Execute(T parameter) => _exe(parameter);

        #region Private Instance Methods

        /// <inheritdoc/>
        bool ICommand.CanExecute(object? parameter)
        {
            T? para = (T?)parameter;
            return _canExec == null || _canExec(para);
        }

        /// <inheritdoc/>
        void ICommand.Execute(object? parameter)
        {
            T? para = (T?)parameter;
            _exe(para);
        }

        #endregion
    }
}
