using System.Security;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using SocialShopper.Core.Messages;
using Cirrious.MvvmCross.FieldBinding;

using SocialShopper.Core.Services.Interface;


namespace SocialShopper.Core.ViewModels
{
    public class ScanViewModel 
		: MvxViewModel
    {
        private readonly IMvxMessenger _mvxMessenger;
        private MvxSubscriptionToken _scanSubscriptionTokenToken;

        public ScanViewModel(IMvxMessenger mvxMessenger)
        {
            _mvxMessenger = mvxMessenger;

            _scanSubscriptionTokenToken = 
                _mvxMessenger.Subscribe<ScanMessage>(OnScan);
        }

        private void OnScan(ScanMessage message)
        {
            Code.Value = message.Code;
        }

        public readonly INC<string> Code = new NC<string>();
    }
}
