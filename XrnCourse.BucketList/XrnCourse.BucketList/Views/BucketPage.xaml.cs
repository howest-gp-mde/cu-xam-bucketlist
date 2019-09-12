using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Mocking;

namespace XrnCourse.BucketList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BucketPage : ContentPage
    {
        private readonly IBucketsService bucketsService;
        private readonly IAppSettingsService settingsService;
        private AppSettings settings;
        private Bucket currentBucket;
        private bool isNew = true;

        public BucketPage(Bucket bucket)
        {
            InitializeComponent();

            settingsService = new MockAppSettingsService();
            bucketsService = new MockBucketsService();

            if (bucket == null)
            {
                currentBucket = new Bucket();
                Title = "New Bucket List";
            }
            else
            {
                isNew = false;
                currentBucket = bucket;
                Title = currentBucket.Title;
            }
        }

        protected async override void OnAppearing()
        {
            settings = await settingsService.GetSettings();
            LoadBucketState();  //fill controls with currentBucket properties
            base.OnAppearing();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            SaveBucketState();
            if (isNew)
            {
                currentBucket.Id = Guid.NewGuid(); // generate ID for this bucket
                await bucketsService.AddBucketList(currentBucket);
            }
            else
            {
                await bucketsService.UpdateBucketList(currentBucket);
            }
            await DisplayAlert("Saved", $"Your bucket list {currentBucket.Title} has been saved", "Ok");
        }

        /// <summary>
        /// Load values from currentBucket into controls
        /// </summary>
        private void LoadBucketState()
        {
            txtTitle.Text = currentBucket.Title;
            txtDescription.Text = currentBucket.Description;
            swIsFavorite.IsToggled = currentBucket.IsFavorite;
            lblPercentComplete.Text = currentBucket.PercentCompleted.ToString("P0");
        }

        /// <summary>
        /// Retrieve values from controls and store them in currentBucket
        /// </summary>
        private void SaveBucketState()
        {
            currentBucket.Title = txtTitle.Text;
            currentBucket.Description = txtDescription.Text;
            currentBucket.IsFavorite = swIsFavorite.IsToggled;
            currentBucket.OwnerId = settings.CurrentUserId;
        }
    }
}
