using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ExceptionMessageBox.Wpf.Extensions
{
    /// <summary>
    /// Collection of extension methods targeting <see cref="Bitmap"/> object.
    /// </summary>
    public static class BitmapExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Converts a <see cref="Bitmap"/> image to an <see cref="ImageSource"/>
        /// object.
        /// </summary>
        /// <param name="bmp">
        /// The <see cref="Bitmap"/> image to convert.
        /// </param>
        /// <returns>
        /// An <see cref="ImageSource"/> object representation of <paramref name="bmp"/>.
        /// </returns>
        public static BitmapSource GetBitmapSource(this Bitmap bmp)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bmp.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        #endregion
    }
}
