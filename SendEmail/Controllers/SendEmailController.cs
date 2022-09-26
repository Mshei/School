using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Coravel;
using Serilog;
using Serilog.Formatting.Json;
using System.Web.Helpers;
using Sprache;
using Newtonsoft.Json;

namespace SendEmail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendEmailController : ControllerBase
    {
        [HttpGet(Name = "SendEmailController")]
        public IEnumerable<SendEmailController> Get()
        {

            String Json;

            DotNetEnv.Env.Load();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    rollingInterval: RollingInterval.Day)
                .WriteTo.File("logs/errorlog.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();

            try
            {
                Log.Information("Starting service..");
                ParkQueueConnector parkQueueConnector = new ParkQueueConnector();
                var host = parkQueueConnector.CreateHostBuilder().Build();
                host.Services.UseScheduler(scheduler =>
                {
                    var jobSchedule = scheduler.Schedule<ProcessPark>();
                    jobSchedule
                        .EverySeconds(2)
                        .PreventOverlapping("ProcessOrderJob");
                });

                host.Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Exception in application");
            }
            finally
            {
                Log.Information("Exiting service");
                Log.CloseAndFlush();
            }

            Json = JsonConvert.SerializeObject("Sent");

            return (IEnumerable<SendEmailController>)Ok(Json);

        }
    }
    
}