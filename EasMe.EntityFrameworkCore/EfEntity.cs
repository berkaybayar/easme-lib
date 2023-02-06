using EasMe.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.EfCore
{
    public abstract class EfEntity : IEfEntity
    {
        protected EfEntity(Guid guid) 
        { 
            Id = guid;
        }

        [Key]
        public Guid Id { get; init; }


        public static bool operator ==(EfEntity left, EfEntity right)
        { 
            return left is not null && right is not null && left.Equals(right); 
        }
        public static bool operator !=(EfEntity left, EfEntity right)
        { 
            return !left.Equals(right); 
        }

        public bool Equals(EfEntity? other)
        {
            if(other is null ) return false;
            if(GetType() != other.GetType()) return false;
            return Id == other.Id;
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (GetType() != obj.GetType()) return false;
            if(obj is not EfEntity entity) return false;
            return Id == entity.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() * 24;
        }
    }
}
