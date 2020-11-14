using LazZiya.ImageResize;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using System.IO;

namespace Mojito.ServiceDesk.Infrastructure.Services.Common
{
    public class ImageService : IImageService
    {
        public byte[] ResizeImage(byte[] file, int desiredWidth)
        {
            if (file.Length > 0)
            {
                using (var stream = new MemoryStream(file))
                {
                    var image = System.Drawing.Image.FromStream(stream);

                    var scaledImage = ImageResize.ScaleByWidth(image, desiredWidth);

                    using (var ms = new MemoryStream())
                    {
                        scaledImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }

                }
            }
            else
            {
                return null;
            }
        }
    }
}
