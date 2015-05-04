using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Winnemen.Core.Image
{
    public class Converter : IConverter
    {
        /// <summary>
        /// Converts to gray scale.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public Bitmap ConvertToGrayScale(Bitmap original)
        {
            unsafe
            {
                //create an empty bitmap the same size as original
                var newBitmap = new Bitmap(original.Width, original.Height);

                //lock the original bitmap in memory
                var originalData = original.LockBits(
                   new Rectangle(0, 0, original.Width, original.Height),
                   ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                //lock the new bitmap in memory
                var newData = newBitmap.LockBits(
                   new Rectangle(0, 0, original.Width, original.Height),
                   ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                //set the number of bytes per pixel
                const int pixelSize = 3;

                for (int y = 0; y < original.Height; y++)
                {
                    //get the data from the original image
                    byte* oRow = (byte*)originalData.Scan0 + (y * originalData.Stride);

                    //get the data from the new image
                    byte* nRow = (byte*)newData.Scan0 + (y * newData.Stride);

                    for (int x = 0; x < original.Width; x++)
                    {
                        //create the grayscale version
                        var grayScale =
                           (byte)((oRow[x * pixelSize] * .11) + //B
                           (oRow[x * pixelSize + 1] * .59) +  //G
                           (oRow[x * pixelSize + 2] * .3)); //R

                        //set the new image's pixel to the grayscale version
                        nRow[x * pixelSize] = grayScale; //B
                        nRow[x * pixelSize + 1] = grayScale; //G
                        nRow[x * pixelSize + 2] = grayScale; //R
                    }
                }

                //unlock the bitmaps
                newBitmap.UnlockBits(newData);
                original.UnlockBits(originalData);

                return newBitmap;
            }
        }


        /// <summary>
        /// Converts the byte array to bitmap.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public Bitmap ConvertByteArrayToBitmap(byte[] bytes)
        {
            Bitmap image;

            using (var memStream = new MemoryStream(bytes))
            {
                image = new Bitmap(memStream);
            }

            return image;
        }

        /// <summary>
        /// Converts the bitmapto bytes.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public byte[] ConvertBitmaptoBytes(System.Drawing.Image image, ImageFormat format)
        {
            byte[] imageBytes;
            using (var outStream = new MemoryStream())
            {
                image.Save(outStream, format);
                outStream.Position = 0;
                imageBytes = new byte[outStream.Length];
                outStream.Read(imageBytes, 0, (int)outStream.Length);
            }

            return imageBytes;
        }
    }
}
