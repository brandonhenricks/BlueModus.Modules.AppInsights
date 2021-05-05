using System;
using BlueModus.Modules.AppInsights.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace BlueModus.Modules.AppInsights.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly TelemetryClient _telemetryClient;

        public TelemetryService()
        {
            TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();

            _telemetryClient = new TelemetryClient(configuration);
        }

        public TelemetryService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public void TrackEvent(EventTelemetry telemetry)
        {
            if (telemetry is null)
            {
                throw new ArgumentNullException(nameof(telemetry));
            }

            _telemetryClient.TrackEvent(telemetry);
        }

        public void TrackException(ExceptionTelemetry telemetry)
        {
            if (telemetry is null)
            {
                throw new ArgumentNullException(nameof(telemetry));
            }

            _telemetryClient.TrackException(telemetry);
        }
    }
}