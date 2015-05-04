using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Winnemen.Core.Image
{
    public static class Resizer
    {
        /// <summary>
        /// Rotates the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static byte[] Rotate(this byte[] b, float angle, ImageFormat format)
        {
            var converter = new Converter();

            byte[] rotatedImage;
            using (Bitmap image = b.ConvertByteArrayToBitmap())
            {
                var i = Rotate(image, angle);
                rotatedImage = converter.ConvertBitmaptoBytes(i, format);
            }

            return rotatedImage;
        }

        /// <summary>
        /// Rotate90s the degrees left.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static byte[] Rotate90DegreesLeft(this byte[] bytes)
        {
            const RotateFlipType flipType = RotateFlipType.Rotate270FlipNone;
            return Rotate90Degrees(bytes, flipType);
        }

        /// <summary>
        /// Rotate90s the degrees right.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static byte[] Rotate90DegreesRight(this byte[] bytes)
        {
            const RotateFlipType flipType = RotateFlipType.Rotate90FlipNone;
            return Rotate90Degrees(bytes, flipType);
        }

        /// <summary>
        /// Rotate90s the degrees.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="flipType">Type of the flip.</param>
        /// <returns></returns>
        private static byte[] Rotate90Degrees(byte[] bytes, RotateFlipType flipType)
        {
            var converter = new Converter();
            byte[] bitmaptoBytes;
            using (Bitmap bitmap = ConvertByteArrayToBitmap(bytes))
            {
                bitmap.RotateFlip(flipType);
                bitmaptoBytes = converter.ConvertBitmaptoBytes(bitmap, ImageFormat.Jpeg);
            }
            return bitmaptoBytes;
        }

        /// <summary>
        /// Rotates the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public static Bitmap Rotate(System.Drawing.Image image, float angle)
        {
            //create a new empty bitmap to hold rotated image
            var returnBitmap = new Bitmap(image.Width, image.Height);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image
            g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
            //rotate
            g.RotateTransform(angle);
            //move image back
            g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(image, new Point(0, 0));

            return returnBitmap;
        }

        /// <summary>
        /// Resizes the specified bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static byte[] Resize(this byte[] bitmap, int width, int height, ImageFormat format)
        {
            var converter = new Converter();
            byte[] rotatedBits;

            using (Bitmap image = ConvertByteArrayToBitmap(bitmap))
            {
                var i = Resize(image, width, height);
                rotatedBits = converter.ConvertBitmaptoBytes(i, format);
            }

            return rotatedBits;
        }

        /// <summary>
        /// Resizes the specified bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="max"> The max height and the max width. </param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static byte[] ResizeToMax(this byte[] bitmap, Size max, ImageFormat format)
        {
            var converter = new Converter();
            byte[] rotatedBits;

            using (Bitmap image = ConvertByteArrayToBitmap(bitmap))
            {
                var size = ResizeToMaxWidthOrMaxHeight(image.Size, max);
                var resize = Resize(image, size.Width, size.Height);
                rotatedBits = converter.ConvertBitmaptoBytes(resize, format);
            }

            return rotatedBits;
        }

        /// <summary>
        /// Finds the image perspective.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="maxSize">The max size.</param>
        /// <returns></returns>
        private static Size ResizeToMaxWidthOrMaxHeight(Size photo, Size maxSize)
        {
            Size imageSize = new Size();

            //determine if it's a portrait or landscape
            if (photo.Height > photo.Width)
            {
                //portrait
                decimal ratio = (photo.Width / (decimal)photo.Height);
                imageSize.Height = (photo.Height < maxSize.Height ? photo.Height : maxSize.Height);
                imageSize.Width = (int)(ratio * imageSize.Height);
            }
            else
            {
                //landscape
                decimal ratio = (photo.Height / (decimal)photo.Width);
                imageSize.Width = (photo.Width < maxSize.Width ? photo.Width : maxSize.Width);
                imageSize.Height = (int)(ratio * imageSize.Width);
            }

            return imageSize;
        }


        /// <summary>
        /// Converts the byte array to bitmap.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static Bitmap ConvertByteArrayToBitmap(this byte[] bytes)
        {
            var converter = new Converter();
            Bitmap image = converter.ConvertByteArrayToBitmap(bytes);
            return image;
        }

        /// <summary>
        /// Resizes the bitmap, pass in the new sizes
        /// </summary>
        /// <param name="bitmap">bitmap to be resized</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static Bitmap Resize(this Bitmap bitmap, int width, int height)
        {
            var result = new Bitmap(width, height);
            using (Graphics graphic = Graphics.FromImage(result))
            {
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphic.DrawImage(bitmap, 0, 0, width, height);
            }

            return result;
        }

    }
}
