﻿// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

namespace idunno.AtProto
{
    /// <summary>
    /// The exception that is thrown when a handle cannot be resolved to a DID.
    /// </summary>
    [Serializable]
    public class HandleResolutionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResponseTypeException"/> class with a specified error message.
        /// </summary>
        public HandleResolutionException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResponseTypeException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the parsing error.</param>
        public HandleResolutionException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResponseTypeException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the parsing error.</param>
        /// <param name="inner">The exception that is the cause of the current exception</param>
        public HandleResolutionException(string message, Exception inner) : base(message, inner) { }
    }
}
