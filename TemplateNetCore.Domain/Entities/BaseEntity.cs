using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateNetCore.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
