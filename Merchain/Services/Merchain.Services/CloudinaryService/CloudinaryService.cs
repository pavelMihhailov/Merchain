namespace Merchain.Services.CloudinaryService
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Merchain.Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class CloudinaryService
    {
        private readonly Cloudinary cloudinary;
        private readonly ILogger<CloudinaryService> logger;

        public CloudinaryService(Cloudinary cloudinary, ILogger<CloudinaryService> logger)
        {
            this.cloudinary = cloudinary;
            this.logger = logger;
        }

        public async Task<string> UploadImage(IFormFile image)
        {
            if (image != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);

                    byte[] imageBytes = memoryStream.ToArray();

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, new MemoryStream(imageBytes)),
                    };

                    var result = await this.cloudinary.UploadAsync(uploadParams);

                    if (result.Error != null)
                    {
                        this.logger.LogError(result.Error.Message);

                        return string.Empty;
                    }

                    return result.SecureUri.AbsoluteUri;
                }
            }

            return ProductConstants.DefaultImage;
        }
    }
}
