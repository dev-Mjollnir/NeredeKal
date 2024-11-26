using MediatR;
using NeredeKal.Infrastructure.Constants.Enums;
using NeredeKal.Infrastructure.Infrastructure.Data.Context;
using System.Text.Json;

namespace ReportService.Application.Command
{
    public class HotelReportCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class HotelReportCommandHandler : IRequestHandler<HotelReportCommand>
    {
        private readonly IHotelDbContext _context;

        public HotelReportCommandHandler(IHotelDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(HotelReportCommand request, CancellationToken cancellationToken)
        {
            var report = _context.Reports.FirstOrDefault(x => x.Id == request.Id);
            if (report is null || report.Status != ReportStatus.Preparing)
            {
                return Unit.Task;
            }

            var location = report.Query.Split(":")[1];
            var contacts = _context.Contacts.AsEnumerable()
                .Where(x => x.ContactInfoType == ContactInfoType.Location && x.ContactInfoContent.Contains(location))
                .DistinctBy(x => x.HotelId)
                .ToList();

            if (contacts.Any())
            {
                var hotelIds = contacts.Select(x => x.HotelId).ToList();
                var phoneCount = _context.Contacts.AsEnumerable()
                    .Count(x => x.ContactInfoType == ContactInfoType.Sms && hotelIds.Contains(x.HotelId));

                var info = new
                {
                    location = location,
                    hotelCount = contacts.Count,
                    phoneCount = phoneCount
                };
                var data = JsonSerializer.Serialize(info, options: new JsonSerializerOptions { WriteIndented = true });
                report.Data = data;
            }
            report.Status = ReportStatus.Completed;
            _context.Reports.Update(report);
            _context.SaveChangesAsync(cancellationToken);
            return Unit.Task;
        }
    }
}
