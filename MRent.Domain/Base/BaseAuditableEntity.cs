﻿namespace MRent.Domain.Base
{
    public abstract class BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
