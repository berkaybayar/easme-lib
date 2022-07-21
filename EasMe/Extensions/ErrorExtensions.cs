namespace EasMe.Extensions
{
    public static class ErrorExtensions
    {
        /// <summary>
        /// Writes an error message for user. Example: "An internal error occured, please contact support. ErrorCode: 82"
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string WriteMessage(this Error error)
        {
            return "An internal error occured, please contact support. ErrorCode: " + (int)error;
        }


    }
}
