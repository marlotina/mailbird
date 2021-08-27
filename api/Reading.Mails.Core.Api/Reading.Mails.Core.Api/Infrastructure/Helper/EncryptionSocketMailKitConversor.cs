using MailKit.Security;
using Reading.Mails.Core.Api.Domain.enumerations;
using Reading.Mails.Core.Api.Extensions;

namespace Reading.Mails.Core.Api.Infrastructure.Helper
{
    public static class EncryptionSocketMailKitConversor
    {
        public static SecureSocketOptions GetSocketOption(string encryptionTypesEnum)
        {
            if (encryptionTypesEnum.ToEnum<EncryptionTypesEnum>() == EncryptionTypesEnum.SSL_TLS)
            {
                return SecureSocketOptions.SslOnConnect;
            }
            else if (encryptionTypesEnum.ToEnum<EncryptionTypesEnum>() == EncryptionTypesEnum.STARTTLS)
            {
                return SecureSocketOptions.StartTlsWhenAvailable;
            }

            return SecureSocketOptions.None;
        }
    }
}
