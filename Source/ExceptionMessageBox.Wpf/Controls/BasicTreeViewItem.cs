using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ExceptionMessageBox.Wpf.Controls
{
    /// <summary>
    /// <see cref="TreeViewItem"/> containing basic information to store
    /// in the <see cref="TreeView"/>.
    /// </summary>
    [Serializable]
    public abstract class BasicTreeViewItem : TreeViewItem
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicTreeViewItem"/>
        /// class that is empty.
        /// </summary>
        protected BasicTreeViewItem() : base()
        {
            DisplayHelpButton = false;
            HelpLink = null;
        }

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Gets the value indicating whether the help button should be displayed.
        /// </summary>
        public bool DisplayHelpButton { get; protected set; }

        /// <summary>
        /// Gets the url link to the appropriate web page to get additional
        /// information regarding the current exception.
        /// </summary>
        public Uri? HelpLink { get; protected set; }

        #endregion
    }
}
