using System;

namespace LinkConverterApplication.Entities
{
    public class Error
    {
        public string Message { get; set; }
        public string Stacktrace { get; set; }
        public string ProjectName { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}