using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cirrious.CrossCore.Converters;
using System.Collections;
using SocialShopper.Core.Entities;
using SocialShopper.Core.Entities.Interface;

namespace SocialShopper.Core.Converters
{
    public class FirstItemValueListValueConverter : MvxValueConverter<IList, string>
    {
        protected override string Convert(IList list, Type targetType, object parameter, CultureInfo culture)
        {
            if (list.Count < 1)
            {
                return "";
            }

            var firstValue = list[0];

            var asIHaveValue = firstValue as IHaveStringValue;

            if (asIHaveValue != null)
            {
                return asIHaveValue.Value;
            }

            return asIHaveValue.ToString();
        }
    }
}
