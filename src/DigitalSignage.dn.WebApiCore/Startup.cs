using Microsoft.AspNetCore.Cors;

namespace DigitalSignage.dn.WebApiCore
{
    public class Startup
    {
        public Startup() { }
        public Startup AddDependencyInjection(WebApplication app)
        {

            return this;
        }

        public Startup ConfigureServices(WebApplication app) {

            app.UseCors(options =>
            {
                options
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:4200", "http://localhost:4201", "http://localhost:4202", "http://localhost:4203");
            });
            return this;
        }
    }
}
