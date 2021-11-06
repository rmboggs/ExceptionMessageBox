using ExceptionMessageBox.Wpf.Models;
using ExceptionMessageBox.Wpf.Windows;
using System;
using System.Windows;

namespace ExceptionMessageBox.Wpf
{
    /// <summary>
    /// Exception Message Box entry class.
    /// </summary>
    public static class ExceptionMessageBox
    {
        #region Private Static Fields

        /// <summary>
        /// The default <see cref="ExceptionSeverity"/> value.
        /// </summary>
        private const ExceptionSeverity _defaultSeverity = ExceptionSeverity.Critical;

        /// <summary>
        /// The default text to use for the message box title.
        /// </summary>
        private const string _defaultTitle = "An exception occurred";

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Displays a message box containing the details of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="e">
        /// The <see cref="Exception"/> to display to the user.
        /// </param>
        /// <returns>
        /// Indicates whether or not the user accepted or canceled the message box.
        /// </returns>
        public static bool? Show(Exception e)
        {
            string title = e.GetType()?.FullName ?? _defaultTitle;
            return Show(e, title, _defaultSeverity);
        }

        /// <summary>
        /// Displays a message box containing the details of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="e">
        /// The <see cref="Exception"/> to display to the user.
        /// </param>
        /// <param name="title">
        /// The text to display in the message box title bar.
        /// </param>
        /// <returns>
        /// Indicates whether or not the user accepted or canceled the message box.
        /// </returns>
        public static bool? Show(Exception e, string title) => Show(e, title, _defaultSeverity);

        /// <summary>
        /// Displays a message box containing the details of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="e">
        /// The <see cref="Exception"/> to display to the user.
        /// </param>
        /// <param name="severity">
        /// The severity indicator of the icon to display to the user.
        /// </param>
        /// <returns>
        /// Indicates whether or not the user accepted or canceled the message box.
        /// </returns>
        public static bool? Show(Exception e, ExceptionSeverity severity)
        {
            string title = e.GetType()?.FullName ?? _defaultTitle;
            return Show(e, title, severity);
        }

        /// <summary>
        /// Displays a message box containing the details of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="e">
        /// The <see cref="Exception"/> to display to the user.
        /// </param>
        /// <param name="title">
        /// The text to display in the message box title bar.
        /// </param>
        /// <param name="severity">
        /// The severity indicator of the icon to display to the user.
        /// </param>
        /// <returns>
        /// Indicates whether or not the user accepted or canceled the message box.
        /// </returns>
        public static bool? Show(Exception e, string title, ExceptionSeverity severity)
        {
            ExceptionWindowModel m = new
#if NETCOREAPP3_1
                ExceptionWindowModel
#endif
                (e)
            {
                WindowTitle = title,
                Severity = severity
            };
            ExceptionWindow win = new
#if NETCOREAPP3_1
                ExceptionWindow
#endif
                ()
            {
                DataContext = m
            };
            return win.ShowDialog();
        }

        /// <summary>
        /// Displays a message box containing the details of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="owner">
        /// The <see cref="Window"/> that will own the message box.
        /// </param>
        /// <param name="e">
        /// The <see cref="Exception"/> to display to the user.
        /// </param>
        /// <returns>
        /// Indicates whether or not the user accepted or canceled the message box.
        /// </returns>
        public static bool? Show(Window owner, Exception e)
        {
            string title = e.GetType()?.FullName ?? _defaultTitle;
            return Show(owner, e, title, _defaultSeverity);
        }

        /// <summary>
        /// Displays a message box containing the details of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="owner">
        /// The <see cref="Window"/> that will own the message box.
        /// </param>
        /// <param name="e">
        /// The <see cref="Exception"/> to display to the user.
        /// </param>
        /// <param name="title">
        /// The text to display in the message box title bar.
        /// </param>
        /// <returns>
        /// Indicates whether or not the user accepted or canceled the message box.
        /// </returns>
        public static bool? Show(Window owner, Exception e, string title) => Show(owner, e, title, _defaultSeverity);

        /// <summary>
        /// Displays a message box containing the details of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="owner">
        /// The <see cref="Window"/> that will own the message box.
        /// </param>
        /// <param name="e">
        /// The <see cref="Exception"/> to display to the user.
        /// </param>
        /// <param name="severity">
        /// The severity indicator of the icon to display to the user.
        /// </param>
        /// <returns>
        /// Indicates whether or not the user accepted or canceled the message box.
        /// </returns>
        public static bool? Show(Window owner, Exception e, ExceptionSeverity severity)
        {
            string title = e.GetType()?.FullName ?? _defaultTitle;
            return Show(owner, e, title, severity);
        }

        /// <summary>
        /// Displays a message box containing the details of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="owner">
        /// The <see cref="Window"/> that will own the message box.
        /// </param>
        /// <param name="e">
        /// The <see cref="Exception"/> to display to the user.
        /// </param>
        /// <param name="title">
        /// The text to display in the message box title bar.
        /// </param>
        /// <param name="severity">
        /// The severity indicator of the icon to display to the user.
        /// </param>
        /// <returns>
        /// Indicates whether or not the user accepted or canceled the message box.
        /// </returns>
        private static bool? Show(Window owner, Exception e, string title, ExceptionSeverity severity)
        {
            ExceptionWindowModel m = new
#if NETCOREAPP3_1
                ExceptionWindowModel
#endif
                (e)
            {
                WindowTitle = title,
                Severity = severity
            };
            ExceptionWindow win = new
#if NETCOREAPP3_1
                ExceptionWindow
#endif
                ()
            {
                DataContext = m
            };
            win.Owner = owner;
            return win.ShowDialog();
        }

        #endregion
    }
}
