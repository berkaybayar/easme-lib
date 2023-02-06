using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.DDD.Abstract
{
    public interface IResult
    {
        ushort Rv { get; init; }
        bool IsSuccess { get => Rv == 0; }
        string ErrorCode { get; init; }
        string[] Parameters { get; init; }
    }
}
