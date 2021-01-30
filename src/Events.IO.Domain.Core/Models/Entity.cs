using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text; 

namespace Events.IO.Domain.Core.Models
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        protected Entity()
        {
            ValidationResult = new ValidationResult(); 
        }

        public Guid Id { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool IsValid();

        #region Compare not basead only on the type but on the ID value too
        // if they are the same instance or they have the same ID
        // important to make sure that the entity is the same including the ID
        // but it is not necessary if you don't need this deep kind of validation
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]";
        }
        #endregion
    }
}
