using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasMe.Abstract
{

    public interface IResult
    {
        /// <summary>
        /// Indicates the exact location of the result coming from.
        /// <br/><br/>
        /// It is to compensate <see cref="Exception.StackTrace"/>. Every <see cref="IResult"/> returned in a single method each return must have different <see cref="Rv"/>.
        /// <br/><br/>
        /// In cases where using <see cref="IResult"/> returning methods that returns <see cref="IResult"/> from another method,
        /// it is recommended to multiply the incoming <see cref="Rv"/> value before returning exact result
        /// <br/>
        /// It is recommended to multiply the <see cref="Rv"/> from another method by 100 or multiples of 100
        /// <br/><br/>
        /// Serialization of <see cref="Rv"/> is disabled for "System.Text.Json.Serialization" due to security reasons.
        /// <br/>In order to see it in logs, must use "Newtonsoft.Json" library
        /// </summary>
        [JsonIgnore]
        ushort Rv { get; init; }

        /// <summary>
        /// Indicates success status of <see cref="IResult"/>. 
        /// </summary>
        bool IsSuccess => Rv == 0;

        /// <summary>
        /// Indicates fail status of <see cref="IResult"/>. 
        /// </summary>
        bool IsFailure => Rv != 0;

        string ErrorCode { get; init; }



    }
}
