using System.ComponentModel.DataAnnotations;

namespace EasMe.EntityFrameworkCore;

public abstract class BaseEntity : IBaseEntity {
    protected BaseEntity(Guid guid) {
        Id = guid;
    }

    protected BaseEntity() {
    }

    public DateTime RegisterDate { get; set; } = DateTime.Now;

    [Key] public Guid Id { get; set; }

    public bool Equals(BaseEntity? other) {
        if (other is null) return false;
        if (GetType() != other.GetType()) return false;
        return Id == other.Id;
    }


    public static bool operator ==(BaseEntity? left, BaseEntity? right) {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(BaseEntity? left, BaseEntity? right) {
        return left?.Equals(right) == true;
    }

    public override bool Equals(object? obj) {
        if (obj is null) return false;
        if (GetType() != obj.GetType()) return false;
        if (obj is not BaseEntity entity) return false;
        return Id == entity.Id;
    }

    public override int GetHashCode() {
        return Id.GetHashCode() * 24;
    }
}