using System;

namespace WpfExceptionMessageBox.Controls
{
    /// <summary>
    /// The <see cref="BasicTreeViewItem"/> containing <see cref="string"/>
    /// information to store in the tree view.
    /// </summary>
    [Serializable]
    internal class StringTreeViewItem : BasicTreeViewItem
    {
        #region Private Instance Fields

        /// <summary>
        /// The information to report on.
        /// </summary>
        private readonly string _details;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTreeViewItem"/> class
        /// with the specified information.
        /// </summary>
        /// <param name="details">
        /// The information that the new object will contain.
        /// </param>
        public StringTreeViewItem(string details) : base()
        {
            _details = details;
        }

        #endregion

        #region Public Instance Methods

        /// <inheritdoc/>
        public override string ToString()
        {
            return _details;
        }

        #endregion
    }
}
