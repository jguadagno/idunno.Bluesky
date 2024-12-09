﻿// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.Bluesky.Chat.Model
{
    internal sealed record UpdateReadRequest
    {
        public UpdateReadRequest(string convoId, string? messageId)
        {
            ConvoId = convoId;
            MessageId = messageId;
        }

        [JsonInclude]
        [JsonRequired]
        public string ConvoId { get; init; }

        [JsonInclude]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? MessageId { get; init; }
    }
}
