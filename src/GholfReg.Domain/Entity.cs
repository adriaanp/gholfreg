using System;

namespace GholfReg.Domain
{
    public abstract class Entity
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            var compareObject = obj as Entity;

            return this.Equals(compareObject);
        }

        public bool Equals(Entity obj)
        {
            if (obj == null)
                return false;

            return Id.Equals(obj.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity e1, Entity e2)
        {
            if (Object.ReferenceEquals(e1, e2))
            {
                return true;
            }
            if ((object)e1 == null || (object)e2 == null)
            {
                return false;
            }

            return e1.Equals(e2);
        }

        public static bool operator !=(Entity e1, Entity e2)
        {
            return !(e1 == e2);
        }
        
    }
}