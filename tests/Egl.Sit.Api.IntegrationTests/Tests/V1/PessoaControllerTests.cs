using Egl.Sit.Api.Configuration.Test;
using Egl.Sit.Api.IntegrationTests.Tests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Egl.Sit.Api.IntegrationTests.Tests.V1
{
    public class PessoaControllerTests : BaseClassFixture
    {
        public PessoaControllerTests(WebApplicationFactory<StartupTest> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetEscolaridade_Unauthorized()
        {
            SetupAdminClaimsViaHeaders();

            var response = await Client.GetAsync("api/v1/pessoas/tipos-escolaridade");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
