using NUnit.Framework;
using Reading.Mails.Core.Api.Extensions;
using System;

namespace Reading.Mails.NUnitTest.Extension
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase("UNO")]
        public void IsValidParse(string value)
        {
            var result = value.ToEnum<TestValuesEnum>();
            Assert.IsTrue(result == TestValuesEnum.UNO);
        }

        [TestCase("DOS")]
        public void NotValidParse(string value)
        {
            var error = "";
            try
            {
                var result = value.ToEnum<TestValuesEnum>();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            Assert.IsTrue(error != null);
        }
    }
}
