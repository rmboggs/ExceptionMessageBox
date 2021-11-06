namespace ExceptionMessageBox.Wpf
{
    /// <summary>
    /// Indicates the severity of the exception that was thrown.
    /// </summary>
    public enum ExceptionSeverity : byte
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Informational.
        /// </summary>
        Informational,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning,

        /// <summary>
        /// Invalid.
        /// </summary>
        Invalid,

        /// <summary>
        /// Critical.
        /// </summary>
        Critical
    }
}
