using NeredeKal.Infrastructure.Constants.Enums;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Entities
{
    public class ContactEntity : BaseEntity
    {
        public Guid HotelId { get; set; }
        public required ContactInfoType ContactInfoType { get; set; } = ContactInfoType.Sms;
        public required string ContactInfoContent { get; set; } = string.Empty;
    }
}
