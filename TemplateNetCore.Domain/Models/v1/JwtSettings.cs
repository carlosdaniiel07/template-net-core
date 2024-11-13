﻿namespace TemplateNetCore.Domain.Models.v1;

public class JwtSettings
{
    public string Secret { get; set; }
    public int ExpirationInMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
