// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ScanView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Android.Provider;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;
using SocialShopper.Core.Messages;
using SocialShopper.Core.Services.Interface;
using SocialShopper.Core.ViewModels;
using ZXing.Mobile;

namespace SocialShopper.Droid.Views
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using SocialShopper.Core.Services;
    using Android.Views;

    /// <summary>
    /// Defines the ScanView type.
    /// </summary>
 //   [Activity(Label = "View for ScanView")]
    [Activity]
    public class ScanView : BaseView
    {
        private readonly IScanPublisherService _scanPublisherService;

        MobileBarcodeScanner scanner;
        Button buttonScanDefaultView;

        public ScanView()
        {
            _scanPublisherService = Mvx.Resolve<IScanPublisherService>();

            scanner = new MobileBarcodeScanner(this);
        }
        
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.ScanView);

            //ScanHelper.ShowScan(scanner,  "Hold the camera up to the barcode\nAbout 6 inches away");

            buttonScanDefaultView = this.FindViewById<Button>(Resource.Id.buttonScanDefaultView);
            
            buttonScanDefaultView.Click += async delegate
            {
                //Tell our scanner to use the default overlay
                scanner.UseCustomOverlay = false;
                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                //scanner.BottomText = "Wait for the barcode to automatically scan!";

                //Start scanning
                var result = await scanner.Scan();

                if (result != null && !string.IsNullOrEmpty(result.Text))
                {
                    _scanPublisherService.Publish(this, result.Text);
                }
            };
        }
    }

    //public static class ScanHelper
    //{
    //    public static void ShowScan(View viewToClick, string topText)
    //    {
            
    //    }

    //    internal static LambdaExpression ShowScan(MobileBarcodeScanner scanner, string topText)
    //    {
    //        return
    //            async () =>
    //            {
    //                var scanPublisherService = Mvx.Resolve<IScanPublisherService>();

    //                //Tell our scanner to use the default overlay
    //                scanner.UseCustomOverlay = false;
    //                scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
    //                //scanner.BottomText = "Wait for the barcode to automatically scan!";

    //                //Start scanning
    //                var result = await scanner.Scan();

    //                if (result != null && !string.IsNullOrEmpty(result.Text))
    //                {
    //                    scanPublisherService.Publish(null, result.Text);
    //                }
    //            };
    //    }
    //}
}