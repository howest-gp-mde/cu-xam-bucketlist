using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Mocking;

namespace XrnCourse.BucketList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        IAppSettingsService settingsService;
        IUsersService usersService;

        public SettingsPage()
        {
            InitializeComponent();

            settingsService = new MockAppSettingsService();
            usersService = new MockUsersService();
        }


        protected async override void OnAppearing()
        {
            //get settings and intialize controls
            var settings = await settingsService.GetSettings();
            swEnableListSharing.On = settings.EnableListSharing;
            swEnableNotifications.On = settings.EnableNotifications;

            //get current User and intialize controls
            var currentUser = await usersService.GetUser(settings.CurrentUserId);
            txtUserName.Text = currentUser.UserName;
            txtEmail.Text = currentUser.Email;

            base.OnAppearing();
        }
        private async void BtnSaveSettings_Clicked(object sender, EventArgs e)
        {
            //save app settings
            var currentSettings = await settingsService.GetSettings();
            currentSettings.EnableListSharing = swEnableListSharing.On;
            currentSettings.EnableNotifications = swEnableNotifications.On;
            await settingsService.SaveSettings(currentSettings);

            //save user info settings
            var user = await usersService.GetUser(currentSettings.CurrentUserId);
            user.UserName = txtUserName.Text.Trim();
            user.Email = txtEmail.Text.Trim();
            await usersService.UpdateUser(user);

            //close settings page
            await Navigation.PopAsync();
        }

    }
}