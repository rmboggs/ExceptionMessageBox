using System.Windows;
using System.Windows.Controls;

namespace ExceptionMessageBox.Wpf.Controls
{
    /// <summary>
    /// A subclass of the <see cref="TreeView"/> control with access to 
    /// the selected item.
    /// </summary>
    public class SelectableTreeView : TreeView
    {
        #region Public Static Fields

        /// <summary>
        /// Static dependency property handler for the SelectedTreeItem property.
        /// </summary>
        public static readonly DependencyProperty SelectedItem_Property =
            DependencyProperty.Register(nameof(SelectedTreeItem), typeof(object),
                typeof(SelectableTreeView), new UIPropertyMetadata(null));

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableTreeView"/>
        /// class that is empty.
        /// </summary>
        public SelectableTreeView() : base()
        {
            SelectedItemChanged += 
                new RoutedPropertyChangedEventHandler<object>(MapSelectedItem);
        }

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Gets or sets the item currently seelcted in the tree.
        /// </summary>
        public object SelectedTreeItem
        {
            get { return (object)GetValue(SelectedItem_Property); }
            set { SetValue(SelectedItem_Property, value); }
        }

        #endregion

        #region Private Instance Methods

        /// <summary>
        /// Maps the selected item object to the selected tree item property.
        /// </summary>
        /// <param name="sender">
        /// Objct sending event.
        /// </param>
        /// <param name="e">
        /// Event args.
        /// </param>
        private void MapSelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItem != null) SetValue(SelectedItem_Property, SelectedItem);
        }

        #endregion
    }
}
