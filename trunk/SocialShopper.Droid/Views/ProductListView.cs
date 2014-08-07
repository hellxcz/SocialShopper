using Android.App;
using Android.OS;

namespace SocialShopper.Droid.Views
{
    [Activity]
    public class ProductListView : BaseView
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.ProductListView);
        }
    }
}