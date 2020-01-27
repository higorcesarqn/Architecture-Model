using Egl.Core.ExceptionHandling;
using Egl.Core.ValuesObjects;
using Xunit;

namespace Egl.Core.UnitTests.ValuesObjectsTests
{
    public class EmailTests
    {
        [Fact]
        [Trait("Core", "Email")]
        public void ValidEmail()
        {
            var emailAddress = "emailteste@teste.com";
            var email = new Email(emailAddress);
            Assert.Equal(emailAddress, email.Address);
        }

        [Fact]
        [Trait("Core", "Email")]
        public void InvalidEmail()
        {
            Assert.Throws<ValuesObjectsException<Email>>(() => { var email = new Email("emailteste@.com"); });
            Assert.Throws<ValuesObjectsException<Email>>(() => { var email = new Email("emailteste.com"); });
            Assert.Throws<ValuesObjectsException<Email>>(() => { var email = new Email("emailteste@com"); });
            Assert.Throws<ValuesObjectsException<Email>>(() => { var email = new Email("@teste.com"); });
            Assert.Throws<ValuesObjectsException<Email>>(() => { var email = new Email(""); });
        }
    }
}
