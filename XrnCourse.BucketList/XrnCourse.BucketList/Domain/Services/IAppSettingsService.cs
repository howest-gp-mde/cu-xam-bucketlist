using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Services
{
    /// <summary>
    /// Provides functionalities to manage <see cref="AppSettings"/> instances in the domain
    /// </summary>
    public interface IAppSettingsService
    {
        /// <summary>
        /// Retrieves the current AppSettings from the data store
        /// </summary>
        /// <returns>a AppSettings instance or <see langword="null"/> if it doesn't exist</returns>
        Task<AppSettings> GetSettings();

        /// <summary>
        /// Persists the supplied AppSettings to the data store
        /// <para>If there are no current settings they are created, otherwise they are updated</para>
        /// </summary>
        /// <returns>true on success</returns>
        Task<bool> SaveSettings(AppSettings settings);
    }
}
