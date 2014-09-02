// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SocialShopper.Core.ViewModels;
using SocialShopper.Core.ViewModels.Interface;
using System;

namespace SocialShopper.Droid.Views
{
    using Cirrious.MvvmCross.Droid.Views;

    /// <summary>
    ///    Defines the BaseView type.
    /// </summary>
    public abstract class BaseView : MvxActivity
    {
        protected IVisible VisibleViewModel
        {
            get { return base.ViewModel as IVisible; }
        }

        protected IKillable KillableViewModel
        {
            get { return base.ViewModel as IKillable; }
        }

		protected override void OnCreate (Android.OS.Bundle bundle)
		{
			base.OnCreate (bundle);
		}

        protected override void OnResume()
        {
            base.OnResume();
            if (VisibleViewModel == null)
            {
                return;
            }

            VisibleViewModel.IsVisible(true);
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (VisibleViewModel == null)
            {
                return;
            }

            VisibleViewModel.IsVisible(false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (KillableViewModel == null)
            {
                return;
            }

            KillableViewModel.KillMe();
        }
    }
}