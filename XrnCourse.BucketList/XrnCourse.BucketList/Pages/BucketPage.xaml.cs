using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BucketPage : ContentPage
    {
        public BucketPage(Bucket bucket)
        {
            InitializeComponent();
        }
    }
}
