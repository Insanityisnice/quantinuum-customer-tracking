namespace CustomerTrackingService
{
    using CustomerTracking;
    using Microsoft.Extensions.Logging;
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;
    using Nancy.Configuration;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private ILoggerFactory loggerFactory;
        private ILogger<Bootstrapper> logger;

        public Bootstrapper(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            logger = this.loggerFactory.CreateLogger<Bootstrapper>();
        }

        public override void Configure(INancyEnvironment environment)
        {
            using(logger.BeginScope("Bootstrapper.Configure"))
            {
                //TODO: Drive these values from configuration.
                bool tracingEnabled = false;
                bool displayErrorTraces = true;

                logger.LogInformation($"Tracing Enabled: {tracingEnabled}, Displaying Error Traces: {displayErrorTraces}");
                environment.Tracing(enabled: tracingEnabled, displayErrorTraces: displayErrorTraces);
            }
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            using(logger.BeginScope("Bootstrapper.ApplicationStartup"))
            {
                base.ApplicationStartup(container, pipelines);

                logger.LogInformation("Registering Logger Factory.");
                container.Register<ILoggerFactory>(loggerFactory);

                // TODO: Dynamicly resolve the logger for a class without registering. 
                // Currently I don't know of a way to capture the request to resolve in TinyIoC and dynamicly resolve 
                // the logger for that class.  Further research is needed.
                logger.LogInformation($"Registering logger for {nameof(CustomerTrackingModule)}.");
                container.Register<ILogger<CustomerTrackingModule>>((c, e) => loggerFactory.CreateLogger<CustomerTrackingModule>());
            }
        }
    }
}