global using NUnit.Framework;
using DigitalSignage.Data;
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.dn.WebApiCore;
using DigitalSignage.dn.WebApiCore.DtoModels;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Controllers.Settings;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace DigitalSignage.UnitTests.WebApiCore;

public class TestPermissionsAPI
{
    protected WebApplicationFactory<Program> factory;
    protected HttpClient client;

    [OneTimeSetUp]
    public void Setup()
    {
        Program.NoTest = false;
        // https://adolfi.dev/blog/integration-testing-an-umbraco-api-using-net-web-application-factory/
        factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(defaultScheme: "TestScheme")
                     .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                         "TestScheme", options => { });
                    //services.AddDbContextPool<ApplicationDbContext>(o => o.UseInMemoryDatabase("t"));
                    //services.AddScoped<IEmailService, FakeEmailService>();
                });
            });
        
        client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
        });
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await factory.DisposeAsync();
        client?.Dispose();
    }

    [Test]
    public async Task BasicPermissions()
    {
        // Arrange
        var descriptionData = "nice Display";
        var titleData = DateTime.Now.ToString();
        
        // Act
        var response = await client.GetAsync("/settings/permissions/BasicPermissions");
        var content = await response.Content.ReadFromJsonAsync< BasicPermissions>();

        // Assert
        response.EnsureSuccessStatusCode();
        content.Should().NotBeNull();
        Assert.IsNotNull(content?.AllowNotes);
        Assert.IsNotNull(content?.AllowTermine);
    }
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
        var principal = new ClaimsPrincipal(WindowsIdentity.GetCurrent());
        var ticket = new AuthenticationTicket(principal, "TestScheme");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}