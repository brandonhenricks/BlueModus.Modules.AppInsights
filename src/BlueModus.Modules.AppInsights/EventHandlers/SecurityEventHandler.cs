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
            SecurityEvents.AuthorizeClass.Execute += Security_Event_AuthorizeClass_Execute;
            SecurityEvents.AuthorizeResource.Execute += Security_Event_AuthorizeResource_Execute;
            base.OnInit();
        }

        private void Security_Event_AuthorizeResource_Execute(object sender, AuthorizationEventArgs e)
        {
            var telemetry = new EventTelemetry("Authorize Resource")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            telemetry.Properties[nameof(e.Authorized)] = e.Authorized.ToString();
            telemetry.Properties[nameof(e.ClassName)] = e.ClassName;
            telemetry.Properties[nameof(e.ElementName)] = e.ElementName;
            telemetry.Properties[nameof(e.PermissionName)] = e.PermissionName;
            telemetry.Properties[nameof(e.ResourceName)] = e.ResourceName;

            _telemetryService.TrackEvent(telemetry);
        }

        private void Security_Event_AuthorizeClass_Execute(object sender, AuthorizationEventArgs e)
        {

            var telemetry = new EventTelemetry("Authorize Object Or Page Type")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            telemetry.Properties[nameof(e.Authorized)] = e.Authorized.ToString();
            telemetry.Properties[nameof(e.ClassName)] = e.ClassName;
            telemetry.Properties[nameof(e.ElementName)] = e.ElementName;
            telemetry.Properties[nameof(e.PermissionName)] = e.PermissionName;
            telemetry.Properties[nameof(e.ResourceName)] = e.ResourceName;

            _telemetryService.TrackEvent(telemetry);
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