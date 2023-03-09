﻿using System.Text.Json.Nodes;

namespace WebApiGateway.Models.API.Responses
{
    public class FailureResponse : BasicApiResponse
    {
        public JsonObject ErrorObject { get; set; }

        public FailureResponse(int statusCode, string? message, JsonObject errorObject) : base(statusCode, false, message)
        {
            ErrorObject = errorObject;
        }
    }
}