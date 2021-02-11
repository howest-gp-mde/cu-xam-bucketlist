using FluentValidation;
using FreshMvvm;
using System.Windows.Input;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Models;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Validators;

namespace XrnCourse.BucketList.ViewModels
{
    public class SettingsViewModel : FreshBasePageModel
    {
        private readonly IAppSettingsService settingsService;
        private readonly IUsersService usersService;

        private IValidator userValidator;

        public SettingsViewModel(
            IAppSettingsService settingsService,
            IUsersService usersService)
        {
            this.settingsService = settingsService;
            this.usersService = usersService;

            userValidator = new UserValidator();
        }

        #region Properties

        private string username;
        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        private string errorUserName;
        public string ErrorUserName
        {
            get { return errorUserName; }
            set { 
                errorUserName = value;
                RaisePropertyChanged(nameof(ErrorUserName));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        private string errorEmail;
        public string ErrorEmail
        {
            get { return errorEmail; }
            set {
                errorEmail = value;
                RaisePropertyChanged(nameof(ErrorEmail));
            }
        }

        private bool enableListSharing;
        public bool EnableListSharing
        {
            get { return enableListSharing; }
            set
            {
                enableListSharing = value;
                RaisePropertyChanged(nameof(EnableListSharing));
            }
        }

        private bool enableNotifications;
        public bool EnableNotifications
        {
            get { return enableNotifications; }
            set
            {
                enableNotifications = value;
                RaisePropertyChanged(nameof(EnableNotifications));
            }
        }

        #endregion

        /// <summary>
        /// Callled whenever the page is navigated to.
        /// </summary>
        /// <param name="initData"></param>
        public async override void Init(object initData)
        {
            base.Init(initData);

            //get settings and intialize controls
            var settings = await settingsService.GetSettings();
            EnableListSharing = settings.EnableListSharing;
            EnableNotifications = settings.EnableNotifications;

            //get current User and intialize controls
            var currentUser = await usersService.GetUser(settings.CurrentUserId);
            UserName = currentUser.UserName;
            Email = currentUser.Email;
        }

        public ICommand SaveSettingsCommand => new Command(
            async () => {
                //save app settings
                var currentSettings = await settingsService.GetSettings();
                currentSettings.EnableListSharing = EnableListSharing;
                currentSettings.EnableNotifications = EnableNotifications;
                await settingsService.SaveSettings(currentSettings);

                //save user info settings
                var user = await usersService.GetUser(currentSettings.CurrentUserId);
                user.UserName = UserName?.Trim();
                user.Email = Email?.Trim();

                if (Validate(user))
                {
                    await usersService.UpdateUser(user);

                    //use coremethodes to Pop pages in FreshMvvm!
                    await CoreMethods.PopPageModel(false, true);
                }
            }
        );

        /// <summary>
        /// Validates the current values of a user object
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns></returns>
        private bool Validate(User user)
        {
            ErrorUserName = "";
            ErrorEmail = "";

            var validationContext = new ValidationContext<User>(user);
            var validationResult = userValidator.Validate(validationContext);
            //loop through error to identify properties
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(user.UserName))
                {
                    ErrorUserName = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(user.Email))
                {
                    ErrorEmail = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }
    }
}
