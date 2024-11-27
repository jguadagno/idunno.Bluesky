﻿// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Web;
using idunno.AtProto;

namespace idunno.DidPlcDirectory
{
    /// <summary>
    /// Provides a class for sending requests to and receiving responses from an directory service, identified by its service URI.
    /// </summary>
    [SuppressMessage("Performance", "CA1812", Justification = "Used in DID resolution.")]
    internal static class DirectoryServer
    {
        // https://web.plc.directory/api/redoc#operation/ResolveDid

        /// <summary>
        /// Resolves the specified <paramref name="did"/> on the specified <paramref name="directory"/>.
        /// </summary>
        /// <param name="did">The DID to resolve.</param>
        /// <param name="directory">The directory server to use to resolve the <paramref name="did"/>.</param>
        /// <param name="httpClient">An <see cref="HttpClient"/> to use when making a request to the <paramref name="directory"/>.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<AtProtoHttpResult<DidDocument>> ResolveDidDocument(Did did, Uri directory, HttpClient httpClient, CancellationToken cancellationToken = default)
        {
            const string plcDidPrefix = "did:plc:"; // https://github.com/did-method-plc/did-method-plc
            const string webDidPrefix = "did:web:"; // https://w3c-ccg.github.io/did-method-web/

            if (did.ToString().StartsWith(plcDidPrefix, StringComparison.InvariantCulture))
            {
                AtProtoHttpClient<DidDocument> request = new();

                return await request.Get(
                    directory,
                    $"/{did}",
                    accessToken: null,
                    httpClient: httpClient,
                    cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            else if (did.ToString().StartsWith(webDidPrefix, StringComparison.InvariantCulture))
            {
                string webDidDomainNameAndPath = HttpUtility.UrlDecode(did.ToString().Substring(webDidPrefix.Length).Replace(':', '/'));

                Uri service = new($"https://{webDidDomainNameAndPath}");

                AtProtoHttpClient<DidDocument> request = new();

                return await request.Get(
                    service,
                    $"/.well-known/did.json",
                    accessToken: null,
                    httpClient: httpClient,
                    cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException("DID is of an unknown type.", nameof(did));
            }
        }
    }
}
