using System;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services.Mocking
{
    /// <summary>
    /// Fake service which holds data in-memory
    /// </summary>
    public class MockAppSettingsService : IAppSettingsService
    {
        // holds current settings in memory for testing purproses
        private static AppSettings currentSettings = new AppSettings
        {
            CurrentUserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            EnableListSharing = true,
            EnableNotifications = false
        };

        public async Task<AppSettings> GetSettings()
        {
            return await Task.FromResult(currentSettings); //ensures async result
        }
        public async Task<bool> SaveSettings(AppSettings settings)
        {
            currentSettings = settings;
            return await Task.FromResult(true); //ensures async result
        }
    }
}
