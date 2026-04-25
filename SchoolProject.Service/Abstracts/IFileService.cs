using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Abstracts
{
    public interface IFileService
    {
        Task<string> UploadImage(string folder, IFormFile file);
    }
}
