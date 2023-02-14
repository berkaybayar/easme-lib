using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.EntityFrameworkCore.Abstract
{
    public interface IBaseEntity : IEquatable<BaseEntity>
    {
        Guid Id { get; init; }
    }
}
