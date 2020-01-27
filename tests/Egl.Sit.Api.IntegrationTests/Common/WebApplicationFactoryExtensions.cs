using Egl.Sit.Api.Configuration.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Egl.Sit.Api.IntegrationTests.Common
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient SetupClient(this WebApplicationFactory<StartupTest> fixture)
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                HandleCookies = true
            };

            var client = fixture.WithWebHostBuilder(
                builder => builder
                    .UseStartup<StartupTest>()
            ).CreateClient(options);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
