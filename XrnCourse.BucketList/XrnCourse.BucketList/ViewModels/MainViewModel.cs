using FreshMvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Local;

namespace XrnCourse.BucketList.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        private readonly IUsersService usersService;
        private readonly IAppSettingsService settingsService;
        private readonly IBucketsService bucketsService;

        public MainViewModel()
        {
            usersService = new JsonUsersService();
            settingsService = new JsonAppSettingsService();
            bucketsService = new JsonBucketsService();
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        private ObservableCollection<Bucket> buckets;
        public ObservableCollection<Bucket> Buckets
        {
            get { return buckets; }
            set
            {
                buckets = value;
                RaisePropertyChanged(nameof(Buckets));
            }
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await EnsureUserAndSettings();

            await RefreshBucketLists();
        }

        public ICommand OpenSettingsPageCommand => new Command(
           async () => {
               await CoreMethods.PushPageModel<SettingsViewModel>(true);
           }
        );

        public ICommand OpenBucketPageCommand => new Command<Bucket>(
            async (Bucket bucket) => {
                await CoreMethods.PushPageModel<BucketViewModel>(bucket, false, true);
            }
        );

        public ICommand DeleteBucketCommand => new Command<Bucket>(
            async (Bucket bucket) => {
                await bucketsService.DeleteBucketList(bucket.Id);
                await RefreshBucketLists();
            }
        );

        private async Task RefreshBucketLists()
        {
            IsBusy = true;
            //get settings, because we need current user Id
            var settings = await settingsService.GetSettings();
            //get all bucket lists for this user
            var buckets = await bucketsService.GetBucketListsForUser(settings.CurrentUserId);
            //bind IEnumerable<Bucket> to the ListView's ItemSource
            Buckets = null;    //Important! ensure the list is empty first to force refresh!
            Buckets = new ObservableCollection<Bucket>(buckets.OrderBy(e => e.Title));
            IsBusy = false;
        }

        private async Task EnsureUserAndSettings()
        {
            IsBusy = true;
            var settings = await settingsService.GetSettings();
            if (settings == null)
            {
                //create new user
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Guest",
                    Email = ""
                };
                await usersService.CreateUser(newUser);

                //create new settings
                var newSettings = new AppSettings
                {
                    CurrentUserId = newUser.Id,
                    EnableListSharing = false,
                    EnableNotifications = false,
                };
                await settingsService.SaveSettings(newSettings);
            }
            IsBusy = false;
        }
    }
}
