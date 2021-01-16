﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GenericApp.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}