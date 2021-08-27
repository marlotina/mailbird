namespace Reading.Mails.Api.Integration.Test.Helper
{
    public class SeetingsHelper
    {
        public const string API_URL = "https://localhost:44379/";
        public const string USER_NAME = "inaki@marlotina.com";
        public const string USER_PASS = "";
        public const string SERVER = "40-87-149-67.cprapid.com";


        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
