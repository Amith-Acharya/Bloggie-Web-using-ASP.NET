namespace Bloggy.web.Repository
{
    public interface IImageUploadRepository
    {
        /// <summary>
        /// Uploads an image asynchronously.
        /// </summary>
        /// <param name="file">The image file to upload.</param>
        /// <returns>A task that represents the asynchronous operation, containing the URL of the uploaded image.</returns>
        Task<string> UploadImageAsync(IFormFile file);
        
        /// <summary>
        /// Deletes an image asynchronously.
        /// </summary>
        /// <param name="imageUrl">The URL of the image to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteImageAsync(string imageUrl);
    }
}
