using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Enums
{
    public enum ErrorCode
    {
        UnexpectedError,
        Exception,
        Fatal,
		Error,
        Warning,
		DbInternal,
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
