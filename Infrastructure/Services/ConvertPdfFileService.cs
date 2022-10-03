using Domain.Common;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using Application.Interfaces.Services;
using System.Text;

namespace Infrastructure.Services
{
    public class ConvertPdfFileService : IConvertToPdfFileService
    {
        public async Task<FileData> ConvertAsync(FileData file)
        {
            using var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(Encoding.Unicode.GetString(file.Data));
            var pdfContent = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });

            return new FileData()
            {
                Data = pdfContent,
                ContentType = "application/pdf",
                FileName = file.FileName
            };
        }
    }
}
