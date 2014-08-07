using Cirrious.MvvmCross.Plugins.Messenger;
using SocialShopper.Core.Messages;
using SocialShopper.Core.Services.Interface;

namespace SocialShopper.Core.Services
{
    public class ScanPublisherService : IScanPublisherService
    {
        private readonly IMvxMessenger _messenger;

        public ScanPublisherService(IMvxMessenger _messenger)
        {
            this._messenger = _messenger;
        }

        public void Publish(object sender, string code)
        {
            _messenger.Publish(new ScanMessage(sender, code));
        }
    }
}