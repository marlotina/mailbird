using System;
using System.Collections.Generic;

namespace Reading.Mails.Api.Integration.Test.Model
{
    public class EmailItem
    {
        public int Id { get; set; }
        public IEnumerable<AddressItem> To { get; set; }
        public IEnumerable<AddressItem> From { get; set; }
        public string Subject { get; set; }
        public DateTime? Date { get; set; }
    }
}
