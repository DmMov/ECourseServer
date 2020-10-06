using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ECourse.Application.Exceptions;
using ECourse.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ECourse.Infrastructure.Services
{
    public sealed class FileService : IFileService
    {
        public async Task<string> AddFileToDirectoryAsync(IFormFile file, string directory)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.Combine(currentDirectory, directory);

            if(!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            string fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            string fullPath = Path.Combine(newPath, fileName);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<bool> RemoveFileFromDirectoryAsync(string fileName, string directory)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(currentDirectory, directory, fileName);

            if(File.Exists(fullPath))
                File.Delete(fullPath);
            else
                throw new BadRequestException($"error while deleting the file {fileName} from the {directory} directory.");

            return await Task.FromResult(true);
        }
    }
}
