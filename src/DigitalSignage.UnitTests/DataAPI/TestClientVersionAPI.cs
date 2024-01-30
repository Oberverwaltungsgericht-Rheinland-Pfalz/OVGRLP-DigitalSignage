global using NUnit.Framework;
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.DataAPI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DigitalSignage.UnitTests.DataAPI;

public class TestClientVersionAPI
{
    protected WebApplicationFactory<Program> factory;

    [SetUp]
    public void Setup()
    {
        factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    //services.AddDbContextPool<ApplicationDbContext>(o => o.UseInMemoryDatabase("t"));
                    //services.AddScoped<IEmailService, FakeEmailService>();
                });
            });
    }

    [Test]
    public async Task GetAll()
    {
        // Arrange
        var versionData = "version:1-alpha";
        var pathData = DateTime.Now.ToString();
        using (var scope = factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();

            db.ClientVersionRepository.Add(new ClientVersion<Guid> { 
                Id = Guid.NewGuid(), 
                Version = versionData,
                Path = pathData
            });
            db.SaveChanges();
        }

        var client = factory.CreateClient();
   
        // Act
        var response = await client.GetAsync("/v1/ClientVersion");
        var actual = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<object>>(actual);

        // Assert
        response.EnsureSuccessStatusCode();
        content.Should().NotBeNull();
        content.Should().HaveCount(1);
        actual.Should().Contain(versionData);
        actual.Should().Contain(pathData);
    }
}