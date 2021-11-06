using ExceptionMessageBox.Wpf.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace ExceptionMessageBox.Wpf.Models
{
    /// <summary>
    /// The main view model responsible for the <see cref="ExceptionWindow"/>.
    /// </summary>
    [Serializable]
    internal class ExceptionWindowModel : ExceptionMessageBoxModelBase
    {
        #region Private Instance Fields

        /// <summary>
        /// Holds the <see cref="ICommand"/> object that displayes the exception
        /// content details in the tree view window.
        /// </summary>
        private ICommand? _detailCmd;

        /// <summary>
        /// The <see cref="ExceptionSeverity"/> value indicating which graphic 
        /// to display to the user.
        /// </summary>
        private ExceptionSeverity _severity;

        /// <summary>
        /// The title to display in the exception box title bar.
        /// </summary>
        private string _winTitle = String.Empty;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionWindowModel"/> class
        /// with an <see cref="Exception"/> object.
        /// </summary>
        /// <param name="ex">
        /// An <see cref="Exception"/> object that needs to be reviewed.
        /// </param>
        public ExceptionWindowModel(Exception ex)
            : base(ex)
        {
            _severity = ExceptionSeverity.None;
            Message = $"{ExceptionItem.Message} ({ExceptionItem.Source})";
        }

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Gets the command to open the exception details window.
        /// </summary>
        public ICommand? DetailsCommand => _detailCmd ??= new ExceptionMessageBoxCommand<Window>(DisplayDetails);

        /// <summary>
        /// Gets or sets the severity of the exception.
        /// </summary>
        /// <remarks>
        /// This is responsible for setting the icon of the resulting message box.
        /// </remarks>
        public ExceptionSeverity Severity
        {
            get => _severity;
            set
            {
                _severity = value;
                OnPropertyChanged(nameof(Severity));
            }
        }

        public string WindowTitle
        {
            get => _winTitle;
            set
            {
                _winTitle = value;
                OnPropertyChanged(nameof(WindowTitle));
            }
        }

        #endregion

        #region Private Instance Methods

        /// <summary>
        /// Shows the details of the current exception in the designated 
        /// window.
        /// </summary>
        /// <param name="owner">
        /// The parent window that is calling the exception details window.
        /// </param>
        private void DisplayDetails(Window? owner)
        {
            if (owner is null) throw new ArgumentNullException(nameof(owner));

            ExceptionDetailsWindowModel model = new
#if NETCOREAPP3_1
                ExceptionDetailsWindowModel
#endif
                (ExceptionItem);
            ExceptionDetailsWindow win = new
#if NETCOREAPP3_1
                ExceptionDetailsWindow
#endif
                ()
            {
                DataContext = model
            };

            owner.IsEnabled = false;
            win.Owner = owner;
            _ = win.ShowDialog();
            owner.IsEnabled = true;
        }

        #endregion
    }
}
