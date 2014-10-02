using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using Cirrious.MvvmCross.FieldBinding;

namespace  SocialShopper.Core.Core
{
	public interface IMyNCList<TValue> : INCList<TValue>
	{
		void Add(TValue value);
	}

	public class MyNCList<TValue> : NCList<TValue>, IMyNCList<TValue>
	{
		public void Add(TValue value)
		{
			this.Value.Add (value);

			this.RaiseChanged ();
		}
	}
}