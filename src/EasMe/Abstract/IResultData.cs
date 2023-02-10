using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Abstract
{
    public interface IResultData<T> : IResult
    {
        T? Data { get; init; }
    }
}
