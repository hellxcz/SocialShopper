using Cirrious.MvvmCross.Plugins.Messenger;

namespace SocialShopper.Core.Messages
{
    public enum EntityChangeEnum
    {
        Update,
        Insert,
        Delete
    }
    
    public class EntityMessage<T> : MvxMessage
        where T : class
    {
        public T Entity{ get; private set; }
        public EntityChangeEnum EntityChange { get; private set; }

        public EntityMessage(object sender, T entity, EntityChangeEnum entityChange) 
            : base(sender)
        {
            Entity = entity;
            EntityChange = entityChange;
        }
    }
}