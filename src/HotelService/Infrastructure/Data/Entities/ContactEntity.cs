﻿using HotelService.Constants.Enums;

namespace HotelService.Infrastructure.Data.Entities
{
    public class ContactEntity : BaseEntity
    {
        public required Guid HotelId { get; set; }
        public required ContactInfoType ContactInfoType { get; set; } = ContactInfoType.Sms;
        public required string ContactInfoContent { get; set; } = string.Empty;
    }
}
