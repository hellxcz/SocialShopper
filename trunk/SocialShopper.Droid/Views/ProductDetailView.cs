using Acr.MvvmCross.Plugins.BarCodeScanner;
using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Plugins.Messenger;
using SocialShopper.Core.Messages;

namespace SocialShopper.Droid.Views
{
    [Activity(Label = "View for Product detail")]
    public class ProductDetailView : BaseView
    {
        public ProductDetailView()
        {
        }

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.ProductDetailView);

            BarCodeScanner.Context = this;
        }
    }
}