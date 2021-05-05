using System;
using BlueModus.Modules.AppInsights;
using BlueModus.Modules.AppInsights.Extensions;
using BlueModus.Modules.AppInsights.Interfaces;
using BlueModus.Modules.AppInsights.Services;
using CMS;
using CMS.Base;
using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.EventLog;
using Microsoft.ApplicationInsights.DataContracts;

[assembly: RegisterModule(typeof(AppInsightsModule))]

namespace BlueModus.Modules.AppInsights
{
    public class AppInsightsModule : Module
    {
        private ITelemetryService _telemetryService;

        // Module class constructor, the system registers the module under the name "CustomInit"
        public AppInsightsModule()
            : base(nameof(AppInsightsModule))
        {
        }

        protected override void OnInit()
        {
            _telemetryService = Service.Resolve<ITelemetryService>();

            ApplicationEvents.Initialized.Execute += Application_Initialized_Execute;
            ApplicationEvents.End.Execute += Application_End_Execute;

            DocumentEvents.Insert.After += Document_Insert_After;
            DocumentEvents.Copy.After += Document_Copy_After;
            DocumentEvents.CheckPermissions.Failure += Document_CheckPermissions_Failure;
            DocumentEvents.Delete.After += Document_Delete_After;
            DocumentEvents.Move.After += Document_Move_After;
            DocumentEvents.Update.After += Document_Update_After;

            //EventLogEvents.LogEvent.After += LogEvent_After;

            SystemEvents.Exception.Execute += System_Exception_Execute;

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

        private void Application_Initialized_Execute(object sender, EventArgs e)
        {
            var telemetry = new EventTelemetry("Application Initialized.")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            _telemetryService.TrackEvent(telemetry);
        }

        private void Document_CheckPermissions_Failure(object sender, DocumentSecurityEventArgs e)
        {
            var telemetry = e.Node.ToEventTelemetry("Document Permission Failure");

            telemetry.Properties[nameof(e.User.UserID)] = e.User.UserID.ToString();
            telemetry.Properties[nameof(e.User.UserName)] = e.User.UserName;
            telemetry.Properties[nameof(e.User.Email)] = e.User.Email;

            _telemetryService.TrackEvent(telemetry);
        }

        private void Document_Copy_After(object sender, DocumentEventArgs e)
        {
            var telemetry = e.Node.ToEventTelemetry("Document Copied");

            _telemetryService.TrackEvent(telemetry);
        }

        private void Document_Delete_After(object sender, DocumentEventArgs e)
        {
            var telemetry = e.Node.ToEventTelemetry("Document Deleted");

            _telemetryService.TrackEvent(telemetry);
        }

        private void Document_Insert_After(object sender, DocumentEventArgs e)
        {
            var telemetry = e.Node.ToEventTelemetry("Document Inserted");

            _telemetryService.TrackEvent(telemetry);
        }

        private void Document_Move_After(object sender, DocumentEventArgs e)
        {
            var telemetry = e.Node.ToEventTelemetry("Document Moved.");

            _telemetryService.TrackEvent(telemetry);
        }

        private void Document_Update_After(object sender, DocumentEventArgs e)
        {
            var telemetry = e.Node.ToEventTelemetry("Document Updated");

            _telemetryService.TrackEvent(telemetry);
        }

        private void LogEvent_After(object sender, LogEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void System_Exception_Execute(object sender, SystemEventArgs e)
        {
            var telemetry = new ExceptionTelemetry(e.Exception)
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            _telemetryService.TrackException(telemetry);
        }
    }
}