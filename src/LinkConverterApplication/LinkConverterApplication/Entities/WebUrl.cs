using System;

namespace LinkConverterApplication.Entities
{
    public class WebUrl
    {
        public string Request { get; set; }
        public bool IsActive { get; set; }
        public string LinkId { get; set; }
        public string Response { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}