using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Core.Exception;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace FarmToFork.Business.Utilities
{
    public interface IGeneralUtility
    {
        string GetLoggedInUsername();
        DateTime GetCurrentNepalTime();
        void CreateFolderIfNotExist(string folderPath);
        DateTime ConvertEpochDateTime(long epochTime);
        Task<string> ReturnFileNameFromFile(IFormFile file, string uploadPath);
        void DeleteFileFromServer(string imageUploadPath, string fileName);
        string ToLocalFormat(decimal value);

        string AddName(string firstName, string? middleName, string lastName);

    }
    public class GeneralUtility : IGeneralUtility
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GeneralUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetLoggedInUsername()
        {
            var user = _httpContextAccessor.HttpContext?.User ?? throw new DataValidationException("No Logged In User");

            if (!user.Claims.Any())
                throw new DataValidationException("No claims data!");

            if (user.Claims.All(x => x.Type != "username"))
                throw new DataValidationException("Username not found in claim!");

            string? username = user.Claims.FirstOrDefault(x => x.Type == "username")!.Value;


            return username ;

        }

        public DateTime GetCurrentNepalTime()
        {
            return DateTime.UtcNow.AddMinutes(345);
        }

        public  void CreateFolderIfNotExist(string folderPath)
        {
            bool exists = System.IO.Directory.Exists(folderPath);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
        }

        public DateTime ConvertEpochDateTime(long epochTime)
        {
            DateTimeOffset dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(epochTime);
            DateTime dateTime = dateTimeOffSet.DateTime.ToLocalTime();
            return dateTime;
        }

        public async Task<string> ReturnFileNameFromFile(IFormFile file, string uploadPath)
        {
            if (file is null)
            {
                throw new DataValidationException("Invalid File");
            }

            if (string.IsNullOrEmpty(uploadPath))
            {
                throw new DataValidationException("Invalid Path");
            }

            string? ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();
          //  string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string savedFileName = DateTime.UtcNow.AddMinutes(345).ToString("yyyyMMddHHmmssffff") + ext;
            string filePath = Path.Combine(uploadPath, savedFileName);
            await using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);
            return savedFileName;
            
        }

        public void DeleteFileFromServer(string imageUploadPath, string fileName)
        {
            if (System.IO.File.Exists($"{imageUploadPath}\\{fileName}"))
            {
                System.IO.File.Delete($"{imageUploadPath}\\{fileName}");
            }

        }

        public string ToLocalFormat(decimal value)
        {
            CultureInfo nepal = new CultureInfo("hi-Ne");
            string text = string.Format(nepal, "{0:#,0.00}", value); 
            return text;
        }

        public string AddName(string firstName, string? middleName, string lastName)
        {
            string middle = string.IsNullOrEmpty(middleName) ? string.Empty : middleName + " ";
            string fullName = string.Concat(firstName, " ", middle, lastName);
            return fullName;
        }
    }
}
