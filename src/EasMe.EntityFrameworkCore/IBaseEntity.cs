namespace EasMe.EntityFrameworkCore;

public interface IBaseEntity : IEquatable<BaseEntity>, IEntity {
    Guid Id { get; set; }
}