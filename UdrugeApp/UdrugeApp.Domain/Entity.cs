using BaseLibrary;

namespace UdrugeApp.Domain
{
    /// <summary>
    /// Domain entity base
    /// </summary>
    /// <typeparam name="TPrimKey"></typeparam>
    public abstract class Entity<TPrimKey>
    {
        protected readonly TPrimKey _id;

        protected Entity(TPrimKey id)
        {
            _id = id;
        }

        public TPrimKey Id => _id;

        public abstract Result IsValid();
        public abstract override bool Equals(object? other);
        public abstract override int GetHashCode();
    }
}