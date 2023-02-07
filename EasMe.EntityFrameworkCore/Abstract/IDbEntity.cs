using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.EntityFrameworkCore.Abstract
{
    public interface IDbEntity : IEquatable<DbEntity>
    {
        Guid Id { get; init; }
    }
}
