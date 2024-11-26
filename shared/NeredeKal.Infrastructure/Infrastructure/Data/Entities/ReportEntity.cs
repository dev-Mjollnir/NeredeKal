using NeredeKal.Infrastructure.Constants.Enums;
using NeredeKal.Infrastructure.Enums;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Entities
{
    public class ReportEntity : BaseEntity
    {
        public EventType EventType { get; set; }
        public ReportStatus Status { get; set; }
        public string Data { get; set; } = string.Empty;
        public string Query { get; set; }
    }
}
