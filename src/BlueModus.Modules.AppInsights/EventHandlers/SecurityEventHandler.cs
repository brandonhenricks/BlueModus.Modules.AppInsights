using System;
using BlueModus.Modules.AppInsights.EventHandlers;
using BlueModus.Modules.AppInsights.Interfaces;
using BlueModus.Modules.AppInsights.Services;
using CMS;
using CMS.Core;
using CMS.DataEngine;
using CMS.Membership;
using Microsoft.ApplicationInsights.DataContracts;

[assembly: RegisterModule(typeof(SecurityEventHandler))]

namespace BlueModus.Modules.AppInsights.EventHandlers
{
    public class SecurityEventHandler : Module
    {
        private ITelemetryService _telemetryService;

        public SecurityEventHandler() : base(nameof(SecurityEventHandler))
        {
        }

        protected override void OnInit()
        {
            _telemetryService = Service.Resolve<ITelemetryService>();

            SecurityEvents.Authenticate.Execute += Security_Event_Authenticate_Execute;
            base.OnInit();
        }

        protected override void OnPreInit()
        {
            // Registered required types
            Service.Use<ITelemetryService, TelemetryService>();

            base.OnPreInit();
        }

        private void Security_Event_Authenticate_Execute(object sender, AuthenticationEventArgs e)
        {
            var telemetry = new EventTelemetry("User Authentication")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            telemetry.Properties[nameof(e.UserName)] = e.UserName;
            telemetry.Properties[nameof(e.SiteName)] = e.SiteName;

            _telemetryService.TrackEvent(telemetry);
        }
    }
}