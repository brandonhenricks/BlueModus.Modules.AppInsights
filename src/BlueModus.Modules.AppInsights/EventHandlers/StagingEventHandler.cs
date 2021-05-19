using System;
using BlueModus.Modules.AppInsights.EventHandlers;
using BlueModus.Modules.AppInsights.Interfaces;
using BlueModus.Modules.AppInsights.Services;
using CMS;
using CMS.Core;
using CMS.DataEngine;
using CMS.Synchronization;
using Microsoft.ApplicationInsights.DataContracts;

[assembly: RegisterModule(typeof(StagingEventHandler))]

namespace BlueModus.Modules.AppInsights.EventHandlers
{
    public class StagingEventHandler : Module
    {
        private ITelemetryService _telemetryService;

        public StagingEventHandler() : base(nameof(StagingEventHandler))
        {
        }

        protected override void OnInit()
        {
            _telemetryService = Service.Resolve<ITelemetryService>();
            StagingEvents.Synchronize.Before += Staging_Synchronize_Before;
            StagingEvents.Synchronize.After += Staging_Synchronize_After;
            StagingEvents.Synchronize.Failure += Staging_Synchronize_Failure;
            StagingEvents.SynchronizeTask.Before += Staging_SynchronizeTask_Before;
            StagingEvents.SynchronizeTask.After += Staging_SynchronizeTask_After;
            StagingEvents.SynchronizeTask.Failure += Staging_SynchronizeTask_Failure;
            base.OnInit();
        }

        protected override void OnPreInit()
        {
            // Registered required types
            Service.Use<ITelemetryService, TelemetryService>();

            base.OnPreInit();
        }

        private void Staging_Synchronize_After(object sender, CMS.Base.CMSEventArgs e)
        {
            var telemetry = new EventTelemetry("Staging Synchronization Finished")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            _telemetryService.TrackEvent(telemetry);
        }

        private void Staging_Synchronize_Before(object sender, CMS.Base.CMSEventArgs e)
        {
            var telemetry = new EventTelemetry("Staging Synchronization Starting")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            _telemetryService.TrackEvent(telemetry);
        }

        private void Staging_Synchronize_Failure(object sender, CMS.Base.CMSEventArgs e)
        {
            var telemetry = new EventTelemetry("Staging Synchronization Failed")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            _telemetryService.TrackEvent(telemetry);
        }

        private void Staging_SynchronizeTask_After(object sender, StagingTaskEventArgs e)
        {
            var telemetry = new EventTelemetry("Staging Synchronization Task Finished")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            telemetry.Properties[nameof(e.Task.TaskDocumentID)] = e.Task.TaskDocumentID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskID)] = e.Task.TaskID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskSiteID)] = e.Task.TaskSiteID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskTitle)] = e.Task.TaskTitle ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskServers)] = e.Task.TaskServers ?? string.Empty;

            _telemetryService.TrackEvent(telemetry);
        }

        private void Staging_SynchronizeTask_Before(object sender, StagingTaskEventArgs e)
        {
            var telemetry = new EventTelemetry("Staging Synchronization Task Starting")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            telemetry.Properties[nameof(e.Task.TaskDocumentID)] = e.Task.TaskDocumentID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskID)] = e.Task.TaskID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskSiteID)] = e.Task.TaskSiteID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskTitle)] = e.Task.TaskTitle ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskServers)] = e.Task.TaskServers ?? string.Empty;

            _telemetryService.TrackEvent(telemetry);
        }

        private void Staging_SynchronizeTask_Failure(object sender, StagingTaskEventArgs e)
        {
            var telemetry = new EventTelemetry("Staging Synchronization Task Failed")
            {
                Timestamp = DateTimeOffset.UtcNow
            };

            telemetry.Properties[nameof(e.Task.TaskDocumentID)] = e.Task.TaskDocumentID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskID)] = e.Task.TaskID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskSiteID)] = e.Task.TaskSiteID.ToString() ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskTitle)] = e.Task.TaskTitle ?? string.Empty;
            telemetry.Properties[nameof(e.Task.TaskServers)] = e.Task.TaskServers ?? string.Empty;

            _telemetryService.TrackEvent(telemetry);
        }
    }
}