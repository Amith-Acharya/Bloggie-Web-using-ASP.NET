
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggy.web.Repository
{
    public class cloudinaryRepository : IImageUploadRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public cloudinaryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account
            (
                configuration.GetSection("Coudinary")["CloudName"],
                configuration.GetSection("Coudinary")["ApiKey"],
                configuration.GetSection("Coudinary")["ApiSecret"]
            );    
        }

        public Task DeleteImageAsync(string imageUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName

            };
            var uploadResult = await client.UploadAsync(uploadParams);
            if(uploadResult!= null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
