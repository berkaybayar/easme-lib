using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models
{
    public static class ErrorType
    {
       


        public enum TypeList
        {
            SUCCESS = 0,
            INFO = 1,
            WARN = 2,
            ERROR = 3,
            INVALID_MODEL = 5,
            EXCEPTION_OCCURED = 6,
            NULL_REFERENCE = 7,
            TIMEOUT = 8,

            SQL_ERROR = 10,
            SQL_UPDATE_FAILED = 11,
            SQL_INSERT_FAILED = 12,
            SQL_DELETE_FAILED = 13,
            SQL_TABLE_CREATE_FAILED = 15,
            SQL_TABLE_DELETE_FAILED = 16,
            SQL_TABLE_TRUNCATE_FAILED = 17,

            LOGGING_ERROR = 20,
            SERIALIZATION_ERROR = 21,
            DESERIALIZATION_ERROR = 22,
            CREATING_LOG_ERROR = 23,
            CREATING_FILE_ERROR = 24,
            CREATING_FOLDER_ERROR = 25,
            READING_FILE_ERROR = 26,
            WRITING_FILE_ERROR = 27,
            SERIALIZING_OR_CREATING_LOG_ERROR = 28,

            NOT_EXISTS = 30,
            ALREADY_EXISTS = 31,
            ALREADY_USED = 32,
            ALREADY_IN_USE = 33,
            EXPIRED = 34,

            AUTHENTICATION_FAILED = 40,
            NOT_LOGGED_IN = 41,
            TOKEN_INVALID = 42,
            PASSWORD_INCORRECT = 43,
            USERNAME_INCORRECT = 44,
            EMAIL_INCORRECT = 45,
            EMAIL_ALREADY_SENT = 46,
            EMAIL_SENT_FAILED = 47,

            NO_ONLINE_NETWORK = 50 ,
            FAILED_TO_CONNECT = 51,
            FAILED_TO_GET_RESPONSE = 52,
            FAILED_TO_READ_RESPONSE = 53,
            FAILED_TO_WRITE_RESPONSE = 54,
            FAILED_TO_SEND_RESPONSE = 55,
            
            FAILED_TO_DELETE_FILE = 61,
            FAILED_TO_DELETE_FOLDER = 62,
            FAILED_TO_CREATE_FILE = 63,
            FAILED_TO_CREATE_FOLDER = 64,
            FAILED_TO_READ_FILE = 65,
            FAILED_TO_WRITE_FILE = 66,

        }
        public static string? EnumGetKeybyValue(int value){
            return Enum.GetName(typeof(TypeList), value);        
        }

    }
}
