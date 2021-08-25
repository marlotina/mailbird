using System;

namespace Reading.Mails.Core.Api.Application.Exceptions
{
    public class NoFoundStrategy : Exception
    {
        public NoFoundStrategy(string message) : base(message)
        {
        }
    }
}
