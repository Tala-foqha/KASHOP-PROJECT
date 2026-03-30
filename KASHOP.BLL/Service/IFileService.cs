using Microsoft.AspNetCore.Http;

namespace KASHOP.PL.images
{
    public interface IFileService
    {
        Task<string?> UploadAsync(IFormFile file);
        void Delete(string fileName);
    }
}
