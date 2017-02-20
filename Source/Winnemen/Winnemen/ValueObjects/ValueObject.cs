namespace Winnemen.ValueObjects
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        protected virtual bool EqualsCore(T other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
                return hashCode;
            }
        
        }
        
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }


        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
