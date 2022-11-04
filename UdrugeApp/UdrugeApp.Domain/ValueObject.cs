using BaseLibrary;

namespace UdrugeApp.Domain
{
    /// <summary>
    /// Value object base
    /// </summary>
    public abstract class ValueObject
    {
        public abstract Result IsValid();
        public abstract override bool Equals(object? other);
        public abstract override int GetHashCode();
    }
}