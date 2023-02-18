using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Result
{
    public enum ErrorCode
    {
		Ok,
        Warning,
        Error,
        Exception,
        Fatal,
        DbInternal,
        Forbidden,
		Unauthorized,
		ValidationError,
		NullReference,
		Deleted,
		Disabled,
        Required,
        TooShort,
        TooLong,
        Expired,
		UnderMaintenance,
        WrongData,

        AlreadyExists,
		AlreadyInUse,
		AlreadyDeleted,

		NotFound,
		NotVerified,
		NotValid,
		NotExist,
		NotAuthorized,
        NotMatch,

        CanNotBeUsed,
		CanNotContainSpace,
		CanNotBeSame, //2 param

        MustContainSpecialCharacter,
        MustContainDigit,
		MustContainLowerCase,
		MustContainUpperCase,
		MustBeSame, //2 param

	}
}
