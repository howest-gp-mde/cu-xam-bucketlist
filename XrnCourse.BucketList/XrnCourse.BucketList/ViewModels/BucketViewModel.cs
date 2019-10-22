using FluentValidation;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Local;
using XrnCourse.BucketList.Domain.Validators;

namespace XrnCourse.BucketList.ViewModels
{
    public class BucketViewModel : FreshBasePageModel
    {
        private readonly IBucketsService bucketsService;
        private readonly IAppSettingsService settingsService;
        private IValidator bucketValidator;

        private AppSettings settings;
        private Bucket currentBucket;
        private bool isNew = true;

        public BucketViewModel()
        {
            settingsService = new JsonAppSettingsService();
            bucketsService = new JsonBucketsService();
            bucketValidator = new BucketValidator();
        }

        #region Properties

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                RaisePropertyChanged(nameof(pageTitle));
            }
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

        private string bucketTitle;
        public string BucketTitle
        {
            get { return bucketTitle; }
            set
            {
                bucketTitle = value;
                RaisePropertyChanged(nameof(BucketTitle));
            }
        }

        private string bucketTitleError;
        public string BucketTitleError
        {
            get { return bucketTitleError; }
            set
            {
                bucketTitleError = value;
                RaisePropertyChanged(nameof(BucketTitleError));
                RaisePropertyChanged(nameof(BucketTitleErrorVisible));
            }
        }

        public bool BucketTitleErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(BucketTitleError); }
        }

        private string bucketDescription;
        public string BucketDescription
        {
            get { return bucketDescription; }
            set
            {
                bucketDescription = value;
                RaisePropertyChanged(nameof(BucketDescription));
            }
        }

        private string bucketDescriptionError;
        public string BucketDescriptionError
        {
            get { return bucketDescriptionError; }
            set
            {
                bucketDescriptionError = value;
                RaisePropertyChanged(nameof(BucketDescriptionError));
                RaisePropertyChanged(nameof(BucketDescriptionErrorVisible));
            }
        }

        public bool BucketDescriptionErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(BucketDescriptionError); }
        }

        private bool bucketIsFavorite;
        public bool BucketIsFavorite
        {
            get { return bucketIsFavorite; }
            set
            {
                bucketIsFavorite = value;
                RaisePropertyChanged(nameof(BucketIsFavorite));
            }
        }

        private string bucketPercentComplete;
        public string BucketPercentComplete
        {
            get { return bucketPercentComplete; }
            set
            {
                bucketPercentComplete = value;
                RaisePropertyChanged(nameof(BucketPercentComplete));
            }
        }

        private ObservableCollection<BucketItem> bucketitems;
        public ObservableCollection<BucketItem> BucketItems
        {
            get { return bucketitems; }
            set
            {
                bucketitems = value;
                RaisePropertyChanged(nameof(BucketItems));
            }
        }
        #endregion

        /// <summary>
        /// Called whenever the page is navigated to.
        /// </summary>
        /// <param name="initData"></param>
        public async override void Init(object initData)
        {
            base.Init(initData);

            currentBucket = initData as Bucket;

            settings = await settingsService.GetSettings();

            await RefreshBucket();
        }

        /// <summary>
        /// Called when returning to this Model from a previous model
        /// </summary>
        /// <param name="returnedData"></param>
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData is BucketItem)
            {
                //refresh list, to update this item visually
                LoadBucketState();
            }
        }

        /// <summary>
        /// Refreshes the currentBucket (to edit) or initializes a new one (to add)
        /// </summary>
        /// <returns></returns>
        private async Task RefreshBucket()
        {
            if (currentBucket != null)
            {
                //editing existing bucketlist
                isNew = false;
                PageTitle = "Edit Bucket List";
                currentBucket = await bucketsService.GetBucketList(currentBucket.Id);
            }
            else
            {
                //editing brand new bucketlist
                isNew = true;
                PageTitle = "New Bucket List";
                currentBucket = new Bucket();
                currentBucket.Id = Guid.NewGuid();
                currentBucket.OwnerId = settings.CurrentUserId;
                currentBucket.Items = new List<BucketItem>();
            }
            LoadBucketState();
        }


        public ICommand SaveBucketCommand => new Command(
            async () => {
                SaveBucketState();

                if (Validate(currentBucket))
                {
                    IsBusy = true;

                    if (isNew)
                    {
                        await bucketsService.AddBucketList(currentBucket);
                    }
                    else
                    {
                        await bucketsService.UpdateBucketList(currentBucket);
                    }
                    IsBusy = false;

                    MessagingCenter.Send(this,
                        Constants.MessageNames.BucketSaved, currentBucket);

                    await CoreMethods.PopPageModel(false, true);
                }
            }
        );

        public ICommand OpenItemPageCommand => new Command<BucketItem>(
            async (BucketItem item) => {

                SaveBucketState();

                if (item == null)
                {
                    //new BucketList Item requested, let's make sure to
                    //pass a reference to the parent Bucket to which the new item will belong
                    item = new BucketItem
                    {
                        BucketId = currentBucket.Id,
                        ParentBucket = currentBucket
                    };
                }
                await CoreMethods.PushPageModel<BucketItemViewModel>(item, false, true);
            }
        );

        public ICommand DeleteItemCommand => new Command<BucketItem>(
            (BucketItem item) => {
                currentBucket.Items.Remove(item);
                LoadBucketState();
            }
        );


        /// <summary>
        /// Loads the currentBucket list properties into the VM properties for display in UI
        /// </summary>
        private void LoadBucketState()
        {
            BucketTitle = currentBucket.Title;
            BucketDescription = currentBucket.Description;
            BucketIsFavorite = currentBucket.IsFavorite;
            BucketPercentComplete = currentBucket.PercentCompleted.ToString("P0");
            BucketItems = new ObservableCollection<BucketItem>(currentBucket.Items);
        }

        /// <summary>
        /// Saves the VM properties back to the current bucket
        /// </summary>
        private void SaveBucketState()
        {
            currentBucket.Title = BucketTitle;
            currentBucket.Description = BucketDescription;
            currentBucket.IsFavorite = BucketIsFavorite;
            currentBucket.OwnerId = settings.CurrentUserId;
        }

        /// <summary>
        /// Validates the bucket using the validator
        /// </summary>
        /// <param name="bucket">The bucket to validate</param>
        /// <returns></returns>
        private bool Validate(Bucket bucket)
        {
            BucketTitleError = "";
            BucketDescriptionError = "";


            var validationContext = new ValidationContext<Bucket>(bucket);
            var validationResult = bucketValidator.Validate(validationContext);
            //loop through error to identify properties
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(bucket.Title))
                {
                    BucketTitleError = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(bucket.Description))
                {
                    BucketDescriptionError = error.ErrorMessage;
                }
            }

            return validationResult.IsValid;
        }
    }
}
