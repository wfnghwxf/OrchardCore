using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.Workflows.Models;

namespace OrchardCore.Contents.Activities
{
    public class Publish : ContentActivity
    {
        private readonly IContentManager _contentManager;

        public Publish(IContentManager contentManager, IStringLocalizer<Publish> s) : base(s)
        {
            _contentManager = contentManager;
        }

        public override string Name => nameof(Publish);
        public override LocalizedString Category => S["Content Items"];
        public override LocalizedString Description => S["Publish the content item."];

        public override IEnumerable<LocalizedString> GetPossibleOutcomes(WorkflowContext workflowContext, ActivityContext activityContext)
        {
            yield return S["Published"];
        }

        public override async Task<IEnumerable<LocalizedString>> ExecuteAsync(WorkflowContext workflowContext, ActivityContext activityContext)
        {
            var content = GetContent(workflowContext);
            await _contentManager.PublishAsync(content.ContentItem);
            return new[] { S["Published"] };
        }
    }
}