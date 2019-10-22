using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Local;

namespace XrnCourse.BucketList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly IUsersService usersService;
        private readonly IAppSettingsService settingsService;
        private readonly IBucketsService bucketsService;

        public MainPage()
        {
            InitializeComponent();

            usersService = new JsonUsersService();
            settingsService = new JsonAppSettingsService();
            bucketsService = new JsonBucketsService();
        }

        protected async override void OnAppearing()
        {
            await EnsureUserAndSettings();

            await RefreshBucketLists();
            base.OnAppearing();
        }

        protected async Task EnsureUserAndSettings()
        {
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
        }

        private async void BtnSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void BtnAddBucketList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BucketPage(null)); //null means we're adding a new bucketlist
        }

        private async void LvBucketLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //get the item on which we received a tap
            var bucket = e.Item as Bucket;
            if (bucket != null)
            {
                await Navigation.PushAsync(new BucketPage(bucket));
            }
        }

        private async void MnuBucketEdit_Clicked(object sender, EventArgs e)
        {
            var selectedBucket = ((MenuItem)sender).CommandParameter as Bucket;
            await Navigation.PushAsync(new BucketPage(selectedBucket));
        }

        private async void MnuBucketDelete_Clicked(object sender, EventArgs e)
        {
            var selectedBucket = ((MenuItem)sender).CommandParameter as Bucket;
            await bucketsService.DeleteBucketList(selectedBucket.Id);
            await RefreshBucketLists();
        }

        private async Task RefreshBucketLists()
        {
            busyIndicator.IsVisible = true;

            //get settings, because we need current user Id
            var settings = await settingsService.GetSettings();
            //get all bucket lists for this user
            var buckets = await bucketsService.GetBucketListsForUser(settings.CurrentUserId);
            //bind IEnumerable<Bucket> to the ListView's ItemSource
            lvBucketLists.ItemsSource = buckets;

            busyIndicator.IsVisible = false;
        }
    }
}