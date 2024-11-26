using NeredeKal.Infrastructure.Constants.Enums;
using NeredeKal.Infrastructure.Enums;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using NeredeKal.Infrastructure.Infrastructure.Models;

namespace ReportService.Application.Dtos
{
    public record ReportDto : BaseDto
    {
        public string Query { get; set; }
        public string Data { get; set; }
        public ReportStatus Status { get; set; }
        public EventType EventType { get; set; }

        public static ReportDto Map(ReportEntity entity)
        {
            return new ReportDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = entity.ModifiedAt,
                Query = entity.Query,
                Data = entity.Data,
                Status = entity.Status,
                EventType = entity.EventType
            };
        }
    }
}