using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Winnemen.Core.Image.Exif;

namespace Winnemen.Core.Image
{
    public class ImageOrientation
    {
        private readonly IDictionary<int, RotateFlipType> _rotateTypes = new Dictionary<int, RotateFlipType>()
        {
            {
                1,
                RotateFlipType.RotateNoneFlipNone
            },
            {
                2,
                RotateFlipType.RotateNoneFlipX
            },
            {
                3,
                RotateFlipType.Rotate180FlipNone
            },
            {
                4,
                RotateFlipType.Rotate180FlipX
            },
            {
                5,
                RotateFlipType.Rotate90FlipX
            },
            {
                6,
                RotateFlipType.Rotate90FlipNone
            },
            {
                7,
                RotateFlipType.Rotate270FlipX
            },
            {
                8,
                RotateFlipType.Rotate270FlipNone
            }
        };
        private readonly byte[] _image;

        public ImageOrientation(byte[] image)
        {
            _image = image;
        }

        private RotateFlipType GetOrientation(int orientation)
        {
            var rotateFlipType = RotateFlipType.Rotate180FlipNone;
            if (_rotateTypes.ContainsKey(orientation))
            {
                rotateFlipType = _rotateTypes[orientation];
            }

            return rotateFlipType;
        }

        public byte[] OrientImage()
        {
            Bitmap bmp = _image.ConvertByteArrayToBitmap();
            ExifExtractor exifExtractor = new ExifExtractor(ref bmp, "n");
            object obj = exifExtractor["Orientation"];
            if (obj != null)
            {
                RotateFlipType orientation = GetOrientation(Convert.ToInt32(obj));
                if (orientation != RotateFlipType.RotateNoneFlipNone)
                {
                    bmp.RotateFlip(orientation);
                    exifExtractor.SetTag(274, "1");
                    MemoryStream memoryStream = new MemoryStream(_image);
                    bmp.Save(memoryStream, ImageFormat.Jpeg);
                    return memoryStream.GetBuffer();
                }
            }
            return _image;
        }
    }
}
