using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.EntityFrameworkCore
{
    public interface IBaseEntity : IEquatable<BaseEntity>, IEntity
    {
        Guid Id { get; set; }
    }
}
