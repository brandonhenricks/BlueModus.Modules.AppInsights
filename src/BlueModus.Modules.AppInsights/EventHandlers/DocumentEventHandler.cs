using BlueModus.Modules.AppInsights.EventHandlers;
using BlueModus.Modules.AppInsights.Extensions;
using BlueModus.Modules.AppInsights.Interfaces;
using BlueModus.Modules.AppInsights.Services;
using CMS;
using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;

[assembly: RegisterModule(typeof(DocumentEventHandler))]

namespace BlueModus.Modules.AppInsights.EventHandlers
{
    public class DocumentEventHandler : Module
    {
        private ITelemetryService _telemetryService;

        public DocumentEventHandler() : base(nameof(DocumentEventHandler))
        {
        }

        protected override void OnInit()
        {
            _telemetryService = Service.Resolve<ITelemetryService>();

            DocumentEvents.Insert.After += Document_Insert_After;
            DocumentEvents.Copy.After += Document_Copy_After;
            DocumentEvents.CheckPermissions.Failure += Document_CheckPermissions_Failure;
            DocumentEvents.Delete.After += Document_Delete_After;
            DocumentEvents.Move.After += Document_Move_After;
            DocumentEvents.Update.After += Document_Update_After;
            base.OnInit();
        }
        

        protected override void OnPreInit()
        {
            // Registered required types
            Service.Use<ITelemetryService, TelemetryService>();

            base.OnPreInit();
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
            var telemetry = e.Node.ToEventTelemetry("Document Moved");

            _telemetryService.TrackEvent(telemetry);
        }

        private void Document_Update_After(object sender, DocumentEventArgs e)
        {
            var telemetry = e.Node.ToEventTelemetry("Document Updated");

            _telemetryService.TrackEvent(telemetry);
        }
    }
}