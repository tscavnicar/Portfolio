using Fieldr.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fieldr.Application.Common.Interfaces
{
    public interface IAzureStorageService
    {
        Task<string> UploadFileToStorage(string base64, string photoName);
    }
}
