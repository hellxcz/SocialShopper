namespace SocialShopper.Core.ViewModels.Interface
{
    public interface IVisible
    {
        void IsVisible(bool isVisible);
        bool SuppressVisibleEvent { get; set; }
    }
}