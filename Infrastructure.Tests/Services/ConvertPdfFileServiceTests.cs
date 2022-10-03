using Application.Interfaces.Services;
using Domain.Common;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Services
{
    public class ConvertPdfFileServiceTests
    {
        [Fact]
        public async Task Should_Return_Pdf_File()
        {
            string htmlPage = "<!DOCTYPE html> <html lang=\"en\"> <head> <meta charset=\"utf-8\"> <title>Convert Files</title> </head> <body> <div style=\"font-size: 30px; text-align: center;color: red;\"> <p>Hello world!!</p> </div> </body> </html>";

            var htmlFileData = new FileData()
            {
                ContentType = "text/html",
                FileName = "MyPage",
                Data = Encoding.Unicode.GetBytes(htmlPage)
            };

            IConvertToPdfFileService convertToPdf = new ConvertPdfFileService();

            var pdfFileData = await convertToPdf.ConvertAsync(htmlFileData);

            Assert.NotNull(pdfFileData);
            Assert.True(pdfFileData.ContentType == "application/pdf");
            Assert.True(pdfFileData.Data.Length > 0);
        }

        [Fact]
        public async Task ArgumentNullException_Should_When_Field_HtmlFileData_Null()
        {
            var htmlFileData = new FileData();

            IConvertToPdfFileService convertToPdf = new ConvertPdfFileService();

            await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await convertToPdf.ConvertAsync(htmlFileData));
        }

        [Fact]
        public async Task NullReferenceException_Should_When_FileData_Null()
        {
            FileData htmlFileData = null!;

            IConvertToPdfFileService convertToPdf = new ConvertPdfFileService();

            await Assert.ThrowsAsync<NullReferenceException>(async () => await convertToPdf.ConvertAsync(htmlFileData));
        }
    }
}
