using Microsoft.ApplicationInsights.DataContracts;

namespace BlueModus.Modules.AppInsights.Interfaces
{
    public interface ITelemetryService
    {
        void TrackEvent(EventTelemetry telemetry);

        void TrackException(ExceptionTelemetry telemetry);
    }
}