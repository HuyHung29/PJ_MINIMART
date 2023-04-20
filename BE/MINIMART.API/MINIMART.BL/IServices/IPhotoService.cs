using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace MINIMART.BL.IServices
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<List<ImageUploadResult>> AddMultiplePhotoAsync(IFormFileCollection file);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
