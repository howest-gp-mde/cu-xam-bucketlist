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
    public partial class SettingsPage : ContentPage
    {
        IAppSettingsService settingsService;
        IUsersService usersService;
        IValidator userValidator;

        public SettingsPage()
        {
            InitializeComponent();

            settingsService = new JsonAppSettingsService();
            usersService = new JsonUsersService();
            userValidator = new UserValidator();
        }


        protected async override void OnAppearing()
        {
            //get settings and intialize controls
            var settings = await settingsService.GetSettings();
            swEnableListSharing.On = settings.EnableListSharing;
            swEnableNotifications.On = settings.EnableNotifications;

            //get current User and intialize controls
            var currentUser = await usersService.GetUser(settings.CurrentUserId);
            txtUserName.Text = currentUser?.UserName;
            txtEmail.Text = currentUser?.Email;

            base.OnAppearing();
        }
        private async void BtnSaveSettings_Clicked(object sender, EventArgs e)
        {
            //save app settings
            var currentSettings = await settingsService.GetSettings();
            currentSettings.EnableListSharing = swEnableListSharing.On;
            currentSettings.EnableNotifications = swEnableNotifications.On;
            await settingsService.SaveSettings(currentSettings);

            var user = await usersService.GetUser(currentSettings.CurrentUserId) ?? new User();

            //save user info settings
            user.UserName = txtUserName.Text.Trim();
            user.Email = txtEmail.Text.Trim();

            if (Validate(user))
            {
                await usersService.UpdateUser(user);

                //close settings page
                await Navigation.PopAsync();
            }
        }

        /// <summary>
        /// Validates the current values of a user object
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns></returns>
        private bool Validate(User user)
        {
            errorEmail.Text = "";
            errorUserName.Text = "";

            var validationContext = new ValidationContext<User>(user);
            var validationResult = userValidator.Validate(validationContext);
            //loop through error to identify properties
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(user.UserName))
                {
                    errorUserName.Text = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(user.Email))
                {
                    errorEmail.Text = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }
    }
}