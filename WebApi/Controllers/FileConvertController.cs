using Application.Interfaces.Services;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ConvertHtmlToPdf.Controllers
{
    [ApiController]
    [RequestSizeLimit(100_000_000)]
    [Route("api/[controller]")]
    public class FileConvertController : ControllerBase
    {
        private readonly IConvertToPdfFileService _convertToPdfFileService;

        private readonly ILogger<FileConvertController> _logger;

        public FileConvertController(IConvertToPdfFileService convertToPdfFileService, ILogger<FileConvertController> logger)
        {
            _convertToPdfFileService = convertToPdfFileService;
            _logger = logger;
        }

        [HttpPost("HtmlToPdf")]
        public async Task<IActionResult> HtmlToPdf(IFormFile file)
        {
            try
            {
                if (!file.ContentType.EndsWith("html"))
                    return BadRequest("Для конвертирования в pdf, отправьте html документ");

                var fileData = await _convertToPdfFileService.ConvertAsync(await ConvertFromFileToFileDataAsync(file));
                return File(fileData.Data, fileData.ContentType, fileData.FileName);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest("Не удалось конвертировать html в pdf");
            }
        }

        private async Task<FileData> ConvertFromFileToFileDataAsync(IFormFile file)
        {
            FileData fileData = new FileData()
            {
                ContentType = file.ContentType,
                FileName = file.ContentType,
            };

            var fileStream = file.OpenReadStream();
            var readerFileStream = new StreamReader(fileStream);
            fileData.Data = Encoding.Unicode.GetBytes(await readerFileStream.ReadToEndAsync());

            fileStream.Dispose();
            readerFileStream.Dispose();

            return fileData;
        }
    }
}