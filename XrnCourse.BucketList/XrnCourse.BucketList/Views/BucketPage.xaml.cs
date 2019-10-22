using FluentValidation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Local;
using XrnCourse.BucketList.Domain.Validators;

namespace XrnCourse.BucketList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BucketPage : ContentPage
    {
        private readonly IBucketsService bucketsService;
        private readonly IAppSettingsService settingsService;
        private IValidator bucketValidator;
        private AppSettings settings;
        private Bucket currentBucket;
        private bool isNew = true;
        
        public BucketPage(Bucket bucket)
        {
            InitializeComponent();

            settingsService = new JsonAppSettingsService();
            bucketsService = new JsonBucketsService();

            bucketValidator = new BucketValidator();

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
            busyIndicator.IsVisible = true;
            SaveBucketState();
            if (Validate(currentBucket))
            {
                if (isNew)
                {
                    currentBucket.Id = Guid.NewGuid(); // generate ID for this bucket
                    await bucketsService.AddBucketList(currentBucket);
                }
                else
                {
                    await bucketsService.UpdateBucketList(currentBucket);
                }
                await Navigation.PopAsync();
            }
            busyIndicator.IsVisible = false;
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

        /// <summary>
        /// Validates the current values of a bucket object
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns></returns>
        private bool Validate(Bucket bucket)
        {
            lblErrorDescription.IsVisible = false;
            lblErrorTitle.IsVisible = false;

            var validationResult = bucketValidator.Validate(bucket);
            //loop through error to identify properties
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(bucket.Title))
                {
                    lblErrorTitle.Text = error.ErrorMessage;
                    lblErrorTitle.IsVisible = true;
                }
                if (error.PropertyName == nameof(bucket.Description))
                {
                    lblErrorDescription.Text = error.ErrorMessage;
                    lblErrorDescription.IsVisible = true;
                }
            }
            return validationResult.IsValid;
        }

    }
}
