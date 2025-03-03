﻿namespace SubmitYourIdea.Services.Common;

public class EmailPayload
{
    public required string Email { get; set; }
    public required string Subject { get; set; }
    public required string Message { get; set; }
}