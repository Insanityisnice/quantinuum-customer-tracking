namespace CustomerTrackingService.CustomerTracking
{
    using System;
    using Microsoft.Extensions.Logging;
    using Nancy;
    using Nancy.ModelBinding;
    
    public class CustomerTrackingModule : NancyModule
    {
        public CustomerTrackingModule(ILogger<CustomerTrackingModule> logger) : base("/customertracking")
        {
            if(logger == null) throw new ArgumentNullException(nameof(logger));

            Get("/{customerId:int}", parameters => {
                using(logger.BeginScope("CustomerTrackingModule.Get('/{customerId})"))
                {
                    var customerId = (int)parameters.customerId;
                    logger.LogDebug($"Retreiving Customer: {customerId}.");

                    return new {
                        customerId = customerId
                    };
                }
            });
        }
    }
}