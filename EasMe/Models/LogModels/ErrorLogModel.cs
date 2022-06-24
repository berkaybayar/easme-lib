namespace EasMe.Models.LogModels
{
    internal class ErrorLogModel
    {

        public string? ExceptionMessage { get; set; }
        public string? ExceptionInner { get; set; }
        public string? ExceptionSource { get; set; }
        public string? ExceptionStackTrace { get; set; }
    }
}
