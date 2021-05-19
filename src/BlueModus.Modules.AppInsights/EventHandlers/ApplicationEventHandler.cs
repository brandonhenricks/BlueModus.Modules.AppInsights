using System;
using BlueModus.Modules.AppInsights.EventHandlers;
using BlueModus.Modules.AppInsights.Interfaces;
using BlueModus.Modules.AppInsights.Services;
using CMS;
using CMS.Base;
using CMS.Core;
using CMS.DataEngine;
using Microsoft.ApplicationInsights.DataContracts;

[assembly: RegisterModule(typeof(ApplicationEventHandler))]

namespace BlueModus.Modules.AppInsights.EventHandlers
{
    public class ApplicationEventHandler : Module
    {
        private ITelemetryService _telemetryService;

        public ApplicationEventHandler() : base(nameof(ApplicationEventHandler))
        {
        }

        protected override void OnInit()
        {
            _telemetryService = Service.Resolve<ITelemetryService>();

            ApplicationEvents.Initialized.Execute += Application_Initialized_Execute;
            ApplicationEvents.Error.Execute += Application_Error_Execute;
            ApplicationEvents.End.Execute += Application_End_Execute;

            base.OnInit();
        }

        protected override void OnPreInit()
        {
            // Registered required types
            Service.Use<ITelemetryService, TelemetryService>();

            base.OnPreInit();
        }

        private void Application_End_Execute(object sender, EventArgs e)
        {
            var telemetry = new EventTelemetry("Application End.")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            _telemetryService.TrackEvent(telemetry);
        }

        private void Application_Error_Execute(object sender, EventArgs e)
        {
            var telemetry = new EventTelemetry("Application Error.")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            _telemetryService.TrackEvent(telemetry);
        }

        private void Application_Initialized_Execute(object sender, EventArgs e)
        {
            var telemetry = new EventTelemetry("Application Initialized.")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            _telemetryService.TrackEvent(telemetry);
        }
    }
}