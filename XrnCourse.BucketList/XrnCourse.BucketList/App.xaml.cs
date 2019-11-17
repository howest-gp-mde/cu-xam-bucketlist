using FreshMvvm;
using System.Net;
using Xamarin.Forms;
using XrnCourse.BucketList.Domain.Services;
using XrnCourse.BucketList.Domain.Services.Api;
using XrnCourse.BucketList.Domain.Services.Local;
using XrnCourse.BucketList.ViewModels;

namespace XrnCourse.BucketList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => 
                true;


            //register dependencies
            FreshIOC.Container.Register<IAppSettingsService>(new JsonAppSettingsService());
            FreshIOC.Container.Register<IUsersService>(new JsonUsersService());
            FreshIOC.Container.Register<IBucketsService>(new ApiBucketsService());

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
