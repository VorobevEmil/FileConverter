﻿using Domain.Common;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Infrastructure.Helpers;

public static class ConvertFileHelper
{
    public static async Task<FileData> ConvertFromFileToFileDataAsync(IFormFile file)
    {
        FileData fileData = new FileData()
        {
            ContentType = file.ContentType,
            FileName = file.ContentType,
        };

        using (var fileStream = file.OpenReadStream())
        {
            using (var readerFileStream = new StreamReader(fileStream))
            {
                fileData.Data = Encoding.Unicode.GetBytes(await readerFileStream.ReadToEndAsync());
            }
        }

        return fileData;
    }
}
