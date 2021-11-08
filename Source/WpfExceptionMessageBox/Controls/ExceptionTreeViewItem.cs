using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace WpfExceptionMessageBox.Controls
{
    /// <summary>
    /// The <see cref="BasicTreeViewItem"/> containing <see cref="Exception"/> information
    /// to store in the tree view.
    /// </summary>
    [Serializable]
    internal class ExceptionTreeViewItem : BasicTreeViewItem
    {
        #region Private Instance Fields

        /// <summary>
        /// The <see cref="Exception"/> to report on.
        /// </summary>
        private readonly Exception _ex;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionTreeViewItem"/> class
        /// with the <see cref="Exception"/> information.
        /// </summary>
        /// <param name="ex">
        /// The <see cref="Exception"/> object.
        /// </param>
        public ExceptionTreeViewItem(Exception ex) : base()
        {
            _ex = ex ?? throw new ArgumentNullException(nameof(ex));

            string[] lines = _ex.Message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            Header = lines.Length > 1 ? lines[0] : _ex.GetType().FullName;

            // setup the HelpLink related items
            if (!String.IsNullOrWhiteSpace(_ex.HelpLink))
            {
                if (Uri.TryCreate(_ex.HelpLink, UriKind.Absolute, out Uri? u))
                {
                    if (u.Scheme == Uri.UriSchemeHttp || u.Scheme == Uri.UriSchemeHttps)
                    {
                        HelpLink = u;
                        DisplayHelpButton = true;
                    }
                }
            }
        }

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Gets the <see cref="Exception"/> information that the current object represents.
        /// </summary>
        public Exception Item => _ex;

        #endregion

        #region Public Instance Methods

        /// <inheritdoc/>
        public override string ToString()
        {
            var txt = new StringBuilder();

            _ = txt.AppendFormat("Exception Type: {0}", _ex.GetType().Name);
            _ = txt.AppendLine();
            _ = txt.AppendLine("=======================");
            _ = txt.AppendLine();

            if (!String.IsNullOrWhiteSpace(_ex.Message))
            {
                _ = txt.AppendLine("Exception Message:");
                _ = txt.AppendLine(_ex.Message);
                _ = txt.AppendLine();
            }
            if (!String.IsNullOrWhiteSpace(_ex.StackTrace))
            {
                _ = txt.AppendLine("Exception Stacktrace:");
                _ = txt.AppendLine(_ex.StackTrace);
                _ = txt.AppendLine();
            }

            if (_ex.Data != null && _ex.Data.Count > 0)
            {
                _ = txt.AppendLine("Data:");
                _ = txt.AppendLine("-----");

                foreach (System.Collections.DictionaryEntry? item in _ex.Data)
                {
                    if (item is null || !item.HasValue) continue;
                    _ = txt.AppendLine($"{item.Value.Key} = {item.Value.Value}");
                }
                _ = txt.AppendLine();
            }

            if (_ex.TargetSite != null)
            {
                _ = txt.AppendLine("Target Site:");
                _ = txt.AppendLine(_ex.TargetSite.ToString());
                _ = txt.AppendLine();
            }

            if (!String.IsNullOrWhiteSpace(_ex.Source))
            {
                _ = txt.AppendLine("Exception Source:");
                _ = txt.AppendLine(_ex.Source);
                _ = txt.AppendLine();
            }

            if (!String.IsNullOrWhiteSpace(_ex.HelpLink))
            {
                _ = txt.AppendLine("Help Link:");
                _ = txt.AppendLine(_ex.HelpLink);
                _ = txt.AppendLine();
            }
            return txt.ToString();
        }

        #endregion
    }
}
