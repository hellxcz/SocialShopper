namespace SocialShopper.Core.ViewModels.Interface
{
    public interface IKillable
    {
        void KillMe();
        bool SuppressKillEvent { get; set; }
    }
}