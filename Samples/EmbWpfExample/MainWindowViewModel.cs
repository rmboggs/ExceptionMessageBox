using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;
using E = ExceptionMessageBox.Wpf;

namespace EmbWpfExample
{
    public class MainWindowViewModel : BindableBase
    {
        #region Private Instance Fields

        private readonly ICommand? _aggCommand;

        private readonly ICommand? _infoCommand;

        private readonly ICommand? _invalidCommand;

        private readonly ICommand? _normalCommand;

        private readonly ICommand? _titleCommand;

        private readonly ICommand? _warningCommand;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class
        /// that is empty.
        /// </summary>
        public MainWindowViewModel() : base()
        {
            _aggCommand = new DelegateCommand<Window>(async (win) => await Aggregated(win));
            _infoCommand = new DelegateCommand<Window>(Informational);
            _invalidCommand = new DelegateCommand<Window>(Invalid);
            _normalCommand = new DelegateCommand<Window>(Normal);
            _titleCommand = new DelegateCommand<Window>(Title);
            _warningCommand = new DelegateCommand<Window>(Warning);
        }

        #endregion

        #region Public Instance Properties

        public ICommand? AggregatedExample => _aggCommand;

        public ICommand? InformationalExample => _infoCommand;

        public ICommand? InvalidExample => _invalidCommand;

        public ICommand? NormalExample => _normalCommand;

        public ICommand? TitleExample => _titleCommand;

        public ICommand? WarningExample => _warningCommand;

        #endregion

        #region Private Instance Methods

        private async Task Aggregated(Window owner)
        {
            try
            {
                Task[] tasks = new Task[2];
                tasks[0] = await Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(1000);
                    throw new InvalidCastException("Invalid cast occurred, in theory");
                });
                tasks[1] = await Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(500);
                    throw new InvalidOperationException();
                });
                Task.WaitAll(tasks);
            }
            catch (Exception e)
            {
                _ = E.ExceptionMessageBox.Show(owner, e);
            }
        }

        private void Informational(Window owner)
        {
            try
            {
                throw new ArgumentNullException(nameof(owner));
            }
            catch (Exception e)
            {
                _ = E.ExceptionMessageBox.Show(owner, e, E.ExceptionSeverity.Informational);
            }
        }

        private void Invalid(Window owner)
        {
            try
            {
                throw new DivideByZeroException("What just happened?");
            }
            catch (Exception e)
            {

                _ = E.ExceptionMessageBox.Show(owner, e, E.ExceptionSeverity.Invalid);
            }
        }

        private void Normal(Window owner)
        {
            try
            {
                try
                {
                    throw new ArgumentOutOfRangeException();
                }
                catch (Exception x)
                {

                    throw new ArgumentException("Something went wrong", x);
                }
            }
            catch (Exception e)
            {
                _ = E.ExceptionMessageBox.Show(owner, e);
            }
        }

        private void Title(Window owner)
        {
            try
            {
                throw new System.IO.DriveNotFoundException("Has anyone seen drive B:");
            }
            catch (Exception e)
            {
                _ = E.ExceptionMessageBox.Show(owner, e, "Where did B: go?");
            }
        }

        private void Warning(Window owner)
        {
            try
            {
                OutOfMemoryException eee = new() { HelpLink = "https://stackoverflow.com/questions/10750849/how-to-make-textblock-scrollable-inwpf" };

                throw eee;
            }
            catch (Exception e)
            {
                _ = E.ExceptionMessageBox.Show(owner, e, E.ExceptionSeverity.Warning);
            }
        }

        #endregion
    }
}
