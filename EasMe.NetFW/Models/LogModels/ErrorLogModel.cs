namespace EasMe.Models
{
    internal class ErrorLogModel
    {
        //[DataMember(EmitDefaultValue = false)]
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExceptionMessage { get; set; }
        public string ExceptionInner { get; set; }
        public string ExceptionSource { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
