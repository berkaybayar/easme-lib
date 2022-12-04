namespace EasMe
{
    public enum LogType
    {
        BASE = 0,
        EXCEPTION = 1,
        WEB = 2,

    }

    public enum Severity
    {
        TRACE = 0,
        DEBUG = 1,
        INFO = 2,
        WARN = 3,
        ERROR = 4,
        EXCEPTION = 5,
        FATAL = 6,
    }
    public enum Error
    {
        NoError,
        Service,
        Exception,
        Fatal,
        Failed,
        Timeout,
        Internal,
        

        InvalidValue,
        InvalidModel,
        NullReference,
        NotValid,
        NotExists,
        NotFound,
        NotAllowed,
        NotSupported,
        NotAvaliable,
        NotEnabled,
        NotDisabled,
        NotConnected,
        NotLoggedIn,
        NotLoggedOut,
        NotAuthorized,
        NotAuthenticated,
        NotReady,
        NotInitialized,
        NotExpired,
        NotLoaded,
        NotPossible,
        NotSent,
        NotReceived,
        NotOnline,
        NotOffline,
        NotDisconnected,
        NotUpdated,
        NotOpen,
        NotClosed,
        AlreadyExists,
        AlreadyUsed,
        AlreadyInUse,
        AlreadySent,
        Expired,
        Offline,
        Disconnected,
        NoOnlineNetwork,
        NotSet,

		//Sql Errors
		DbSuccess,
		DbError,
        DbNotFound,
        DbSelect,
        DbAddFailed,
        DbUpdateFailed,
        DbInsertFailed,
        DbDeleteFailed,
        DbCreateFailed,
        DbTruncateFailed,
        DbExecNonQueryFailed,
        DbExecScalarFailed,
        DbBackupFailed,
        DbShrinkFailed,
        DbDropFailed,
        DbRestoreFailed,
        DbNoPermission,
        DbNoRowsFound,
        DbNoRowsAffected,
        DbMultipleRows,
		DbAlreadyExists,

        //ApiErrors
        FailedToSend,
        FailedToSendGet,
        FailedToSendPost,
        FailedToSendPut,
        FailedToSendDelete,
        FailedToSendHead,
        FailedToConnect,

        //Validation login etc.
        FailedToValidate,
        FailedToInvalidate,
        FailedToAuthenticate,
        FailedToAuthorize,
        FailedToLogin,
        FailedToRegister,

        //Action
        FailedToLog,
        FailedToSerialize,
        FailedToDeserialize,
        FailedToCreate,
        FailedToDelete,
        FailedToRead,
        FailedToWrite,
        FailedToConvert,
        FailedToMove,
        FailedToParse,
        FailedToCopy,
        FailedToCut,
        FailedToPaste,
        FailedToSave,
        FailedToRemove,
        FailedToBlock,

        //Other
        TooBigValue,
        TooSmallValue,
        SameValue,

        //WebLogin
        WrongPassword,
        WrongUsername,
        WrongEmail,

        Blocked,
        AlreadyBlocked,

        InvalidOperation,
        FailedToRun,
        MaxValueReached,
        MinValueReached,


    }

}
