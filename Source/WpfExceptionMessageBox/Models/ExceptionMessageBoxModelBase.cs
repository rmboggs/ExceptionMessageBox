using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfExceptionMessageBox.Models
{
    /// <summary>
    /// Base class to use when building MVVM objects for this project.
    /// </summary>
    [Serializable]
    internal abstract class ExceptionMessageBoxModelBase : INotifyPropertyChanged
    {
        #region Protected Static Fields

        /// <summary>
        /// Holds the current culture settings to use when formatting exception messages.
        /// </summary>
        protected static readonly CultureInfo culture = CultureInfo.CurrentCulture;

        #endregion

        #region Private Instance Fields

        /// <summary>
        /// Holds the <see cref="ICommand"/> object responsible for closing the 
        /// current window.
        /// </summary>
        private ICommand? _closeWinCmd;

        /// <summary>
        /// Holds the <see cref="ICommand"/> object that copies the details
        /// of the exception onto the clipboard.
        /// </summary>
        private ICommand? _copyCmd;

        /// <summary>
        /// The error message to display to the user.
        /// </summary>
        private string _message = String.Empty;

        #endregion

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMessageBoxModelBase"/> class
        /// with an <see cref="Exception"/> object.
        /// </summary>
        /// <param name="ex">
        /// An <see cref="Exception"/> object that needs to be reviewed.
        /// </param>
        protected ExceptionMessageBoxModelBase(Exception e)
        {
            ExceptionItem = e;
        }

        #endregion

        #region Public Instance Events

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Gets the command to close the current window.
        /// </summary>
        public ICommand? CloseWindowCommand =>
            _closeWinCmd ??= new ExceptionMessageBoxCommand<Window>(CloseWindow);

        /// <summary>
        /// Gets the command that copies the exception details into the system clipboard.
        /// </summary>
        public ICommand? CopyCommand => _copyCmd ??= new ExceptionMessageBoxCommand(Copy);

        /// <summary>
        /// Gets the <see cref="string"/> message to display to the user.
        /// </summary>
        public string Message
        {
            get => _message;
            protected set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        #endregion

        #region Protected Instance Properties

        /// <summary>
        /// Gets the exception object that was thrown.
        /// </summary>
        protected Exception ExceptionItem
        {
            get;
#if NET5_0_OR_GREATER
            init;
#else
            private set;
#endif
        }

#endregion

        #region Protected Instance Methods

        /// <summary>
        /// Closes the current window.
        /// </summary>
        /// <param name="win">
        /// The <see cref="Window"/> to close.
        /// </param>
        protected virtual void CloseWindow(Window? win)
        {
            if (win == null) throw new ArgumentNullException(nameof(win));
            win.Close();
        }

        /// <summary>
        /// Copies the details from <see cref="ExceptionItem"/> in a user friendly
        /// format into the system clipboard.
        /// </summary>
        protected void Copy() => Clipboard.SetText(FormatExceptionMessage(ExceptionItem));

        /// <summary>
        /// Organizes the information in a thrown <see cref="Exception"/> object
        /// into a user friendly format.
        /// </summary>
        /// <param name="e">
        /// The thrown <see cref="Exception"/> object.
        /// </param>
        /// <returns>
        /// A user friendly formatted representation of <paramref name="e"/>.
        /// </returns>
        protected virtual string FormatExceptionMessage(Exception e)
        {
            StringBuilder txt = new
#if NETCOREAPP3_1
                StringBuilder
#endif
                ();

            _ = txt.AppendFormat(culture, "Exception Type: {0}", e.GetType().Name);
            _ = txt.AppendLine();
            _ = txt.AppendLine("=======================");
            _ = txt.AppendLine();

            if (!String.IsNullOrWhiteSpace(e.Message))
            {
                _ = txt.AppendLine("Exception Message:");
                _ = txt.AppendLine(e.Message);
                _ = txt.AppendLine();
            }

            if (!String.IsNullOrWhiteSpace(e.StackTrace))
            {
                _ = txt.AppendLine("Exception Stacktrace:");
                _ = txt.AppendLine(e.StackTrace);
                _ = txt.AppendLine();
            }

            if (e.Data != null && e.Data.Count > 0)
            {
                _ = txt.AppendLine("Data:");
                _ = txt.AppendLine("-----");

                foreach (System.Collections.DictionaryEntry? item in e.Data)
                {
                    if (item is null || !item.HasValue) continue;
                    _ = txt.AppendFormat(culture, "{0} = {1}", item.Value.Key, item.Value.Value);
                    _ = txt.AppendLine();
                }
            }

            if (e.TargetSite != null)
            {
                _ = txt.AppendLine("Target Site:");
                _ = txt.AppendLine(e.TargetSite.Name);
                _ = txt.AppendLine();
            }

            if (!String.IsNullOrWhiteSpace(e.Source))
            {
                _ = txt.AppendLine("Exception Source:");
                _ = txt.AppendLine(e.Source);
                _ = txt.AppendLine();
            }

            if (!String.IsNullOrWhiteSpace(e.HelpLink))
            {
                _ = txt.AppendLine("Help Link:");
                _ = txt.AppendLine(e.HelpLink);
                _ = txt.AppendLine();
            }

            if (e is AggregateException agg)
            {
                _ = txt.AppendLine("***********************");
                _ = txt.AppendLine();

                if (agg.InnerExceptions != null && agg.InnerExceptions.Any())
                {
                    foreach (Exception x in agg.InnerExceptions)
                    {
                        _ = txt.AppendLine(FormatExceptionMessage(x));
                    }
                }
            }
            else if (e.InnerException != null)
            {
                _ = txt.AppendLine("+++++++++++++++++++++++");
                _ = txt.AppendLine();
                _ = txt.AppendLine(FormatExceptionMessage(e.InnerException));
            }
            return txt.ToString();
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event with the name of the
        /// property raising the event.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property raising the event.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event with the provided
        /// <see cref="PropertyChangedEventArgs"/>.
        /// </summary>
        /// <param name="e">
        /// The <see cref="PropertyChangedEventArgs"/> to pass to the event.
        /// </param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        #endregion
    }
}
