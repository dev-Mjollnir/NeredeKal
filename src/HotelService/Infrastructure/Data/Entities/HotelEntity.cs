namespace HotelService.Infrastructure.Data.Entities
{
    public class HotelEntity : BaseEntity
    {
        public required string FirmName { get; set; } = string.Empty;
        public required string ResponsibleName { get; set; } = string.Empty;
        public required string ResponsibleSurname { get; set; } = string.Empty;
        public required IEnumerable<ContactEntity> Contacts { get; set; } = new List<ContactEntity>();
    }
}
