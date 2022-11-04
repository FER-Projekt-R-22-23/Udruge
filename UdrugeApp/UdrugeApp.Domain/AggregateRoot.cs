namespace UdrugeApp.Domain
{
    /// <summary>
    /// Domain aggregate root base
    /// </summary>
    /// <typeparam name="TPrimKey"></typeparam>
    public abstract class AggregateRoot<TPrimKey> : Entity<TPrimKey>
    {

        protected AggregateRoot(TPrimKey id) : base(id)
        {
        }
    }
}