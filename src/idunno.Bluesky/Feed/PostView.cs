﻿// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

using idunno.AtProto;
using idunno.AtProto.Labels;
using idunno.AtProto.Repo;
using idunno.Bluesky.Actor;
using idunno.Bluesky.Embed;

namespace idunno.Bluesky.Feed
{
    /// <summary>
    /// A view over a post record generated by a Bluesky feed.
    /// </summary>
    /// <remarks>
    /// <para>See https://docs.bsky.app/docs/api/app-bsky-feed-get-posts</para>
    /// <para>See https://github.com/bluesky-social/atproto/blob/main/lexicons/app/bsky/feed/defs.json</para>
    /// </remarks>
    public sealed record PostView : PostViewBase
    {
        [JsonConstructor]
        internal PostView(
            AtUri uri,
            Cid cid,
            ProfileViewBasic author,
            PostRecord record,
            int replyCount,
            int repostCount,
            int likeCount,
            int quoteCount,
            ThreadGateView? threadGate,
            DateTimeOffset indexedAt,
            FeedViewerState? viewer,
            IReadOnlyCollection<Label>? labels,
            EmbeddedView? embed)
        {
            Uri = uri;
            Cid = cid;
            StrongReference = new StrongReference(uri, cid);

            Author = author;

            Record = record;

            ReplyCount = replyCount;
            RepostCount = repostCount;
            LikeCount = likeCount;
            QuoteCount = quoteCount;

            ThreadGate = threadGate;

            IndexedAt = indexedAt;

            Viewer = viewer;

            if (labels is not null)
            {
                Labels = labels;
            }
            else
            {
                Labels = new List<Label>().AsReadOnly<Label>();
            }

            Embed = embed;
        }

        /// <summary>
        /// The <see cref="AtUri"/> for the post.</param>
        /// </summary>
        [JsonRequired]
        public AtUri Uri { get; init; }

        /// <summary>
        /// The <see cref="Cid">content identifier</see> for the post.</param>
        /// </summary>
        [JsonRequired]
        public Cid Cid { get; init; }

        /// <summary>
        /// A <see cref="StrongReference"/> to the post.
        /// </summary>
        [JsonIgnore]
        public StrongReference StrongReference { get; }

        /// <summary>
        /// A <see cref="ProfileViewBasic"/> of the author of the post.
        /// </summary>
        [JsonRequired]
        public ProfileViewBasic Author { get; init; }

        /// <summary>
        /// The record for the post.
        /// </summary>
        [JsonRequired]
        public PostRecord Record {get; init;}

        /// <summary>
        /// The number of replies to this post.
        /// </summary>
        [JsonInclude]
        public int ReplyCount { get; init; }

        /// <summary>
        /// The number of times this post has been reposted.
        /// </summary>
        [JsonInclude]
        public int RepostCount { get; init; }

        /// <summary>
        /// The number of likes this post has.
        /// </summary>
        [JsonInclude]
        public int LikeCount { get; init; }

        /// <summary>
        /// The number of quotes this post has.
        /// </summary>
        [JsonInclude]
        public int QuoteCount { get; init; }

        /// <summary>
        /// The date and time the post was indexed at.
        /// </summary>
        [JsonInclude]
        [JsonRequired]
        public DateTimeOffset IndexedAt { get; init; }

        /// <summary>
        /// An optional <see cref="ThreadGate"/> controlling who can reply to a post.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("threadgate")]
        public ThreadGateView? ThreadGate { get; init; }

        /// <summary>
        /// Metadata about the requesting account's relationship with the subject content.
        /// Only has meaningful content for authenticated requests.
        /// </summary>
        [JsonInclude]
        public FeedViewerState? Viewer { get; init; }

        /// <summary>
        /// Any labels applied to the post.
        /// </summary>
        [JsonInclude]
        public IReadOnlyCollection<Label> Labels { get; init; }

        /// <summary>
        /// An optional <see cref="EmbeddedView" /> for embedded media information.
        /// </summary>
        [JsonInclude]
        public EmbeddedView? Embed { get; init; }
    }
}
