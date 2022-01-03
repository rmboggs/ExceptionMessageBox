using WpfExceptionMessageBox.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfExceptionMessageBox.Models
{
    /// <summary>
    /// The main view model responsible for the <see cref="Windows.ExceptionDetailsWindow"/>
    /// </summary>
    [Serializable]
    internal class ExceptionDetailsWindowModel : ExceptionMessageBoxModelBase
    {
        #region Private Instance Fields

        /// <summary>
        /// Indicates whether or not the help button should be displayed.
        /// </summary>
        private bool _helpIsVis;

        /// <summary>
        /// The <see cref="Uri"/> link to the help details for the current 
        /// <see cref="Exception"/>.
        /// </summary>
        private Uri? _helpLink;

        /// <summary>
        /// The <see cref="ICommand"/> to open the help link (if applicable).
        /// </summary>
        private ICommand? _openHelpLinkCmd;

        /// <summary>
        /// The object currently selected in the tree.
        /// </summary>
        private object? _selectedItem;

        /// <summary>
        /// The collection of <see cref="BasicTreeViewItem"/> elements currently
        /// visible in the tree view.
        /// </summary>
        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        private ObservableCollection<BasicTreeViewItem> _treeData;

        /// <summary>
        /// A read-only version of <see cref="_treeData"/>.
        /// </summary>
        private ReadOnlyObservableCollection<BasicTreeViewItem> _treeDataRo;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDetailsWindowModel"/> class
        /// with an <see cref="Exception"/> object.
        /// </summary>
        /// <param name="ex">
        /// An <see cref="Exception"/> object that needs to be reviewed.
        /// </param>
        public ExceptionDetailsWindowModel(Exception e) : base(e)
        {
            _treeData = BuildTreeItemCollection();
            _treeDataRo = new ReadOnlyObservableCollection<BasicTreeViewItem>(_treeData);
            TreeData = _treeDataRo;
        }

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Indicates whether or not the help button should be displayed.
        /// </summary>
        public bool HelpIsVisible
        {
            get => _helpIsVis;
            private set
            {
                _helpIsVis = value;
                OnPropertyChanged(nameof(HelpIsVisible));
            }
        }

        /// <summary>
        /// Gets the command to open the help link when applicable.
        /// </summary>
        public ICommand OpenHelpLinkCommand =>
            _openHelpLinkCmd ??= new ExceptionMessageBoxCommand(OpenHelpLink);

        /// <summary>
        /// Gets or sets the tree view item that is currently selected.
        /// </summary>
        public object? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                if (_selectedItem is BasicTreeViewItem item)
                {
                    Message = item.ToString();
                    HelpIsVisible = item.DisplayHelpButton;
                    _helpLink = item.HelpLink;
                }
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        /// <summary>
        /// Gets the exception data tree view items to display in the tree.
        /// </summary>
        public ReadOnlyObservableCollection<BasicTreeViewItem> TreeData
        {
            get => _treeDataRo;
#if NET5_0_OR_GREATER
            init
#else
            private set
#endif
            {
                _treeDataRo = value;
                OnPropertyChanged(nameof(TreeData));
            }
        }

        #endregion

        #region Private Instance Methods

        /// <summary>
        /// Creates a new observable collection of <see cref="BasicTreeViewItem"/> elements
        /// to insert into the tree view with.
        /// </summary>
        /// <returns>
        /// An <see cref="ObservableCollection{T}"/> of <see cref="BasicTreeViewItem"/> elements.
        /// </returns>
        private ObservableCollection<BasicTreeViewItem> BuildTreeItemCollection()
        {
            ObservableCollection<BasicTreeViewItem> result = new
#if NETCOREAPP3_1
                ObservableCollection<BasicTreeViewItem>
#endif
                ();

            StringTreeViewItem treeTop = new
#if NETCOREAPP3_1
                StringTreeViewItem
#endif
                (FormatExceptionMessage(ExceptionItem))
            {
                Header = "All messages"
            };
            BuildTreeItems(treeTop, ExceptionItem);
            result.Add(treeTop);
            return result;
        }

        /// <summary>
        /// Populates the top tree view item with the contents of the current exception.
        /// </summary>
        /// <param name="top">
        /// The <see cref="TreeViewItem"/> to append the contents to.
        /// </param>
        /// <param name="e">
        /// The current <see cref="Exception"/> with the information to append to
        /// <paramref name="top"/>.
        /// </param>
        private void BuildTreeItems(TreeViewItem top, Exception e)
        {
            ExceptionTreeViewItem item = new
#if NETCOREAPP3_1
                ExceptionTreeViewItem
#endif
                (e);

            if (!String.IsNullOrWhiteSpace(e.Message))
            {
                _ = item.Items.Add(new StringTreeViewItem(e.Message) { Header = "Message" });
            }
            if (!String.IsNullOrWhiteSpace(e.StackTrace))
            {
                _ = item.Items.Add(new StringTreeViewItem(e.StackTrace) { Header = "Stack Trace" });
            }
            if (!String.IsNullOrWhiteSpace(e.HelpLink))
            {
                _ = item.Items.Add(new StringTreeViewItem(e.HelpLink) { Header = "Help Link" });
            }
            if (e.TargetSite != null)
            {
                _ = item.Items.Add(new StringTreeViewItem(e.TargetSite.Name) { Header = "Target Site" });
            }
            _ = top.Items.Add(item);

            if (e is AggregateException agg)
            {
                if (agg.InnerExceptions != null && agg.InnerExceptions.Any())
                {
                    foreach (Exception ex in agg.InnerExceptions)
                    {
                        BuildTreeItems(top, ex);
                    }
                }
            }
            else if (e.InnerException != null)
            {
                BuildTreeItems(top, e.InnerException);
            }
        }

        /// <summary>
        /// Opens the exception help link.
        /// </summary>
        private void OpenHelpLink()
        {
            if (_helpLink is null) return;
            Process p = new
#if NETCOREAPP3_1
                Process
#endif
                ();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = _helpLink.AbsoluteUri;
            _ = p.Start();
        }

        #endregion
    }
}
