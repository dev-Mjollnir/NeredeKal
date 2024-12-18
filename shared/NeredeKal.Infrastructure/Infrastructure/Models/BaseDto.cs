﻿namespace NeredeKal.Infrastructure.Infrastructure.Models
{
    public record BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
