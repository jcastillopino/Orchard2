﻿using Orchard.ContentManagement;
using Orchard.Alias.Models;
using YesSql.Core.Indexes;

namespace Orchard.Alias.Indexes
{
    public class AliasPartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public string Alias { get; set; }
    }

    public class AliasPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<AliasPartIndex>()
                .Map(contentItem =>
                {
                    if (!contentItem.IsPublished())
                    {
                        return null;
                    }

                    var aliasPart = contentItem.As<AliasPart>();

                    if (aliasPart?.Alias == null)
                    {
                        return null;
                    }

                    return new AliasPartIndex
                    {
                        Alias = aliasPart.Alias.ToLowerInvariant(),
                        ContentItemId = contentItem.ContentItemId,
                    };
                });
        }
    }
}