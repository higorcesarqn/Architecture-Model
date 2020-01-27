using Shouldly;
using System;
using Xunit;

namespace Egl.Api.Api.IntegrationTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var url = Environment.GetEnvironmentVariable("API_URL");

            url.ShouldBe(@"http://api");

            

        }
    }
}
