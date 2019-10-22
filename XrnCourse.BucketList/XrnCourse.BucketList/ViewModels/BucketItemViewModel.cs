using FluentValidation;
using FreshMvvm;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Mocking;
using XrnCourse.BucketList.Domain.Validators;

namespace XrnCourse.BucketList.ViewModels
{
    public class BucketItemViewModel : FreshBasePageModel
    {
        private BucketItem currentItem;
        private IValidator bucketitemValidator;
        private IBucketsService bucketService;

        public BucketItemViewModel()
        {
            this.bucketService = new MockBucketsService();
            bucketitemValidator = new BucketItemValidator();
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

        private string itemDescription;
        public string ItemDescription
        {
            get { return itemDescription; }
            set
            {
                itemDescription = value;
                RaisePropertyChanged(nameof(ItemDescription));
            }
        }

        private string itemDescriptionError;
        public string ItemDescriptionError
        {
            get { return itemDescriptionError; }
            set
            {
                itemDescriptionError = value;
                RaisePropertyChanged(nameof(ItemDescriptionError));
                RaisePropertyChanged(nameof(ItemDescriptionErrorVisible));
            }
        }

        public bool ItemDescriptionErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(ItemDescriptionError); }
        }

        private bool itemIsComplete;
        public bool ItemIsComplete
        {
            get { return itemIsComplete; }
            set
            {
                itemIsComplete = value;
                RaisePropertyChanged(nameof(ItemIsComplete));
            }
        }

        private DateTime itemCompletionDate;
        public DateTime ItemCompletionDate
        {
            get { return itemCompletionDate; }
            set
            {
                itemCompletionDate = value;
                RaisePropertyChanged(nameof(itemCompletionDate));
            }
        }

        #endregion

        public override void Init(object initData)
        {
            BucketItem item = initData as BucketItem;
            currentItem = item;
            if (item.Id == Guid.Empty)
            {
                PageTitle = "New Item";
            }
            else
            {
                PageTitle = "Edit Item";
            }

            LoadItemState();
            base.Init(initData);
        }

        private void LoadItemState()
        {
            ItemDescription = currentItem.ItemDescription;
            ItemIsComplete = currentItem.CompletionDate.HasValue;
            ItemCompletionDate = currentItem.CompletionDate ?? DateTime.Now;
        }

        private void SaveItemState()
        {
            currentItem.ItemDescription = ItemDescription;
            currentItem.CompletionDate = ItemIsComplete ? new DateTime?(ItemCompletionDate) : null;
        }

        public ICommand SaveBucketItemCommand => new Command(
            async () => {
                try
                {
                    SaveItemState();

                    if (Validate(currentItem))
                    {
                        if (currentItem.Id == Guid.Empty)
                        {
                            currentItem.Id = Guid.NewGuid();
                            currentItem.ParentBucket.Items.Add(currentItem);
                        }
                        //use coremethodes to Pop pages in FreshMvvm!
                        await CoreMethods.PopPageModel(currentItem, false, true);
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        );

        private bool Validate(BucketItem item)
        {
            var validationResult = bucketitemValidator.Validate(item);
            //loop through error to identify properties
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(item.ItemDescription))
                {
                    ItemDescriptionError = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }
    }
}
