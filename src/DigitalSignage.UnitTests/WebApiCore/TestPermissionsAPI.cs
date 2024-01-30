global using NUnit.Framework;
using DigitalSignage.Data;
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.dn.WebApiCore;
using DigitalSignage.Infrastructure.Models.Settings;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace DigitalSignage.UnitTests.DataAPI;

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

    [Test]
    public async Task GetDisplays()
    {
        // Arrange
        var descriptionData = "nice Display";
        var titleData = DateTime.Now.ToString();
        using (var scope = factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<DigitalSignageDbContext>();

            db.Displays.Add(new Display(){ 
                Dummy= false,
                Description = descriptionData,
                Title = titleData
            });
            db.SaveChanges();
        }

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "TestScheme");

        // Act
        var response = await client.GetAsync("/settings/displays");
        var actual = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<object>>(actual);

        // Assert
        response.EnsureSuccessStatusCode();
        content.Should().NotBeNull();
        content.Should().HaveCount(1);
        actual.Should().Contain(descriptionData);
        actual.Should().Contain(titleData);
    }

    [Test]
    public async Task BasicPermissions()
    {
        // Arrange
        var descriptionData = "nice Display";
        var titleData = DateTime.Now.ToString();
        
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/settings/permissions/BasicPermissions");
        var actual = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<object>>(actual);

        // Assert
        response.EnsureSuccessStatusCode();
        content.Should().NotBeNull();
        content.Should().HaveCount(1);
        actual.Should().Contain(descriptionData);
        actual.Should().Contain(titleData);
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
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "TestScheme");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}