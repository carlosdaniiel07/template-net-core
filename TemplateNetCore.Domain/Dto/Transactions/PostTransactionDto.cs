using System;
using System.ComponentModel.DataAnnotations;
using TemplateNetCore.Domain.Attributes;

namespace TemplateNetCore.Domain.Dto.Transactions
{
    public class PostTransactionDto
    {
        [Required]
        [MinLength(3)]
        public string Description { get; set; }
        
        [PositiveValue]
        public decimal Value { get; set; }

        [Required]
        public string TargetKey { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
