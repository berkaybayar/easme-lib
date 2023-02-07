using System.ComponentModel.DataAnnotations;
using EasMe.EntityFrameworkCore.Abstract;

namespace EasMe.EntityFrameworkCore
{
    public abstract class DbEntity : IDbEntity
    {
        protected DbEntity(Guid guid) 
        { 
            Id = guid;
        }

        [Key]
        public Guid Id { get; init; }


        public static bool operator ==(DbEntity? left, DbEntity? right)
        { 
            return left is not null && right is not null && left.Equals(right); 
        }
        public static bool operator !=(DbEntity? left, DbEntity? right)
        { 
            return left?.Equals(right) == true; 
        }

        public bool Equals(DbEntity? other)
        {
            if(other is null ) return false;
            if(GetType() != other.GetType()) return false;
            return Id == other.Id;
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (GetType() != obj.GetType()) return false;
            if(obj is not DbEntity entity) return false;
            return Id == entity.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() * 24;
        }
    }
}
