﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CashFlow.Reports.API.Domain.DTOs.Base;

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
}
