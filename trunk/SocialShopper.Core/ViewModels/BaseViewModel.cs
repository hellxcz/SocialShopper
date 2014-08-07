// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading;
using Cirrious.MvvmCross.Views;
using SocialShopper.Core.ViewModels.Interface;

namespace SocialShopper.Core.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.CrossCore.Platform;
    using System.Collections.Generic;

    public abstract class InitMessage
    {
        
    }

    public abstract class BaseViewModel<TInitMessage> 
        : BaseViewModel, IVisible, IKillable
        where TInitMessage : InitMessage, new()
    {
        public BaseViewModel()
        {
        }
        
        public void Init(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                InitByMessage(default(TInitMessage));
                return;
            }

            var deserialized = Mvx.Resolve<IMvxJsonConverter>().DeserializeObject<TInitMessage>(parameter);
            InitByMessage(deserialized);
        }

        protected abstract void InitByMessage(TInitMessage parameter);
    }

    /// <summary>
    ///    Defines the BaseViewModel type.
    /// </summary>
    public abstract class BaseViewModel : MvxViewModel,
        IVisible, IKillable
    {

        public BaseViewModel()
        {
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>An instance of the service.</returns>
        public TService GetService<TService>() where TService : class
        {
            return Mvx.Resolve<TService>();
        }

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="backingStore">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="property">The property.</param>
        protected void SetProperty<T>(
            ref T backingStore,
            T value,
            Expression<Func<T>> property)
        {
            if (Equals(backingStore, value))
            {
                return;
            }

            backingStore = value;

            this.RaisePropertyChanged(property);
        }

        private const string ParameterName = "parameter";

        protected void ShowViewModel<TViewModel>(object parameter)
            where TViewModel : IMvxViewModel
        {
            var converter = Mvx.Resolve<IMvxJsonConverter>();
            
            var text = converter.SerializeObject(parameter);
            base.ShowViewModel<TViewModel>(new Dictionary<string, string>()
            {
                { ParameterName, text}
            });
        }
        
        public void IsVisible(bool isVisible)
        {
            if (SuppressVisibleEvent)
            {
                return;
            }

            if (isVisible)
            {
                OnShow();
            }
            else
            {
                OnHide();
            }
        }

        public virtual void OnShow()
        {
            
        }

        public virtual void OnHide()
        {
            
        }

        public virtual void OnKill()
        {
            
        }

        public void KillMe()
        {
            if (SuppressKillEvent)
            {
                return;
            }

            OnKill();
        }


        public bool SuppressVisibleEvent { get; set; }

        public bool SuppressKillEvent { get; set; }
    }
}
