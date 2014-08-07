namespace SocialShopper.Core.Entities.Interface
{
    public interface IHaveId<T>
    {
        T Id { get; set; }
    }
}