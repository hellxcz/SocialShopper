using Android.App;
using Android.OS;

namespace SocialShopper.Droid.Views
{
    [Activity]
    public class ProductSearchView : BaseView
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.ProductSearchView);
        }
    }
}