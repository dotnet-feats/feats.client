﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace Feats.Evaluation.Client
{
    [ExcludeFromCodeCoverage]
    public class FailedToCommunicateWithEvaluationsException : Exception
    {
        public FailedToCommunicateWithEvaluationsException(HttpResponseMessage response, string content)
        : base($"An unexpected response was returned by the Feats Evaluation server: ${response.StatusCode} - {content}.")
        {
        }
    }
}
