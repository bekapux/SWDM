﻿namespace Sawoodamo.API.Database.Entities;

public sealed class AuditTrail
{
    public string? Id { get; set; }
    public string? EntityId { get; set; }
    public string? EntityType { get; set; }
    public string? Action { get; set; }
    public string? ModifiesFieldsJoined { get; set; }
    public string? OldValue{ get; set; }
    public string? NewValue{ get; set; }
    public string? UserId { get; set; }
    public DateTime Timestamp { get; set; }
}
