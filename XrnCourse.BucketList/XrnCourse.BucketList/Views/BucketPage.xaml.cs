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
            base.OnAppearing();
        }

    }
}