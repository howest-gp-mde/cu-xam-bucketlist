using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Mocking;

namespace XrnCourse.BucketList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly IAppSettingsService settingsService;
        private readonly IBucketsService bucketsService;

        public MainPage()
        {
            InitializeComponent();

            settingsService = new MockAppSettingsService();
            bucketsService = new MockBucketsService();
        }

        protected async override void OnAppearing()
        {
            await RefreshBucketLists();
            base.OnAppearing();
        }

        private async void BtnSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void BtnAddBucketList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BucketsPage());
        }

        private async void LvBucketLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //get the item on which we received a tap
            var bucket = e.Item as Bucket;
            if (bucket != null)
            {
                await DisplayAlert("Tap!", $"Congratulations!\nYou tapped {bucket.Title}", "Uh, ok..");
                await Navigation.PushAsync(new BucketsPage());
            }
        }
        private async void MnuBucketEdit_Clicked(object sender, EventArgs e)
        {
            var selectedBucket = ((MenuItem)sender).CommandParameter as Bucket;
            await DisplayAlert("Edit", $"Editing  {selectedBucket.Title}", "OK");
            await Navigation.PushAsync(new BucketsPage());
        }

        private async void MnuBucketDelete_Clicked(object sender, EventArgs e)
        {
            var selectedBucket = ((MenuItem)sender).CommandParameter as Bucket;
            await bucketsService.DeleteBucketList(selectedBucket.Id);
            await RefreshBucketLists();
        }

        private async Task RefreshBucketLists()
        {
            //get settings, because we need current user Id
            var settings = await settingsService.GetSettings();
            //get all bucket lists for this user
            var buckets = await bucketsService.GetBucketListsForUser(settings.CurrentUserId);
            //bind IEnumerable<Bucket> to the ListView's ItemSource
            lvBucketLists.ItemsSource = buckets;
        }
    }
}