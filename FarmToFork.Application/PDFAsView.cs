using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FarmToFork.Application
{
    public class PdfAsView : IActionResult
    { 
        private readonly string _url;
        public PdfAsView(string url)
        {
            _url = url;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            string tempFileName = Path.GetTempFileName();
            var exited = false;
            var fileName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "wkhtmltopdf" : @"wkhtmltopdf.exe";
            using (var process = new Process())
            {
                process.StartInfo.FileName = @"wkhtmltopdf.exe"; // relative path. absolute path works too.
                process.StartInfo.Arguments = $"-O Portrait --dpi 600 -L 2mm -R 2mm -T 5mm -B 5mm --page-size A6 {_url} {tempFileName}";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
                process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
                Console.WriteLine("starting");
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                exited = process.WaitForExit(1000 * 15);     // (optional) wait up to 10 seconds
                Console.WriteLine($"exit {exited}");
                string filePath = tempFileName;
                if (exited)
                {
                    await Task.CompletedTask;
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        await fileStream.CopyToAsync(context.HttpContext.Response.Body);
                        var fileStreamResult = new FileStreamResult(fileStream, "application/pdf");
                        await fileStreamResult.ExecuteResultAsync(context);
                    }
                }
            }
        }
    }
}
