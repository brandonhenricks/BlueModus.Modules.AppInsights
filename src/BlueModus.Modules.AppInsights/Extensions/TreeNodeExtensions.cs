using System;
using CMS.DocumentEngine;
using Microsoft.ApplicationInsights.DataContracts;

namespace BlueModus.Modules.AppInsights.Extensions
{
    public static class TreeNodeExtensions
    {
        public static EventTelemetry ToEventTelemetry(this TreeNode treeNode, string eventName)
        {
            var eventTelemtry = new EventTelemetry()
            {
                Name = eventName,
                Timestamp = DateTimeOffset.UtcNow
            };

            eventTelemtry.Properties[nameof(TreeNode.ClassName)] = treeNode.ClassName;
            eventTelemtry.Properties[nameof(TreeNode.DocumentName)] = treeNode.DocumentName;
            eventTelemtry.Properties[nameof(TreeNode.DocumentID)] = treeNode.DocumentID.ToString();
            eventTelemtry.Properties[nameof(TreeNode.DocumentModifiedByUserID)] = treeNode.DocumentModifiedByUserID.ToString() ?? string.Empty;
            eventTelemtry.Properties[nameof(TreeNode.DocumentModifiedWhen)] = treeNode.DocumentModifiedWhen.ToString() ?? string.Empty;
            eventTelemtry.Properties[nameof(TreeNode.IsPublished)] = treeNode.IsPublished.ToString();
            eventTelemtry.Properties[nameof(TreeNode.NodeGUID)] = treeNode.NodeGUID.ToString();
            eventTelemtry.Properties[nameof(TreeNode.NodeAliasPath)] = treeNode.NodeAliasPath;
            eventTelemtry.Properties[nameof(TreeNode.Site.SiteName)] = treeNode.Site.SiteName;
            eventTelemtry.Properties[nameof(TreeNode.Site.SiteGUID)] = treeNode.Site.SiteGUID.ToString();
            eventTelemtry.Properties[nameof(TreeNode.Site.SitePresentationURL)] = treeNode.Site.SitePresentationURL;

            return eventTelemtry;
        }
    }
}