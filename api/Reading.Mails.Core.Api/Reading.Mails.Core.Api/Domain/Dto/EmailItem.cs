using System;
using System.Collections.Generic;

namespace Reading.Mails.Core.Api.Domain.Dto
{
    public class EmailItem
    {
        public string Id { get; set; }
        public IEnumerable<AddressItem> To { get; set; }
        public IEnumerable<AddressItem> From { get; set; }
        public string Subject { get; set; }
        public DateTime? Date { get; set; }
        public EmailExtraInfo ExtraInfo { get; set; }
    }
}
