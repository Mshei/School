using System;

using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
public class MocksHost : IDisposable
{
    private readonly IHost hostForMocks;
    public MocksHost(int port)
    {
        this.hostForMocks =
        Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(x => x
        .ConfigureServices(services => services.AddControllers())
        .Configure(app =>
        app.UseRouting().UseEndpoints(opt => opt.MapControllers()))
        Listing 8.14 Starting ASP.NET inside the test process
        Reference the MVC application
        of the service under test.
        Reference the event
        consumer of the
        service under test.
        Creates an
        ASP.NET host
        Adds the mock
        controllers
        Writing service-level tests 201
.UseUrls($"http://localhost:{port}"))
.Build();
    new Thread(() => this.hostForMocks.Run()).Start();
}
public void Dispose()
{
    this.hostForMocks.Dispose();
}
}
