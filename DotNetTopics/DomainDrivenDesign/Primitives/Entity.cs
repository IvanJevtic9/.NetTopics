﻿namespace DomainDrivenDesign.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get; private init; }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public static bool operator ==(Entity? first, Entity second)
        {
            return 
                first is not null &&
                second is not null &&
                first.Equals(second);
        }

        public static bool operator !=(Entity? first, Entity? second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            if(obj.GetType() != GetType()) return false;

            if( obj is not Entity entity) return false;

            return entity.Id == Id;
        }

        public bool Equals(Entity other)
        {
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 41;
        }        
    }
}
