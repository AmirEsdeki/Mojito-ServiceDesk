namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common
{
    public interface IImageService
    {
        byte[] ResizeImage(byte[] file, int desiredWidth);
    }
}
