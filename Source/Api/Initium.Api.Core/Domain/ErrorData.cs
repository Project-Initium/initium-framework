// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Initium.Api.Core.Domain
{
    public sealed class ErrorData
    {
        public ErrorData(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public ErrorData(string code)
        {
            this.Code = code;
        }

        public string Code { get; }

        public string? Message { get; }
    }
}