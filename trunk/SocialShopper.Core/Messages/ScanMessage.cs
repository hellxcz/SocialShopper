using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Plugins.Messenger;
using SocialShopper.Core.Entities;

namespace SocialShopper.Core.Messages
{
    public class ScanMessage : MvxMessage
    {
        public string Code { get; private set; }

        public ScanMessage(object sender, string code) 
            : base(sender)
        {
            Code = code;
        }
    }
}
