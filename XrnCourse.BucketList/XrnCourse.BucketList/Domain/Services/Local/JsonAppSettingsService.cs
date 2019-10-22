using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Local
{
    public class JsonAppSettingsService : IAppSettingsService
    {
        private readonly string _filePath;

        public JsonAppSettingsService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "appsettings.json");
        }

        public async Task<AppSettings> GetSettings()
        {
            try
            {
                string settingsJson = File.ReadAllText(_filePath);
                AppSettings settings = JsonConvert.DeserializeObject<AppSettings>(settingsJson);
                return await Task.FromResult(settings);  //using Task.FromResult to have atleast one await in this async method
            }
            catch  //return null on file not found, deserialization error, ...
            {
                return null;
            }
        }

        public async Task<bool> SaveSettings(AppSettings settings)
        {
            try
            {
                string settingsJson = JsonConvert.SerializeObject(settings);
                File.WriteAllText(_filePath, settingsJson);
                return await Task.FromResult(true);
            }
            catch
            {
                //todo: log error!
                return await Task.FromResult(false);
            }
        }
    }
}
